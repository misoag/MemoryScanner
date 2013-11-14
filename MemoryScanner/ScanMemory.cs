using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using System.IO.MemoryMappedFiles;
using Aecial.MemoryScanner;
using AecialEngine;
/// <summary>
/// Scans pages of memory in sequential order
/// </summary>
/// 
namespace Aecial.MemoryScanner
{
    class ScanMemory
    {
        #region Variables/Objects
        private string ExecutablePath;
        //Thread related
        private List<Thread> ScanThreads = new List<Thread>();
        private const int ScanThreadCount = 1;  //Three seems to be a comfortable number
        private int FinishedThreads;            //Threads done scanning
        private volatile static int esi;        //Shared between threads to cycle through memory regions in a scan
        private static Int64 NextWriteIndex = 0;
        Object WriteLock = new Object();

        private Int64 MaxScanFileSize = 0;
        private Int64 CompareScanFileSize = 0;

        //Instance of ProcessMemoryEditor class to be used to read/write the memory.
        private ProcessMemoryEditor ProcessMemoryEditor;

        private ScanOptimizationData ScanOptimizationData;
        private ScanCompareType ScanCompareType;
        private ScanDataType ScanDataType;

        //Memory mapped files allow me to use files as virtual memory to store scan data to prevent this
        //program from running out of memory while holding memory of the target process.
        private MemoryMappedFile CurrentScanValues;
        private MemoryMappedFile CurrentScanAddresses;
        private MemoryMappedFile FirstScanValues;
        private MemoryMappedFile FirstScanAddresses;
        private MemoryMappedFile LastScanValues;
        private MemoryMappedFile LastScanAddresses;

        private bool IsRescan = false;
        private bool CompareToFirstScan = false;

        private int Progress; //Scan progress

        private IntPtr BaseAddress;
        private IntPtr MaxAddress;
        private List<IntPtr> AddressLower = new List<IntPtr>();
        private List<IntPtr> AddressUpper = new List<IntPtr>();

        private object ScanValue;
        private object SecondScanValue; //Used for in between x and y scans

        //Float/Double scan variables
        private Int64 _ScanValue; //Rounded value for singles/doubles for significant digit comparisons
        private int SignificantDigits;
        private bool Truncated;

        private uint[] Protect = new uint[8];
        private uint[] Type = new uint[3];
        private int RegionCount;

        //Scan events
        public delegate void ScanProgressedEventHandler(object sender, ScanProgressChangedEventArgs e);
        public event ScanProgressedEventHandler ScanProgressChanged;
        private ScanProgressChangedEventArgs ScanProgressEventArgs;
        public delegate void ScanCompletedEventHandler(object sender, ScanCompletedEventArgs e);
        public event ScanCompletedEventHandler ScanCompleted;
        public delegate void ScanCanceledEventHandler(object sender, ScanCanceledEventArgs e);
        public event ScanCanceledEventHandler ScanCanceled;

        #endregion

        #region Full Scan
        //Constructor
        public ScanMemory(Process Process, UInt64 StartAddress, UInt64 EndAddress, ScanDataType ScanDataType,
            ScanCompareType ScanCompareType, object ScanValue, object SecondScanValue, uint[] Protect,
            uint[] Type, bool Truncated, ScanOptimizationData ScanOptimizationData, string ExecutablePath, bool CompareToFirstScan)
        {
            ProcessMemoryEditor = new ProcessMemoryEditor();
            ProcessMemoryEditor.ReadProcess = Process;

            this.BaseAddress = (IntPtr)StartAddress;
            this.MaxAddress = (IntPtr)EndAddress;
            this.ScanDataType = ScanDataType;
            this.ScanCompareType = ScanCompareType;
            this.Protect = Protect;
            this.Type = Type;
            this.ScanValue = ScanValue;
            this.SecondScanValue = SecondScanValue;
            this.Truncated = Truncated;
            this.ScanOptimizationData = ScanOptimizationData;
            this.ExecutablePath = ExecutablePath;
            this.CompareToFirstScan = CompareToFirstScan;
        }

        #region Scan / Rescan methods
        //Get ready to scan the memory for the value
        public void StartScan(bool IsRescan)
        {
            this.IsRescan = IsRescan;
            //Check if the thread is already defined or not.
            if (ScanThreads != null)
                for (int ecx = 0; ecx < ScanThreads.Count; ecx++)
                    //If the thread is already defined and is Alive,
                    if (ScanThreads[ecx].IsAlive)
                    {
                        //raise the event that shows that the last scan task is canceled
                        //(because a new task is going to be started as wanted),
                        ScanCanceledEventArgs cancelEventArgs = new ScanCanceledEventArgs();
                        ScanCanceled(this, cancelEventArgs);

                        //and then abort the alive thread and so cancel last scan task
                        ScanThreads[ecx].Abort();
                    }

            //Make necessary preperations for scan
            PrepareScan();

            //Start multiple threads scanning for our new value
            for (int ecx = 0; ecx < ScanThreadCount; ecx++)
            {
                Thread ScanThread;
                if (!IsRescan)
                    ScanThread = new Thread(MemoryScanner);
                else
                    ScanThread = new Thread(MemoryRescanner);
                ScanThread.Priority = ThreadPriority.AboveNormal;
                ScanThread.Start();
                ScanThreads.Add(ScanThread);
            }
        }

        //Cancels this scan
        public void CancelScan()
        {
            //Raise scan canceled event
            ScanCanceledEventArgs cancelEventArgs = new ScanCanceledEventArgs();
            ScanCanceled(this, cancelEventArgs);

            //Abort all threads
            for (int ecx = 0; ecx < ScanThreads.Count; ecx++)
                ScanThreads[ecx].Abort();
        }

        #endregion

