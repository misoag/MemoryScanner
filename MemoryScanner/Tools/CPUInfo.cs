using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Management;

namespace AecialEngine.Tools
{
    public partial class CPUInfo : Form
    {

        #region Variables

        //Cnstants used for processor types
        public const int PROCESSOR_INTEL_386 = 386;
        public const int PROCESSOR_INTEL_486 = 486;
        public const int PROCESSOR_INTEL_PENTIUM = 586;
        public const int PROCESSOR_MIPS_R4000 = 4000;
        public const int PROCESSOR_ALPHA_21064 = 21064;

        #endregion

        #region Init / Events / Functions

        public CPUInfo()
        {
            InitializeComponent();
        }

        private void CPUInfo_Load(object sender, EventArgs e)
        {
            GetInfo();
        }

      private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetInfo();
        }

        private void GetInfo()
        {
            cpuListView.Items.Clear();

            cpuListView.SuspendLayout();
            cpuListView.BeginUpdate();

            try
            {
                ManagementObjectCollection mbsList = null;
                ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select * From Win32_processor");
                mbsList = mbs.Get();
                foreach (ManagementObject mo in mbsList)
                    AddItemToList("Processor ID", mo["ProcessorID"].ToString());

                ManagementObject dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""c:""");
                dsk.Get();
                AddItemToList("Hard Drive ID", dsk["VolumeSerialNumber"].ToString());

                ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
                ManagementObjectCollection moc = mos.Get();
                foreach (ManagementObject mo in moc)
                    AddItemToList("MotherboardID", mo["SerialNumber"].ToString());

                SYSTEM_INFO pSI = new SYSTEM_INFO();
                GetSystemInfo(ref pSI);
                string CPUType;
                switch (pSI.dwProcessorType)
                {

                    case PROCESSOR_INTEL_386:
                        CPUType = "Intel 386";
                        break;
                    case PROCESSOR_INTEL_486:
                        CPUType = "Intel 486";
                        break;
                    case PROCESSOR_INTEL_PENTIUM:
                        CPUType = "Intel Pentium";
                        break;
                    case PROCESSOR_MIPS_R4000:
                        CPUType = "MIPS R4000";
                        break;
                    case PROCESSOR_ALPHA_21064:
                        CPUType = "DEC Alpha 21064";
                        break;
                    default:
                        CPUType = pSI.dwProcessorType.ToString();
                        break;
                }
                AddItemToList("Active Processor Mask", pSI.dwActiveProcessorMask.ToString());
                AddItemToList("Allocation Granularity", pSI.dwAllocationGranularity.ToString());
                AddItemToList("Number Of Processors", pSI.dwNumberOfProcessors.ToString());
                AddItemToList("OEM ID", pSI.dwOemId.ToString());
                AddItemToList("Page Size", pSI.dwPageSize.ToString());
                // Processor Level (Req filtering to get level)
                AddItemToList("Processor Level Value", pSI.dwProcessorLevel.ToString());
                AddItemToList("Processor Revision", pSI.dwProcessorRevision.ToString());
                AddItemToList("CPU type", CPUType);
                AddItemToList("Minimum Application Address", pSI.lpMinimumApplicationAddress.ToString());
                AddItemToList("Maximum Application Address", pSI.lpMaximumApplicationAddress.ToString());

                /**************	To retrive info from GlobalMemoryStatus ****************/

                MEMORYSTATUS memSt = new MEMORYSTATUS();
                GlobalMemoryStatus(ref memSt);

                AddItemToList("Available Page File", (memSt.dwAvailPageFile / 1024).ToString());
                AddItemToList("Available Physical Memory", (memSt.dwAvailPhys / 1024).ToString());
                AddItemToList("Available Virtual Memory", (memSt.dwAvailVirtual / 1024).ToString());
                AddItemToList("Size of structure", memSt.dwLength.ToString());
                AddItemToList("Memory In Use", memSt.dwMemoryLoad.ToString());
                AddItemToList("Total Page Size", (memSt.dwTotalPageFile / 1024).ToString());
                AddItemToList("Total Physical Memory", (memSt.dwTotalPhys / 1024).ToString());
                AddItemToList("Total Virtual Memory", (memSt.dwTotalVirtual / 1024).ToString());

                if (System.IntPtr.Size == 4)
                    AddItemToList("Operating System", "32 bit");
                else if (System.IntPtr.Size == 8)
                    AddItemToList("Operating System", "64 bit");
                else
                    AddItemToList("Operating System", "Unknown");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }

            cpuListView.EndUpdate();
            cpuListView.ResumeLayout();
        }

        string[] addInfo = new string[2];
        private void AddItemToList(string Title, string Info)
        {
            addInfo[0] = Title;
            addInfo[1] = Info;
            cpuListView.Items.Add(addInfo[0]).SubItems.Add(addInfo[1]);
        }

        #endregion

        #region Structures

        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEM_INFO
        {
            public uint dwOemId;
            public uint dwPageSize;
            public uint lpMinimumApplicationAddress;
            public uint lpMaximumApplicationAddress;
            public uint dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public uint dwProcessorLevel;
            public uint dwProcessorRevision;
        }
        //struct to retrive memory status
        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORYSTATUS
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public uint dwTotalPhys;
            public uint dwAvailPhys;
            public uint dwTotalPageFile;
            public uint dwAvailPageFile;
            public uint dwTotalVirtual;
            public uint dwAvailVirtual;
        }

        #endregion

        #region P/Invokes

        //To get system information
        [DllImport("kernel32")]
        static extern void GetSystemInfo(ref SYSTEM_INFO pSI);

        //To get Memory status
        [DllImport("kernel32")]
        static extern void GlobalMemoryStatus(ref MEMORYSTATUS buf);
        #endregion

    }
}