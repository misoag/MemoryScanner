using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using Aecial.Conversions;

/// <summary>
/// Functions and invokes in order to manipulate memory
/// </summary>
/// 
namespace Aecial.MemoryScanner
{

    [Flags]
    public enum DataTypeSize
    {
        Byte = sizeof(Byte),
        SByte = sizeof(SByte),
        Int16 = sizeof(Int16),
        UInt16 = sizeof(UInt16),
        Int32 = sizeof(Int32),
        UInt32 = sizeof(UInt32),
        Int64 = sizeof(Int64),
        UInt64 = sizeof(UInt64),
        Single = sizeof(Single),
        Double = sizeof(Double),
    }



    #region ProcessMemoryReader class

    class ProcessMemoryEditor
    {

        public ProcessMemoryEditor()
        {
        }

        /// <summary>	
        /// Process from which to read		
        /// </summary>
        public Process ReadProcess
        {
            get
            {
                return m_ReadProcess;
            }
            set
            {
                m_ReadProcess = value;
            }
        }

        private Process m_ReadProcess = null;

        private IntPtr m_hProcess = IntPtr.Zero;

        public void OpenProcess()
        {
            //			m_hProcess = ProcessMemoryReaderApi.OpenProcess(ProcessMemoryReaderApi.PROCESS_VM_READ, 1, (uint)m_ReadProcess.Id);
            ProcessMemoryReaderApi.ProcessAccessType access;
            access = ProcessMemoryReaderApi.ProcessAccessType.PROCESS_VM_READ
                | ProcessMemoryReaderApi.ProcessAccessType.PROCESS_VM_WRITE
                | ProcessMemoryReaderApi.ProcessAccessType.PROCESS_VM_OPERATION;
            m_hProcess = ProcessMemoryReaderApi.OpenProcess((uint)access, 1, (uint)m_ReadProcess.Id);
            //m_hProcess = ZWOpen

        }
        public bool OpenProcess(string sProcessName)
        {
            Process[] aProcesses = Process.GetProcessesByName(sProcessName);
            if (aProcesses.Length > 0)
            {
                m_ReadProcess = aProcesses[0];
                if (m_ReadProcess.Handle != IntPtr.Zero)
                {
                    m_hProcess = m_ReadProcess.Handle;
                    return true;
                }
                else
                    return false;
            }
            return false;
        }

        public bool OpenProcess(int iProcessID)
        {
            m_ReadProcess = Process.GetProcessById(iProcessID);
            if (m_ReadProcess.Handle != IntPtr.Zero)
            {
                m_hProcess = m_ReadProcess.Handle;
                return true;
            }
            else
                return false;
        }