        private void PrepareScan()
        {
            //Calculate significant digits if applicable
            CalculateSignificantFigures(ScanDataType);

            esi = 0;
            FinishedThreads = 0;
            NextWriteIndex = 0;

            //Open the pocess to allow reading of memory
            ProcessMemoryEditor.OpenProcess();

            //Get memory regions that we need
            GetMemoryRegions();

            //Set region of virtual memory to the highest possible size based on memory region sizes
            if (!IsRescan) //First scan
            {
                using (FileStream FileStream = File.Open(@ExecutablePath + @"/Scan Data/FirstScanAddresses", FileMode.Open, FileAccess.ReadWrite))
                {
                    FileStream.SetLength(MaxScanFileSize);
                    FileStream.Close();
                }
                using (FileStream FileStream = File.Open(@ExecutablePath + @"/Scan Data/FirstScanValues", FileMode.Open, FileAccess.ReadWrite))
                {
                    FileStream.SetLength(MaxScanFileSize);
                    FileStream.Close();
                }
                FirstScanAddresses = MemoryMappedFile.CreateFromFile(@ExecutablePath + @"/Scan Data/FirstScanAddresses", FileMode.Open, "FirstScanAddresses", MaxScanFileSize);
                FirstScanValues = MemoryMappedFile.CreateFromFile(@ExecutablePath + @"/Scan Data/FirstScanValues", FileMode.Open, "FirstScanValues", MaxScanFileSize);
            }
            else //Rescan
            {
                //Copy and overwrite current scan data to last scan data file
                File.Delete(@ExecutablePath + @"/Scan Data/LastScanAddresses");
                File.Copy(@ExecutablePath + @"/Scan Data/CurrentScanAddresses", @ExecutablePath + @"/Scan Data/LastScanAddresses", false);
                File.Delete(@ExecutablePath + @"/Scan Data/LastScanValues");
                File.Copy(@ExecutablePath + @"/Scan Data/CurrentScanValues", @ExecutablePath + @"/Scan Data/LastScanValues", false);

                //Create Memory Map accessors to first/last scan depending on compare type
                if (CompareToFirstScan)
                {
                    using (FileStream FileStream = File.Open(@ExecutablePath + @"/Scan Data/FirstScanAddresses", FileMode.Open, FileAccess.Read))
                    {
                        CompareScanFileSize = FileStream.Length;
                        FileStream.Close();
                    }
                    FirstScanAddresses = MemoryMappedFile.CreateFromFile(@ExecutablePath + @"/Scan Data/FirstScanAddresses", FileMode.Open, "FirstScanAddresses", CompareScanFileSize);
                    FirstScanValues = MemoryMappedFile.CreateFromFile(@ExecutablePath + @"/Scan Data/FirstScanValues", FileMode.Open, "FirstScanValues", CompareScanFileSize);
                }
                else
                {
                    using (FileStream FileStream = File.Open(@ExecutablePath + @"/Scan Data/LastScanAddresses", FileMode.Open, FileAccess.Read))
                    {
                        CompareScanFileSize = FileStream.Length;
                        FileStream.Close();
                    }
                    LastScanAddresses = MemoryMappedFile.CreateFromFile(@ExecutablePath + @"/Scan Data/LastScanAddresses", FileMode.Open, "LastScanAddresses", CompareScanFileSize);
                    LastScanValues = MemoryMappedFile.CreateFromFile(@ExecutablePath + @"/Scan Data/LastScanValues", FileMode.Open, "LastScanValues", CompareScanFileSize);
                }

                //Current scan file size will never exceed that of the last, so set the file size to that of the last
                using (FileStream FileStream = File.Open(@ExecutablePath + @"/Scan Data/CurrentScanAddresses", FileMode.Open, FileAccess.ReadWrite))
                {
                    FileStream.SetLength(CompareScanFileSize);
                    FileStream.Close();
                }
                using (FileStream FileStream = File.Open(@ExecutablePath + @"/Scan Data/CurrentScanValues", FileMode.Open, FileAccess.ReadWrite))
                {
                    FileStream.SetLength(CompareScanFileSize);
                    FileStream.Close();
                }

                //Get memory mapped files for current scan to create accessors later.
                CurrentScanAddresses = MemoryMappedFile.CreateFromFile(@ExecutablePath + @"/Scan Data/CurrentScanAddresses", FileMode.Open, "CurrentScanAddresses", CompareScanFileSize);
                CurrentScanValues = MemoryMappedFile.CreateFromFile(@ExecutablePath + @"/Scan Data/CurrentScanValues", FileMode.Open, "CurrentScanValues", CompareScanFileSize);

            }
        }

        private unsafe void MemoryScanner()
        {
            //Calculate how many bytes after a byte[] index that the type needs to properly retrieve a value
            int ArraysDifference = (int)Conversions.Conversions.ScanDataTypeToDataTypeSize(ScanDataType) - 1;

            byte[] ArrayOfBytes; //An array to hold the bytes read from the memory.
            int BytesReadSize;   //Checks if bytes have been read from the memory. 
            ulong RegionSize;    //Region sized based on difference of start/end addresses of memory block

            List<UInt64> FoundAddresses = new List<UInt64>();
            List<UInt64> FoundValues = new List<UInt64>();

            MemoryMappedViewAccessor ThreadFirstAddressAccessor = FirstScanAddresses.CreateViewAccessor(0, MaxScanFileSize);
            MemoryMappedViewAccessor ThreadFirstValuesAccessor = FirstScanValues.CreateViewAccessor(0, MaxScanFileSize);

            //Set up address increment value according to scan optimization data
            UInt64 IncrementValue = 1;
            if (ScanOptimizationData.Enabled)
            {
                if (!ScanOptimizationData.IsLastDigit)
                    IncrementValue = ScanOptimizationData.Allignment;
            }

            int ecx;

            //Cycle through evaluating one region at a time (per thread)
            while (true)
            {
                //Grab the next unique esi value and store it locally. The current thread will be
                //responsible for dealing with this index.
                ecx = esi++;

                //Exit if out of range
                if (ecx >= RegionCount)
                    break;

                //Calculate the size of memory to scan.
                RegionSize = (ulong)AddressUpper[ecx] - (ulong)AddressLower[ecx];

                //Read all memory from the region
                ArrayOfBytes = ProcessMemoryEditor.ReadAOB(AddressLower[ecx], (uint)RegionSize, out BytesReadSize);

                //Only proceed if bytes have been read
                if (BytesReadSize <= 0)
                    continue;

                UInt64 OffsetMax = (UInt64)(ArrayOfBytes.Length - ArraysDifference);

                //Loop through the bytes (according to allignment) to look for the values.
                for (UInt64 Offset = 0; Offset < OffsetMax; Offset += IncrementValue)
                {

                    switch (ScanDataType)
                    {
                        case ScanDataType.Binary:
                            break;
                        case ScanDataType.Byte:
                            if (CompareValues(ArrayOfBytes[Offset], 0))
                            {
                                FoundAddresses.Add(Offset + (UInt64)AddressLower[ecx]);
                                FoundValues.Add((UInt64)ArrayOfBytes[Offset]);
                            }
                            break;
                        case ScanDataType.Int16:
                            fixed (byte* PointerToAOB = &ArrayOfBytes[Offset])
                            {
                                if (CompareValues(*(UInt16*)PointerToAOB, 0))
                                {
                                    FoundAddresses.Add(Offset + (UInt64)AddressLower[ecx]);
                                    FoundValues.Add((UInt64)(*(UInt16*)PointerToAOB));
                                }
                            }
                            break;
                        case ScanDataType.Int32:
                            fixed (byte* PointerToAOB = &ArrayOfBytes[Offset])
                            {
                                if (CompareValues(*(UInt32*)PointerToAOB, 0))
                                {
                                    FoundAddresses.Add(Offset + (UInt64)AddressLower[ecx]);
                                    FoundValues.Add(*(UInt32*)PointerToAOB);
                                }
                            }
                            break;
                        case ScanDataType.Int64:
                            fixed (byte* PointerToAOB = &ArrayOfBytes[Offset])
                            {
                                if (CompareValues(*(UInt64*)PointerToAOB, 0))
                                {
                                    FoundAddresses.Add(Offset + (UInt64)AddressLower[ecx]);
                                    FoundValues.Add(*(UInt64*)PointerToAOB);
                                }
                            }
                            break;
                        case ScanDataType.Single:
                            fixed (byte* PointerToAOB = &ArrayOfBytes[Offset])
                            {
                                if (CompareValues(*(Single*)PointerToAOB, 0))
                                {
                                    FoundAddresses.Add(Offset + (UInt64)AddressLower[ecx]);
                                    FoundValues.Add((UInt64)(*(UInt32*)PointerToAOB));
                                }
                            }
                            break;
                        case ScanDataType.Double:
                            fixed (byte* PointerToAOB = &ArrayOfBytes[Offset])
                            {
                                if (CompareValues(*(Double*)PointerToAOB, 0))
                                {
                                    FoundAddresses.Add(Offset + (UInt64)AddressLower[ecx]);
                                    FoundValues.Add(*(UInt64*)PointerToAOB);
                                }
                            }
                            break;
                        default:
                            throw new Exception("Unsupported scan type");
                    } //End switch

                } //End for

                //Write all found from that memory region
                lock (WriteLock)
                {
                    ThreadFirstAddressAccessor.WriteArray(NextWriteIndex, FoundAddresses.ToArray(), 0, FoundAddresses.Count);
                    ThreadFirstValuesAccessor.WriteArray(NextWriteIndex, FoundValues.ToArray(), 0, FoundValues.Count);
                    NextWriteIndex += FoundAddresses.Count * (Int64)DataTypeSize.Int64;
                }

                FoundAddresses.Clear();
                FoundValues.Clear();

                UpdateProgress(ecx);

            } //End while

            //Clean up so that we can access the file later without it being in use.
            ThreadFirstAddressAccessor.Dispose();
            ThreadFirstValuesAccessor.Dispose();

            if (++FinishedThreads == ScanThreadCount)
                FinishEvents(NextWriteIndex);

        }

        //TODO: USE readarray function because
        //Reading one by one is a sack of shit
        //Quasi purpose defeating. Well shit.