        public void CloseHandle()
        {
            try
            {
                int iRetValue;
                iRetValue = ProcessMemoryReaderApi.CloseHandle(m_hProcess);
                if (iRetValue == 0)
                {
                    throw new Exception("CloseHandle failed");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        //Modules
        private ProcessModule FindModule(string sModuleName)
        {
            for (int iModule = 0; iModule < m_ReadProcess.Modules.Count; iModule++)
            {
                if (m_ReadProcess.Modules[iModule].ModuleName == sModuleName)
                    return m_ReadProcess.Modules[iModule];
            }
            return null;
        }
        public ProcessModuleCollection GetModules()
        {
            return m_ReadProcess.Modules;
        }

        #region getInfo

        //Get Process Info
        public string Name()
        {
            return m_ReadProcess.ProcessName;
        }
        public int PID()
        {
            return m_ReadProcess.Id;
        }
        public int SID()
        {
            return m_ReadProcess.SessionId;
        }
        public string FileVersion()
        {
            return m_ReadProcess.MainModule.FileVersionInfo.FileVersion;
        }
        public string StartTime()
        {
            return m_ReadProcess.StartTime.ToString();
        }

        //Get Module Info
        public int BaseAddress()
        {
            return m_ReadProcess.MainModule.BaseAddress.ToInt32();
        }
        public int BaseAddress(string sModuleName)
        {
            return FindModule(sModuleName).BaseAddress.ToInt32();
        }
        public int EntryPoint()
        {
            return m_ReadProcess.MainModule.EntryPointAddress.ToInt32();
        }
        public int EntryPoint(string sModuleName)
        {
            return FindModule(sModuleName).EntryPointAddress.ToInt32();
        }
        public int MemorySize()
        {
            return m_ReadProcess.MainModule.ModuleMemorySize;
        }
        public int MemorySize(string sModuleName)
        {
            return FindModule(sModuleName).ModuleMemorySize;
        }
        #endregion

        #region WriteMemory
        //Write to Address
        public bool Write(UInt64 iMemoryAddress, byte bByteToWrite)
        {
            byte[] bBuffer = { bByteToWrite }; IntPtr ptrBytesWritten;
            ProcessMemoryReaderApi.WriteProcessMemory(m_hProcess, (IntPtr)iMemoryAddress, bBuffer, 1, out ptrBytesWritten);

            return (ptrBytesWritten.ToInt32() == 1);
        }
        public bool Write(UInt64 iMemoryAddress, short iShortToWrite)
        {
            byte[] bBuffer = BitConverter.GetBytes(iShortToWrite); IntPtr ptrBytesWritten;
            ProcessMemoryReaderApi.WriteProcessMemory(m_hProcess, (IntPtr)iMemoryAddress, bBuffer, 2, out ptrBytesWritten);

            return (ptrBytesWritten.ToInt32() == 2);
        }
        public bool Write(UInt64 iMemoryAddress, int iIntToWrite)
        {
            byte[] bBuffer = BitConverter.GetBytes(iIntToWrite); IntPtr ptrBytesWritten;
            ProcessMemoryReaderApi.WriteProcessMemory(m_hProcess, (IntPtr)iMemoryAddress, bBuffer, 4, out ptrBytesWritten);

            return (ptrBytesWritten.ToInt32() == 4);
        }
        public bool Write(UInt64 iMemoryAddress, long iLongToWrite)
        {
            byte[] bBuffer = BitConverter.GetBytes(iLongToWrite); IntPtr ptrBytesWritten;
            ProcessMemoryReaderApi.WriteProcessMemory(m_hProcess, (IntPtr)iMemoryAddress, bBuffer, 8, out ptrBytesWritten);

            return (ptrBytesWritten.ToInt32() == 8);
        }
        public bool Write(UInt64 iMemoryAddress, float iSingleToWrite)
        {
            byte[] bBuffer = BitConverter.GetBytes(iSingleToWrite); IntPtr ptrBytesWritten;
            ProcessMemoryReaderApi.WriteProcessMemory(m_hProcess, (IntPtr)iMemoryAddress, bBuffer, 4, out ptrBytesWritten);

            return (ptrBytesWritten.ToInt32() == 4);
        }
        public bool Write(UInt64 iMemoryAddress, double iDoubleToWrite)
        {
            byte[] bBuffer = BitConverter.GetBytes(iDoubleToWrite); IntPtr ptrBytesWritten;
            ProcessMemoryReaderApi.WriteProcessMemory(m_hProcess, (IntPtr)iMemoryAddress, bBuffer, 8, out ptrBytesWritten);

            return (ptrBytesWritten.ToInt32() == 8);
        }
        public bool Write(UInt64 iMemoryAddress, string sStringToWrite, int iMode = 0)
        {
            byte[] bBuffer = { 0 }; IntPtr ptrBytesWritten;

            if (iMode == 0)
                bBuffer = CreateAOBText(sStringToWrite);
            else if (iMode == 1)
                bBuffer = ReverseBytes(CreateAOBString(sStringToWrite));

            ProcessMemoryReaderApi.WriteProcessMemory(m_hProcess, (IntPtr)iMemoryAddress, bBuffer, (uint)bBuffer.Length, out ptrBytesWritten);

            return (ptrBytesWritten.ToInt32() == bBuffer.Length);
        }
        public bool Write(UInt64 iMemoryAddress, byte[] bBytesToWrite)
        {
            IntPtr ptrBytesWritten;
            ProcessMemoryReaderApi.WriteProcessMemory(m_hProcess, (IntPtr)iMemoryAddress, bBytesToWrite, (uint)bBytesToWrite.Length, out ptrBytesWritten);

            return (ptrBytesWritten.ToInt32() == bBytesToWrite.Length);
        }
        public void Write(IntPtr MemoryAddress, byte[] bytesToWrite, out int bytesWritten)
        {
            IntPtr ptrBytesWritten;
            ProcessMemoryReaderApi.WriteProcessMemory(m_hProcess, MemoryAddress, bytesToWrite, (uint)bytesToWrite.Length, out ptrBytesWritten);

            bytesWritten = ptrBytesWritten.ToInt32();
        }

        public bool NOP(UInt64 iMemoryAddress, int iLength)
        {
            byte[] bBuffer = new byte[iLength]; IntPtr ptrBytesWritten;
            for (int i = 0; i < iLength; i++)
                bBuffer[i] = 0x90;

            ProcessMemoryReaderApi.WriteProcessMemory(m_hProcess, (IntPtr)iMemoryAddress, bBuffer, (uint)iLength, out ptrBytesWritten);
            return (ptrBytesWritten.ToInt32() == iLength);
        }
        #endregion

        #region ReadMemory
        //Read from Address
        public Byte ReadByte(UInt64 iMemoryAddress)
        {
            byte[] bBuffer = new byte[1]; IntPtr ptrBytesRead;
            int iReturn = ProcessMemoryReaderApi.ReadProcessMemory(m_hProcess, (IntPtr)iMemoryAddress, bBuffer, 1, out ptrBytesRead);

            if (iReturn == 0)
                return 0;
            else
                return bBuffer[0];
        }
        public SByte ReadSByte(UInt64 iMemoryAddress)
        {
            byte[] bBuffer = new byte[1]; IntPtr ptrBytesRead;
            int iReturn = ProcessMemoryReaderApi.ReadProcessMemory(m_hProcess, (IntPtr)iMemoryAddress, bBuffer, 1, out ptrBytesRead);

            if (iReturn == 0)
                return 0;
            else
                return (SByte)bBuffer[0];
        }
        public UInt16 ReadUInt16(UInt64 iMemoryAddress)
        {
            byte[] bBuffer = new byte[2]; IntPtr ptrBytesRead;
            int iReturn = ProcessMemoryReaderApi.ReadProcessMemory(m_hProcess, (IntPtr)iMemoryAddress, bBuffer, 2, out ptrBytesRead);

            if (iReturn == 0)
                return 0;
            else
                return BitConverter.ToUInt16(bBuffer, 0);
        }
        public Int16 ReadInt16(UInt64 iMemoryAddress)
        {
            byte[] bBuffer = new byte[2]; IntPtr ptrBytesRead;
            int iReturn = ProcessMemoryReaderApi.ReadProcessMemory(m_hProcess, (IntPtr)iMemoryAddress, bBuffer, 2, out ptrBytesRead);

            if (iReturn == 0)
                return 0;
            else
                return BitConverter.ToInt16(bBuffer, 0);
        }

        public UInt32 ReadUInt32(UInt64 iMemoryAddress)
        {
            byte[] bBuffer = new byte[4]; IntPtr ptrBytesRead;
            int iReturn = ProcessMemoryReaderApi.ReadProcessMemory(m_hProcess, (IntPtr)iMemoryAddress, bBuffer, 4, out ptrBytesRead);

            if (iReturn == 0)
                return 0;
            else
                return BitConverter.ToUInt32(bBuffer, 0);
        }
        public Int32 ReadInt32(UInt64 iMemoryAddress)
        {
            byte[] bBuffer = new byte[4]; IntPtr ptrBytesRead;
            int iReturn = ProcessMemoryReaderApi.ReadProcessMemory(m_hProcess, (IntPtr)iMemoryAddress, bBuffer, 4, out ptrBytesRead);

            if (iReturn == 0)
                return 0;
            else
                return BitConverter.ToInt32(bBuffer, 0);
        }
        public UInt64 ReadUInt64(UInt64 iMemoryAddress)
        {
            byte[] bBuffer = new byte[8]; IntPtr ptrBytesRead;
            int iReturn = ProcessMemoryReaderApi.ReadProcessMemory(m_hProcess, (IntPtr)iMemoryAddress, bBuffer, 8, out ptrBytesRead);

            if (iReturn == 0)
                return 0;
            else
                return BitConverter.ToUInt64(bBuffer, 0);
        }
        public Int64 ReadInt64(UInt64 iMemoryAddress)
        {
            byte[] bBuffer = new byte[8]; IntPtr ptrBytesRead;
            int iReturn = ProcessMemoryReaderApi.ReadProcessMemory(m_hProcess, (IntPtr)iMemoryAddress, bBuffer, 8, out ptrBytesRead);

            if (iReturn == 0)
                return 0;
            else
                return BitConverter.ToInt64(bBuffer, 0);
        }
        public float ReadSingle(UInt64 iMemoryAddress)
        {
            byte[] bBuffer = new byte[4]; IntPtr ptrBytesRead;
            int iReturn = ProcessMemoryReaderApi.ReadProcessMemory(m_hProcess, (IntPtr)iMemoryAddress, bBuffer, 4, out ptrBytesRead);

            if (iReturn == 0)
                return 0;
            else
                return BitConverter.ToSingle(bBuffer, 0);
        }

        public double ReadDouble(UInt64 iMemoryAddress)
        {
            byte[] bBuffer = new byte[8]; IntPtr ptrBytesRead;
            int iReturn = ProcessMemoryReaderApi.ReadProcessMemory(m_hProcess, (IntPtr)iMemoryAddress, bBuffer, 8, out ptrBytesRead);

            if (iReturn == 0)
                return 0;
            else
                return BitConverter.ToDouble(bBuffer, 0);
        }
        public String ReadString(UInt64 iMemoryAddress, uint iTextLength, int iMode = 0)
        {
            byte[] bBuffer = new byte[iTextLength]; IntPtr ptrBytesRead;
            ProcessMemoryReaderApi.ReadProcessMemory(m_hProcess, (IntPtr)iMemoryAddress, bBuffer, iTextLength, out ptrBytesRead);

            if (iMode == 0)
                return Encoding.UTF8.GetString(bBuffer);
            else if (iMode == 1)
                return BitConverter.ToString(bBuffer).Replace("-", "");
            else
                return "";
        }
        public Byte[] ReadAOB(IntPtr iMemoryAddress, uint iBytesToRead, out int bytesRead)
        {
            byte[] bBuffer = new byte[iBytesToRead];
            IntPtr ptrBytesRead;
            ProcessMemoryReaderApi.ReadProcessMemory(m_hProcess, iMemoryAddress, bBuffer, iBytesToRead, out ptrBytesRead);
            bytesRead = ptrBytesRead.ToInt32();
            return bBuffer;
        }
        public Byte[] ReadAOB(IntPtr iMemoryAddress, uint iBytesToRead)
        {
            byte[] bBuffer = new byte[iBytesToRead];
            IntPtr ptrBytesRead;
            ProcessMemoryReaderApi.ReadProcessMemory(m_hProcess, iMemoryAddress, bBuffer, iBytesToRead, out ptrBytesRead);
            return bBuffer;
        }

        #endregion

        public unsafe static void VirtualQueryEx(IntPtr processHandle, IntPtr lpAddress, out ProcessMemoryReaderApi.MEMORY_BASIC_INFORMATION64 lpBuffer, uint dwLength)
        {
            bool Is32Bit;

            //First check if we are a 32 bit OS, in which case target process cannot be 64 bit
            if (sizeof(IntPtr) == (int)DataTypeSize.Int32)
                Is32Bit = true;
            else
                ProcessMemoryReaderApi.IsWow64Process(processHandle, out Is32Bit);

            if (!Is32Bit)
                ProcessMemoryReaderApi.VirtualQueryEx(processHandle, lpAddress, out lpBuffer, dwLength);
            else
            {
                ProcessMemoryReaderApi.MEMORY_BASIC_INFORMATION32 lpBuffer32;
                ProcessMemoryReaderApi.VirtualQueryEx(processHandle.ToInt32(), lpAddress.ToInt32(), out lpBuffer32, dwLength);

                lpBuffer.AllocationBase = lpBuffer32.AllocationBase;
                lpBuffer.AllocationProtect = lpBuffer32.AllocationProtect;
                lpBuffer.BaseAddress = lpBuffer32.BaseAddress;
                lpBuffer.lType = lpBuffer32.Type;
                lpBuffer.Protect = lpBuffer32.Protect;
                lpBuffer.RegionSize = (UInt64)lpBuffer32.RegionSize;
                lpBuffer.State = lpBuffer32.State;
                lpBuffer.__alignment1 = 0;
                lpBuffer.__alignment2 = 0;
            }
        }

        #region Misc
        //Miscellaneous
        public bool BytesEqual(byte[] bBytes_1, byte[] bBytes_2)
        {
            return (BitConverter.ToString(bBytes_1) == BitConverter.ToString(bBytes_2));
        }
        public bool IsNumeric(string sNumber)
        {
            return new Regex(@"^\d+$").IsMatch(sNumber);
        }
        public byte[] ReverseBytes(byte[] bOriginalBytes)
        {
            int iBytes = bOriginalBytes.Length; byte[] bNewBytes = new byte[iBytes];

            for (int i = 0; i < iBytes; i++)
            {
                bNewBytes[iBytes - i - 1] = bOriginalBytes[i];
            }
            return bNewBytes;
        }
        //Miscellaneous Conversions
        public byte[] CreateAOBText(string sBytes)
        {
            return Encoding.ASCII.GetBytes(sBytes);
        }
        public string CreateAOBText(byte[] bBytes)
        {
            return System.Text.Encoding.ASCII.GetString(bBytes);
        }
        public byte[] CreateAOBString(string sBytes)
        {
            return BitConverter.GetBytes(Conversions.Conversions.HexToInt((sBytes)));
        }
        public string CreateAddress(byte[] bBytes)
        {
            string sAddress = "";

            for (int i = 0; i < bBytes.Length; i++)
            {
                if (Convert.ToInt16(bBytes[i]) < 10)
                    sAddress = "0" + bBytes[i].ToString("X") + sAddress;
                else
                    sAddress = bBytes[i].ToString("X") + sAddress;
            }

            return sAddress;
        }

        //Calculate Pointer
        public int CalculatePointer(int iMemoryAddress, int[] iOffsets)
        {
            int iPointerCount = iOffsets.Length - 1; IntPtr ptrBytesRead; byte[] bBuffer = new byte[4]; int iTemporaryAddress = 0;

            if (iPointerCount == 0)
                iTemporaryAddress = iMemoryAddress;

            for (int i = 0; i <= iPointerCount; i++)
            {
                if (i == iPointerCount)
                {
                    ProcessMemoryReaderApi.ReadProcessMemory(m_hProcess, (IntPtr)iTemporaryAddress, bBuffer, 4, out ptrBytesRead);
                    iTemporaryAddress = Conversions.Conversions.HexToInt(CreateAddress(bBuffer)) + iOffsets[i];
                    return iTemporaryAddress;
                }
                else if (i == 0)
                {
                    ProcessMemoryReaderApi.ReadProcessMemory(m_hProcess, (IntPtr)iMemoryAddress, bBuffer, 4, out ptrBytesRead);
                    iTemporaryAddress = Conversions.Conversions.HexToInt(CreateAddress(bBuffer)) + iOffsets[0];
                }
                else
                {
                    ProcessMemoryReaderApi.ReadProcessMemory(m_hProcess, (IntPtr)iTemporaryAddress, bBuffer, 4, out ptrBytesRead);
                    iTemporaryAddress = Conversions.Conversions.HexToInt(CreateAddress(bBuffer)) + iOffsets[i];
                }
            }
            return 0;
        }

        //Calculate Static Address
        public int CalculateStaticAddress(string sStaticOffset)
        {
            return BaseAddress() + Conversions.Conversions.HexToInt(sStaticOffset);
        }
        public int CalculateStaticAddress(int iStaticOffset)
        {
            return BaseAddress() + iStaticOffset;
        }
        public int CalculateStaticAddress(string sStaticOffset, string sModuleName)
        {
            return BaseAddress(sModuleName) + Conversions.Conversions.HexToInt(sStaticOffset);
        }
        public int CalculateStaticAddress(int iStaticOffset, string sModuleName)
        {
            return BaseAddress(sModuleName) + iStaticOffset;
        }


        #endregion

        /// <summary>
        /// ProcessMemoryReader is a class that enables direct reading a process memory
        /// </summary>
        public class ProcessMemoryReaderApi
        {
            // constants information can be found in <winnt.h>
            [Flags]
            public enum ProcessAccessType
            {
                PROCESS_TERMINATE = (0x0001),
                PROCESS_CREATE_THREAD = (0x0002),
                PROCESS_SET_SESSIONID = (0x0004),
                PROCESS_VM_OPERATION = (0x0008),
                PROCESS_VM_READ = (0x0010),
                PROCESS_VM_WRITE = (0x0020),
                PROCESS_DUP_HANDLE = (0x0040),
                PROCESS_CREATE_PROCESS = (0x0080),
                PROCESS_SET_QUOTA = (0x0100),
                PROCESS_SET_INFORMATION = (0x0200),
                PROCESS_QUERY_INFORMATION = (0x0400)
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct MEMORY_BASIC_INFORMATION64
            {
                public UInt64 BaseAddress;
                public UInt64 AllocationBase;
                public UInt32 AllocationProtect;
                public UInt32 __alignment1;
                public UInt64 RegionSize;
                public UInt32 State;
                public UInt32 Protect;
                public UInt32 lType;
                public UInt32 __alignment2;
            };

            [StructLayout(LayoutKind.Sequential)]
            public struct MEMORY_BASIC_INFORMATION32
            {
                public UInt64 BaseAddress;
                public UInt64 AllocationBase;
                public UInt32 AllocationProtect;
                public UInt64 RegionSize;
                public UInt32 State;
                public UInt32 Protect;
                public UInt32 Type;
            };

            [Flags]
            public enum MEMORY_STATE : int
            {
                COMMIT = 0x1000,
                FREE = 0x10000,
                RESERVE = 0x2000,
                RESET_UNDO = 0x1000000
            }

            [Flags]
            public enum MEMORY_TYPE : int
            {
                IMAGE = 0x1000000,
                MAPPED = 0x40000,
                PRIVATE = 0x20000
            }

            [Flags]
            public enum MemoryProtection : uint
            {
                PAGE_EXECUTE = 0x00000010,
                PAGE_EXECUTE_READ = 0x00000020,
                PAGE_EXECUTE_READWRITE = 0x00000040,
                PAGE_EXECUTE_WRITECOPY = 0x00000080,
                PAGE_NOACCESS = 0x00000001,
                PAGE_READONLY = 0x00000002,
                PAGE_READWRITE = 0x00000004,
                PAGE_WRITECOPY = 0x00000008,
                PAGE_GUARD = 0x00000100,
                PAGE_NOCACHE = 0x00000200,
                PAGE_WRITECOMBINE = 0x00000400
            }

            // function declarations are found in the MSDN and in <winbase.h> 

            //		HANDLE OpenProcess(
            //			DWORD dwDesiredAccess,  // access flag
            //			BOOL bInheritHandle,    // handle inheritance option
            //			DWORD dwProcessId       // process identifier
            //			);
            [DllImport("kernel32.dll")]
            public static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, Int32 bInheritHandle, UInt32 dwProcessId);

            //		BOOL CloseHandle(
            //			HANDLE hObject   // handle to object
            //			);
            [DllImport("kernel32.dll")]
            public static extern Int32 CloseHandle(IntPtr hObject);

            //		BOOL ReadProcessMemory(
            //			HANDLE hProcess,              // handle to the process
            //			LPCVOID lpBaseAddress,        // base of memory area
            //			LPVOID lpBuffer,              // data buffer
            //			SIZE_T nSize,                 // number of bytes to read
            //			SIZE_T * lpNumberOfBytesRead  // number of bytes read
            //			);
            [DllImport("kernel32.dll")]
            public static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesRead);

            //		BOOL WriteProcessMemory(
            //			HANDLE hProcess,                // handle to process
            //			LPVOID lpBaseAddress,           // base of memory area
            //			LPCVOID lpBuffer,               // data buffer
            //			SIZE_T nSize,                   // count of bytes to write
            //			SIZE_T * lpNumberOfBytesWritten // count of bytes written
            //			);
            [DllImport("kernel32.dll")]
            public static extern Int32 WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesWritten);

            [DllImport("kernel32.dll")]
            public static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION64 lpBuffer, uint dwLength);
            [DllImport("kernel32.dll")]
            public static extern int VirtualQueryEx(Int32 hProcess, Int32 lpAddress, out MEMORY_BASIC_INFORMATION32 lpBuffer, uint dwLength);

            [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool IsWow64Process([In] IntPtr processHandle,
                 [Out, MarshalAs(UnmanagedType.Bool)] out bool wow64Process);
        }
    }

    #endregion
}