        private unsafe void MemoryRescanner()
        {
            //An array to hold the bytes read from the memory.
            byte[] ArrayOfBytes;
            //This will be used to check if any bytes have been read from the memory
            int BytesReadSize;
            ulong RegionSize; //Region sized based on difference of start/end addresses of memory block

            //UInt64 StoredValue;
            UInt64 StoredAddress;

            List<UInt64> FoundAddresses = new List<UInt64>();
            List<UInt64> FoundValues = new List<UInt64>();

            //Create virtual memory accessors for the current scan data
            MemoryMappedViewAccessor ThreadCurrentAddressAccessor = CurrentScanAddresses.CreateViewAccessor(0, CompareScanFileSize);
            MemoryMappedViewAccessor ThreadCurrentValueAccessor = CurrentScanValues.CreateViewAccessor(0, CompareScanFileSize);

            //Create virtual memory accessors for either first scan or last scan
            MemoryMappedViewAccessor ThreadCompareAddressAccessor;
            MemoryMappedViewAccessor ThreadCompareValueAccessor;
            if (CompareToFirstScan)
            {
                ThreadCompareAddressAccessor = FirstScanAddresses.CreateViewAccessor(0, CompareScanFileSize);
                ThreadCompareValueAccessor = FirstScanValues.CreateViewAccessor(0, CompareScanFileSize);
            }
            else
            {
                ThreadCompareAddressAccessor = LastScanAddresses.CreateViewAccessor(0, CompareScanFileSize);
                ThreadCompareValueAccessor = LastScanValues.CreateViewAccessor(0, CompareScanFileSize);
            }

            int ecx; //Region index to be evaluated by this thread

            while (true)
            {
                //Get unique esi value, and then a thread will be responsible for the obtained index
                ecx = esi++;

                //Exit
                if (ecx >= RegionCount)
                    break;

                //Get the region size of this memory block
                RegionSize = (ulong)AddressUpper[ecx] - (ulong)AddressLower[ecx];

                Int64 LowAddressIndex; //Lowest address in memory block that is also in our list

                //Walk forward through found addresses until we find the first address in range for this memory block
                for (Int64 edx = 0; edx < CompareScanFileSize; edx += (Int64)DataTypeSize.Int64)
                {
                    if (ThreadCompareAddressAccessor.ReadUInt64(edx) >= (UInt64)AddressLower[ecx])
                    {
                        //If address is too large, then there are no addresses we care for in this memory block.
                        if (ThreadCompareAddressAccessor.ReadUInt64(edx) > (UInt64)AddressUpper[ecx])
                            break;
                        LowAddressIndex = edx / (Int64)DataTypeSize.Int64;
                        goto ValidMemoryBlock;
                    }
                }
                continue; //No addresses in list are valid
            ValidMemoryBlock:

                Int64 HighAddressIndex = LowAddressIndex;
       
                //Walk forwards through addresses (starting with next index) until we find the highest in this memory block
            for (Int64 edx = LowAddressIndex * (Int64)DataTypeSize.Int64 + (Int64)DataTypeSize.Int64; edx < CompareScanFileSize; edx += (Int64)DataTypeSize.Int64)
                {
                    //Continue to find next highest
                    if (ThreadCompareAddressAccessor.ReadUInt64(edx) <= (UInt64)AddressUpper[ecx])
                        HighAddressIndex = edx / (Int64)DataTypeSize.Int64;
                    else //Next highest exceeds this memory regions bounds; don't take new value & continue
                        break;
                }

                //Read all memory from the region
                ArrayOfBytes = ProcessMemoryEditor.ReadAOB(AddressLower[ecx], (uint)RegionSize, out BytesReadSize);

                //Don't proceed if nothing has been read
                if (BytesReadSize == 0)
                    continue;

                int RegionSizeAsInt = (int)RegionSize; //Prevents need for casting thousands of times later
                int Offset; //Offset from this memory region that is the same as current address
                //Cycle through range of valid addresses in memory block
                for (Int64 CurrentAddressIndex = LowAddressIndex; CurrentAddressIndex <= HighAddressIndex; CurrentAddressIndex++)
                {
                    ThreadCompareAddressAccessor.Read(CurrentAddressIndex * (Int64)DataTypeSize.UInt64, out StoredAddress);
                    //figure out an offset based on address being checked and lowest address in memory block
                    Offset = (int)(StoredAddress - (UInt64)AddressLower[ecx]);

                    if (Offset == RegionSizeAsInt)
                        continue;

                    //Compare & add if a match is found
                    switch (ScanDataType)
                    {
                        case ScanDataType.Binary:
                            break;
                        case ScanDataType.Byte:
                            if (CompareValues(ArrayOfBytes[Offset], ThreadCompareValueAccessor.ReadUInt64(CurrentAddressIndex * (Int64)DataTypeSize.UInt64)))
                            {
                                FoundAddresses.Add(StoredAddress);
                                FoundValues.Add((UInt64)ArrayOfBytes[Offset]);
                            }
                            break;
                        case ScanDataType.Int16:
                            fixed (byte* PointerToAOB = &ArrayOfBytes[Offset])
                            {
                                if (CompareValues(*(UInt16*)PointerToAOB, ThreadCompareValueAccessor.ReadUInt64(CurrentAddressIndex * (Int64)DataTypeSize.UInt64)))
                                {
                                    FoundAddresses.Add(StoredAddress);
                                    FoundValues.Add((UInt64)(*(UInt16*)PointerToAOB));
                                }
                            }
                            break;
                        case ScanDataType.Int32:
                            fixed (byte* PointerToAOB = &ArrayOfBytes[Offset])
                            {
                                if (CompareValues(*(UInt32*)PointerToAOB, ThreadCompareValueAccessor.ReadUInt64(CurrentAddressIndex * (Int64)DataTypeSize.UInt64)))
                                {
                                    FoundAddresses.Add(StoredAddress);
                                    FoundValues.Add((UInt64)(*(UInt32*)PointerToAOB));
                                }
                            }
                            break;
                        case ScanDataType.Int64:
                            fixed (byte* PointerToAOB = &ArrayOfBytes[Offset])
                            {
                                if (CompareValues(*(UInt64*)PointerToAOB, ThreadCompareValueAccessor.ReadUInt64(CurrentAddressIndex * (Int64)DataTypeSize.UInt64)))
                                {
                                    FoundAddresses.Add(StoredAddress);
                                    FoundValues.Add(*(UInt64*)PointerToAOB);
                                }
                            }
                            break;
                        case ScanDataType.Single:
                            fixed (byte* PointerToAOB = &ArrayOfBytes[Offset])
                            {
                                if (CompareValues(*(Single*)PointerToAOB, ThreadCompareValueAccessor.ReadUInt64(CurrentAddressIndex * (Int64)DataTypeSize.UInt64)))
                                {
                                    FoundAddresses.Add(StoredAddress);
                                    FoundValues.Add((UInt64)(*(UInt32*)PointerToAOB));
                                }
                            }
                            break;
                        case ScanDataType.Double:
                            fixed (byte* PointerToAOB = &ArrayOfBytes[Offset])
                            {
                                if (CompareValues(*(Double*)PointerToAOB, ThreadCompareValueAccessor.ReadUInt64(CurrentAddressIndex * (Int64)DataTypeSize.UInt64)))
                                {
                                    FoundAddresses.Add(StoredAddress);
                                    FoundValues.Add(*(UInt64*)PointerToAOB);
                                }
                            }
                            break;
                        default:
                            throw new Exception("Unsupported scan type.");
                    } //end switch
                } //end for

                //Write all found from that memory region
                ThreadCurrentAddressAccessor.WriteArray(NextWriteIndex, FoundAddresses.ToArray(), 0, FoundAddresses.Count);
                ThreadCurrentValueAccessor.WriteArray(NextWriteIndex, FoundValues.ToArray(), 0, FoundValues.Count);
                NextWriteIndex += FoundAddresses.Count * (Int64)DataTypeSize.Int64;
                FoundAddresses.Clear();
                FoundValues.Clear();

                UpdateProgress(ecx);
            }

            ThreadCurrentAddressAccessor.Dispose();
            ThreadCurrentValueAccessor.Dispose();
            ThreadCompareAddressAccessor.Dispose();
            ThreadCompareValueAccessor.Dispose();
            //Check if all threads are finished and wrap up scan
            if (++FinishedThreads == ScanThreadCount)
                FinishEvents(NextWriteIndex);
        }


        #endregion

        private void FinishEvents(Int64 TotalFoundSize)
        {

            if (!IsRescan) //First scan unique events
            {

                FirstScanAddresses.Dispose();
                FirstScanValues.Dispose();
                //Resize the files to match how many items were found
                using (FileStream FileStream = File.Open(@ExecutablePath + @"/Scan Data/FirstScanAddresses", FileMode.Open, FileAccess.ReadWrite))
                {
                    FileStream.SetLength(TotalFoundSize);
                    FileStream.Close();
                }
                using (FileStream FileStream = File.Open(@ExecutablePath + @"/Scan Data/FirstScanValues", FileMode.Open, FileAccess.ReadWrite))
                {
                    FileStream.SetLength(TotalFoundSize);
                    FileStream.Close();
                }
                //Copy first scan results to current
                File.Delete(@ExecutablePath + @"/Scan Data/CurrentScanAddresses");
                File.Copy(@ExecutablePath + @"/Scan Data/FirstScanAddresses", @ExecutablePath + @"/Scan Data/CurrentScanAddresses", false);
                File.Delete(@ExecutablePath + @"/Scan Data/CurrentScanValues");
                File.Copy(@ExecutablePath + @"/Scan Data/FirstScanValues", @ExecutablePath + @"/Scan Data/CurrentScanValues", false);
            }
            else //Rescan unique events
            {
                CurrentScanAddresses.Dispose();
                CurrentScanValues.Dispose();

                if (CompareToFirstScan)
                {
                    FirstScanAddresses.Dispose();
                    FirstScanValues.Dispose();
                }
                else
                {
                    LastScanAddresses.Dispose();
                    LastScanValues.Dispose();
                }

                //Resize the files to match how many items were found
                using (FileStream FileStream = File.Open(@ExecutablePath + @"/Scan Data/CurrentScanAddresses", FileMode.Open, FileAccess.ReadWrite))
                {
                    FileStream.SetLength(TotalFoundSize);
                    FileStream.Close();
                }
                using (FileStream FileStream = File.Open(@ExecutablePath + @"/Scan Data/CurrentScanValues", FileMode.Open, FileAccess.ReadWrite))
                {
                    FileStream.SetLength(TotalFoundSize);
                    FileStream.Close();
                }
            }


            //Close the handle to the process to avoid process errors.
            ProcessMemoryEditor.CloseHandle();

            //Prepare and raise the ScanCompleted event.
            ScanCompletedEventArgs ScanCompleteEventArgs = new ScanCompletedEventArgs(TotalFoundSize);
            ScanCompleted(this, ScanCompleteEventArgs);
        }

        //Update scan progress
        private void UpdateProgress(int ecx)
        {
            //Only update if new progress is higher (can be lower due to multithreading)
            if (Progress < (int)(((double)ecx / (double)(RegionCount-1)) * 100d))
            {
                Progress = (int)(((double)ecx / (double)(RegionCount - 1)) * 100d);
                ScanProgressEventArgs = new ScanProgressChangedEventArgs(Progress);
                ScanProgressChanged(this, ScanProgressEventArgs);
            }
        }

        //Computes logic regarding significant decimal places if the scan type is single/float
        public void CalculateSignificantFigures(ScanDataType ScanDataType)
        {
            switch (ScanDataType)
            {
                case ScanDataType.Single:
                    //Determine significant decimal figures based on how many user typed
                    if (ScanValue != null)
                    {
                        string FloatString = Convert.ToString((Single)ScanValue);
                        int DecimalIndex = FloatString.IndexOf(".");
                        if (DecimalIndex == -1)
                            SignificantDigits = 0;
                        else
                            SignificantDigits = (FloatString.Length - 1) - DecimalIndex;
                        //We multiply numbers we find by 10^x power and cast to an int64,
                        //so that we only check up to the correct decimal place
                        SignificantDigits = (Int32)Math.Pow(10d, SignificantDigits);
                        _ScanValue = (Int64)((Single)ScanValue * SignificantDigits);
                    }
                    break;
                case ScanDataType.Double:
                    if (ScanValue != null)
                    {
                        string DoubleString = Convert.ToString((Double)ScanValue);
                        int DecimalIndex = DoubleString.IndexOf(".");
                        if (DecimalIndex == -1)
                            SignificantDigits = 0;
                        else
                            SignificantDigits = (DoubleString.Length - 1) - DecimalIndex;

                        SignificantDigits = (Int32)Math.Pow(10d, SignificantDigits);
                        _ScanValue = (Int64)((Double)ScanValue * SignificantDigits);
                    }
                    break;
                default:
                    return;
            }
        }

        public void GetMemoryRegions()
        {
            ulong Address = (ulong)BaseAddress;
            ulong PreviousAddress = (ulong)BaseAddress;
            MaxScanFileSize = 0;

            AddressLower.Clear();
            AddressUpper.Clear();

            //Use VirtualQueryEx to find readable blocks of memory with protection types we care about
            do
            {
                ProcessMemoryEditor.ProcessMemoryReaderApi.MEMORY_BASIC_INFORMATION64 m = new ProcessMemoryEditor.ProcessMemoryReaderApi.MEMORY_BASIC_INFORMATION64();

                //Call our own VirtualQueryEx function that distinguishes between a 64
                //And 32-bit process and returns useful information about the process.
                ProcessMemoryEditor.VirtualQueryEx(ProcessMemoryEditor.ReadProcess.Handle,
                    (IntPtr)Address, out m, (uint)Marshal.SizeOf(m));

                PreviousAddress = Address;
                Address = (ulong)((uint)m.BaseAddress + m.RegionSize);

                //Compare to memory protections we want to scan
                //Protect[] is either 0 or 1 (disabled or enabled)
                if (m.State == (uint)ProcessMemoryEditor.ProcessMemoryReaderApi.MEMORY_STATE.COMMIT)
                    if (m.Protect * Protect[0] == (uint)ProcessMemoryEditor.ProcessMemoryReaderApi.MemoryProtection.PAGE_NOACCESS
                        || m.Protect * Protect[1] == (uint)ProcessMemoryEditor.ProcessMemoryReaderApi.MemoryProtection.PAGE_READONLY
                        || m.Protect * Protect[2] == (uint)ProcessMemoryEditor.ProcessMemoryReaderApi.MemoryProtection.PAGE_READWRITE
                        || m.Protect * Protect[3] == (uint)ProcessMemoryEditor.ProcessMemoryReaderApi.MemoryProtection.PAGE_WRITECOPY
                        || m.Protect * Protect[4] == (uint)ProcessMemoryEditor.ProcessMemoryReaderApi.MemoryProtection.PAGE_EXECUTE
                        || m.Protect * Protect[5] == (uint)ProcessMemoryEditor.ProcessMemoryReaderApi.MemoryProtection.PAGE_EXECUTE_READ
                        || m.Protect * Protect[6] == (uint)ProcessMemoryEditor.ProcessMemoryReaderApi.MemoryProtection.PAGE_EXECUTE_READWRITE
                        || m.Protect * Protect[7] == (uint)ProcessMemoryEditor.ProcessMemoryReaderApi.MemoryProtection.PAGE_EXECUTE_WRITECOPY)
                    {
                        //Compare to memory types we want to scan
                        if (m.lType * Type[0] == (uint)ProcessMemoryEditor.ProcessMemoryReaderApi.MEMORY_TYPE.PRIVATE
                            || m.lType * Type[1] == (uint)ProcessMemoryEditor.ProcessMemoryReaderApi.MEMORY_TYPE.IMAGE
                           || m.lType * Type[2] == (uint)ProcessMemoryEditor.ProcessMemoryReaderApi.MEMORY_TYPE.MAPPED)
                        {
                            //Create the next upper and lower bounds of the memory chunks to be scanned
                            AddressLower.Add((IntPtr)m.BaseAddress);
                            AddressUpper.Add((IntPtr)(m.BaseAddress + m.RegionSize));
                            MaxScanFileSize += (Int64)m.RegionSize;

                            RegionCount++;
                        }
                    }

            } while (!(PreviousAddress > Address) && !(Address > (ulong)MaxAddress));
            MaxScanFileSize *= (Int64)DataTypeSize.Int64;
        }


        #region Value Comparisons

        /*
         * NO need for last addresses or first addresses
         * 
         * last values indices will be the same for both
         * 
         * first address indices should be removed on a compare value false return
         * 
         * at the end of a next scan, create a backup of the first scan in case of an undo scan
         * 
         */
        public bool CompareValues(Byte ReadValue, UInt64 StoredValue)
        {

            switch (ScanCompareType)
            {
                case ScanCompareType.Exact_Value:
                    if (ReadValue == (Byte)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Not_Equal_to_Value:
                    if (ReadValue != (Byte)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Changed_Value:

                    if (ReadValue != (Byte)StoredValue)
                        return true;
                    break;
                case ScanCompareType.Increased_Value:
                    if (ReadValue > (Byte)(StoredValue))
                        return true;
                    break;
                case ScanCompareType.Decreased_Value:
                    if (ReadValue < (Byte)(StoredValue))
                        return true;
                    break;
                case ScanCompareType.Unchanged_Value:
                    if (ReadValue == (Byte)(StoredValue))
                        return true;
                    break;
                case ScanCompareType.Less_Than:
                    if (ReadValue < (Byte)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Greater_Than:
                    if (ReadValue > (Byte)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Less_Than_or_Equal:
                    if (ReadValue <= (Byte)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Greater_Than_or_Equal:
                    if (ReadValue >= (Byte)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Between_Exclusive:
                    if (ReadValue > (Byte)ScanValue && ReadValue < (Byte)SecondScanValue)
                        return true;
                    break;
                case ScanCompareType.Between_Inclusive:
                    if (ReadValue >= (Byte)ScanValue && ReadValue <= (Byte)SecondScanValue)
                        return true;
                    break;
                case ScanCompareType.Increased_by_Value:
                    if (ReadValue == (Byte)(StoredValue) + (Byte)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Decreased_by_Value:
                    if (ReadValue == (Byte)(StoredValue) - (Byte)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Unknown_Value:
                    return true;
            }
            return false;
        }

        private unsafe bool CompareValues(UInt16 ReadValue, UInt64 StoredValue)
        {

            switch (ScanCompareType)
            {
                case ScanCompareType.Exact_Value:
                    if (ReadValue == (UInt16)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Not_Equal_to_Value:
                    if (ReadValue != (UInt16)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Changed_Value:
                    if (ReadValue != (UInt16)(StoredValue))
                        return true;
                    break;
                case ScanCompareType.Increased_Value:
                    if (ReadValue > (UInt16)(StoredValue))
                        return true;
                    break;
                case ScanCompareType.Decreased_Value:
                    if (ReadValue < (UInt16)(StoredValue))
                        return true;
                    break;
                case ScanCompareType.Unchanged_Value:
                    if (ReadValue == (UInt16)(StoredValue))
                        return true;
                    break;
                case ScanCompareType.Less_Than:
                    if (ReadValue < (UInt16)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Greater_Than:
                    if (ReadValue > (UInt16)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Less_Than_or_Equal:
                    if (ReadValue <= (UInt16)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Greater_Than_or_Equal:
                    if (ReadValue >= (UInt32)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Between_Exclusive:
                    if (ReadValue > (UInt16)ScanValue && ReadValue < (UInt16)SecondScanValue)
                        return true;
                    break;
                case ScanCompareType.Between_Inclusive:
                    if (ReadValue >= (UInt16)ScanValue && ReadValue <= (UInt16)SecondScanValue)
                        return true;
                    break;
                case ScanCompareType.Increased_by_Value:
                    if (ReadValue == (UInt16)(StoredValue) + (UInt16)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Decreased_by_Value:
                    if (ReadValue == (UInt16)(StoredValue) - (UInt16)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Unknown_Value:
                    return true;
            }
            return false;
        }

        private bool CompareValues(UInt32 ReadValue, UInt64 StoredValue)
        {
            switch (ScanCompareType)
            {
                case ScanCompareType.Exact_Value:
                    if (ReadValue == (UInt32)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Not_Equal_to_Value:
                    if (ReadValue != (UInt32)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Changed_Value:
                    if (ReadValue != (UInt32)(StoredValue))
                        return true;
                    break;
                case ScanCompareType.Increased_Value:
                    if (ReadValue > (UInt32)(StoredValue))
                        return true;
                    break;
                case ScanCompareType.Decreased_Value:
                    if (ReadValue < (UInt32)(StoredValue))
                        return true;
                    break;
                case ScanCompareType.Unchanged_Value:
                    if (ReadValue == (UInt32)(StoredValue))
                        return true;
                    break;
                case ScanCompareType.Less_Than:
                    if (ReadValue < (UInt32)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Greater_Than:
                    if (ReadValue > (UInt32)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Less_Than_or_Equal:
                    if (ReadValue <= (UInt32)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Greater_Than_or_Equal:
                    if (ReadValue >= (UInt32)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Between_Exclusive:
                    if (ReadValue > (UInt32)ScanValue && ReadValue < (UInt32)SecondScanValue)
                        return true;
                    break;
                case ScanCompareType.Between_Inclusive:
                    if (ReadValue >= (UInt32)ScanValue && ReadValue <= (UInt32)SecondScanValue)
                        return true;
                    break;
                case ScanCompareType.Increased_by_Value:
                    if (ReadValue == (UInt32)(StoredValue) + (UInt32)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Decreased_by_Value:
                    if (ReadValue == (UInt32)(StoredValue) - (UInt32)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Unknown_Value:
                    return true;
            }
            return false;
        }

        private bool CompareValues(UInt64 ReadValue, UInt64 StoredValue)
        {
            UInt64 _ReadValue = (UInt64)Math.Round((ReadValue * (Single)SignificantDigits), 0, MidpointRounding.AwayFromZero);

            switch (ScanCompareType)
            {
                case ScanCompareType.Exact_Value:
                    if (ReadValue == (UInt64)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Not_Equal_to_Value:
                    if (ReadValue != (UInt64)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Changed_Value:
                    if (ReadValue != (UInt64)(StoredValue))
                        return true;
                    break;
                case ScanCompareType.Increased_Value:
                    if (ReadValue > (UInt64)(StoredValue))
                        return true;
                    break;
                case ScanCompareType.Decreased_Value:
                    if (ReadValue < (UInt64)(StoredValue))
                        return true;
                    break;
                case ScanCompareType.Unchanged_Value:
                    if (ReadValue == (UInt64)(StoredValue))
                        return true;
                    break;
                case ScanCompareType.Less_Than:
                    if (ReadValue < (UInt64)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Greater_Than:
                    if (ReadValue > (UInt64)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Less_Than_or_Equal:
                    if (ReadValue <= (UInt64)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Greater_Than_or_Equal:
                    if (ReadValue >= (UInt64)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Between_Exclusive:
                    if (ReadValue > (UInt64)ScanValue && ReadValue < (UInt64)SecondScanValue)
                        return true;
                    break;
                case ScanCompareType.Between_Inclusive:
                    if (ReadValue >= (UInt64)ScanValue && ReadValue <= (UInt64)SecondScanValue)
                        return true;
                    break;
                case ScanCompareType.Increased_by_Value:
                    if (ReadValue == (UInt64)(StoredValue) + (UInt64)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Decreased_by_Value:
                    if (ReadValue == (UInt64)(StoredValue) - (UInt64)ScanValue)
                        return true;
                    break;
                case ScanCompareType.Unknown_Value:
                    return true;
            }
            return false;
        }


        private bool CompareValues(Single ReadValue, UInt64 StoredValue)
        {
            Int64 _ReadValue;
            if (Truncated)
                _ReadValue = (Int64)(ReadValue * SignificantDigits);
            else
                _ReadValue = (Int64)(Math.Round(ReadValue, (int)(Math.Log(SignificantDigits, 10) + 1), MidpointRounding.AwayFromZero) * SignificantDigits);


            switch (ScanCompareType)
            {
                case ScanCompareType.Exact_Value:
                    if (_ReadValue == _ScanValue)
                        return true;
                    break;
                case ScanCompareType.Not_Equal_to_Value:
                    if (_ReadValue != _ScanValue)
                        return true;
                    break;
                case ScanCompareType.Changed_Value: //
                    if (ReadValue != BitConverter.ToSingle(BitConverter.GetBytes(StoredValue), 0))
                        return true;
                    break;
                case ScanCompareType.Increased_Value: //
                    if (ReadValue > BitConverter.ToSingle(BitConverter.GetBytes(StoredValue), 0))
                        return true;
                    break;
                case ScanCompareType.Decreased_Value: //
                    if (ReadValue < BitConverter.ToSingle(BitConverter.GetBytes(StoredValue), 0))
                        return true;
                    break;
                case ScanCompareType.Unchanged_Value: //
                    if (ReadValue == BitConverter.ToSingle(BitConverter.GetBytes(StoredValue), 0))
                        return true;
                    break;
                case ScanCompareType.Less_Than:
                    if (_ReadValue < _ScanValue)
                        return true;
                    break;
                case ScanCompareType.Greater_Than:
                    if (_ReadValue > _ScanValue)
                        return true;
                    break;
                case ScanCompareType.Less_Than_or_Equal:
                    if (_ReadValue <= _ScanValue)
                        return true;
                    break;
                case ScanCompareType.Greater_Than_or_Equal:
                    if (_ReadValue >= _ScanValue)
                        return true;
                    break;
                case ScanCompareType.Between_Exclusive:
                    if (_ReadValue > _ScanValue && _ReadValue < (Single)SecondScanValue * SignificantDigits)
                        return true;
                    break;
                case ScanCompareType.Between_Inclusive:
                    if (_ReadValue >= _ScanValue && _ReadValue <= (Single)SecondScanValue * SignificantDigits)
                        return true;
                    break;
                case ScanCompareType.Increased_by_Value: //
                    if (_ReadValue == BitConverter.ToSingle(BitConverter.GetBytes(StoredValue), 0) * SignificantDigits + _ScanValue)
                        return true;
                    break;
                case ScanCompareType.Decreased_by_Value: //
                    if (_ReadValue == BitConverter.ToSingle(BitConverter.GetBytes(StoredValue), 0) * SignificantDigits - _ScanValue)
                        return true;
                    break;
                case ScanCompareType.Unknown_Value:
                    return true;
            }
            return false;
        }

        private bool CompareValues(Double ReadValue, UInt64 StoredValue)
        {
            Int64 _ReadValue;
            if (Truncated)
                _ReadValue = (Int64)(ReadValue * SignificantDigits);
            else
                _ReadValue = (Int64)(Math.Round(ReadValue, (int)(Math.Log(SignificantDigits, 10) + 1), MidpointRounding.AwayFromZero) * SignificantDigits);

            switch (ScanCompareType)
            {
                case ScanCompareType.Exact_Value:
                    if (_ReadValue == _ScanValue)
                        return true;
                    break;
                case ScanCompareType.Not_Equal_to_Value:
                    if (_ReadValue != _ScanValue)
                        return true;
                    break;
                case ScanCompareType.Changed_Value: //
                    if (ReadValue != BitConverter.Int64BitsToDouble((Int64)StoredValue))
                        return true;
                    break;
                case ScanCompareType.Increased_Value: //
                    //BitConverter.ToDouble(BitConverter.GetBytes(FoundValues[CompareIndex]), 0); //This also works
                    if (ReadValue > BitConverter.Int64BitsToDouble((Int64)StoredValue))
                        return true;
                    break;
                case ScanCompareType.Decreased_Value: //
                    if (ReadValue < BitConverter.Int64BitsToDouble((Int64)StoredValue))
                        return true;
                    break;
                case ScanCompareType.Unchanged_Value: //
                    if (ReadValue == BitConverter.Int64BitsToDouble((Int64)StoredValue))
                        return true;
                    break;
                case ScanCompareType.Less_Than:
                    if (_ReadValue < _ScanValue)
                        return true;
                    break;
                case ScanCompareType.Greater_Than:
                    if (_ReadValue > _ScanValue)
                        return true;
                    break;
                case ScanCompareType.Less_Than_or_Equal:
                    if (_ReadValue <= _ScanValue)
                        return true;
                    break;
                case ScanCompareType.Greater_Than_or_Equal:
                    if (_ReadValue >= _ScanValue)
                        return true;
                    break;
                case ScanCompareType.Between_Exclusive:
                    if (_ReadValue > _ScanValue && _ReadValue < (Double)SecondScanValue * SignificantDigits)
                        return true;
                    break;
                case ScanCompareType.Between_Inclusive:
                    if (_ReadValue >= _ScanValue && _ReadValue <= (Double)SecondScanValue * SignificantDigits)
                        return true;
                    break;
                case ScanCompareType.Increased_by_Value: //
                    if (_ReadValue == BitConverter.Int64BitsToDouble((Int64)StoredValue) * SignificantDigits + _ScanValue)
                        return true;
                    break;
                case ScanCompareType.Decreased_by_Value: //
                    if (_ReadValue == BitConverter.Int64BitsToDouble((Int64)StoredValue) * SignificantDigits - _ScanValue)
                        return true;
                    break;
                case ScanCompareType.Unknown_Value:
                    return true;
            }
            return false;
        }

        #endregion
    }

    #region EventArgs classes
    public class ScanProgressChangedEventArgs : EventArgs
    {
        public ScanProgressChangedEventArgs(int Progress)
        {
            progress = Progress;
        }
        private int progress;
        public int Progress
        {
            set
            {
                progress = value;
            }
            get
            {
                return progress;
            }
        }
    }

    public class ScanCompletedEventArgs : EventArgs
    {
        public Int64 TotalFoundSize;
        public ScanCompletedEventArgs(Int64 TotalFoundSize)
        {
            this.TotalFoundSize = TotalFoundSize;
        }
    }

    public class ScanCanceledEventArgs : EventArgs
    {
        public ScanCanceledEventArgs() { }
    }

    #endregion


    public struct ScanOptimizationData
    {
        public bool Enabled;
        public UInt64 Allignment;
        public UInt64 LastDigits;
        public bool IsLastDigit;

        public ScanOptimizationData(bool Enabled, UInt64 Value, bool IsLastDigit)
        {
            this.Enabled = Enabled;
            this.Allignment = Value;
            this.LastDigits = Value;
            this.IsLastDigit = IsLastDigit;
        }
    }

    public enum ScanCompareType
    {
        Exact_Value,
        Unchanged_Value,
        Changed_Value,
        Not_Equal_to_Value,
        Increased_Value,
        Decreased_Value,
        Increased_by_Value,
        Decreased_by_Value,
        Less_Than,
        Greater_Than,
        Less_Than_or_Equal,
        Greater_Than_or_Equal,
        Between_Exclusive,
        Between_Inclusive,
        Unknown_Value,
    }

}