using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using Aecial.MemoryScanner;
using Aecial.Conversions;

/// <summary>
/// A list of memory pages where addresses are stored
/// </summary>
/// 
namespace AecialEngine
{
    public partial class MemoryRegions : Form
    {

        #region Main function

        private IntPtr ProcessHandle;
        public MemoryRegions(IntPtr ProcessHandle)
        {
            InitializeComponent();
            this.ProcessHandle = ProcessHandle;

            if (ProcessHandle != null)
                LoadMemoryRegions();
        }

        public void UpdateProcessHandle(IntPtr ProcessHandle)
        {
            this.ProcessHandle = ProcessHandle;
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadMemoryRegions();
        }

        private void LoadMemoryRegions()
        {
            MemoryRegionsView.Items.Clear();

            long address = 0;
            long previous = 0;
            long lastAddress = 0xFFFFFFFF;

            string first = "s0";
            string[] addInfo = { "s1", "s2", "s3", "s4", "s5" };

            //Use VirtualQueryEx to find memory pages
            do
            {
                //Create instance of virtualqueryex & Gather data
                Aecial.MemoryScanner.ProcessMemoryEditor.ProcessMemoryReaderApi.MEMORY_BASIC_INFORMATION64 m =
                    new Aecial.MemoryScanner.ProcessMemoryEditor.ProcessMemoryReaderApi.MEMORY_BASIC_INFORMATION64();

                //Call our own VirtualQueryEx function (differentiates between 32 & 64 bit targets)
                ProcessMemoryEditor.VirtualQueryEx(ProcessHandle,
                    (IntPtr)address, out m, (uint)Marshal.SizeOf(m)); //stores result in m

                previous = address;
                address = (long)((uint)m.BaseAddress + (uint)m.RegionSize);

                //Exit when finished
                if (previous >= address || address >= (long)lastAddress)
                    break;

                //Get string that corresponds with the value found
                string allocProtect;
                switch (m.AllocationProtect)
                {
                    case 0x00:
                        allocProtect = "";
                        break;
                    case 0x01:
                        allocProtect = "No Access";
                        //Execute:N Read:N Write:N
                        break;
                    case 0x02:
                        allocProtect = "Read Only";
                        //Execute:N Read:Y Write:N
                        break;
                    case 0x04:
                        allocProtect = "Read+Write";
                        //Execute:N Read:Y Write:Y
                        break;
                    case 0x08:
                        allocProtect = "Write Copy";
                        //Execute:N Read:Y Write:N WriteCopy:Y
                        break;
                    case 0x10:
                        allocProtect = "Execute";
                        //Execute:Y Read:N Write:N
                        break;
                    case 0x20:
                        allocProtect = "Execute+Read";
                        //Execute:Y Read:Y Write:N
                        break;
                    case 0x40:
                        allocProtect = "Execute+Read+Write";
                        //Execute:Y Read:Y Write:Y
                        break;
                    case 0x80:
                        allocProtect = "Execute+Write Copy";
                        //Execute:Y Read:Y Write:N WriteCopy:Y
                        break;
                    default:
                        allocProtect = "Unknown";
                        break;
                }

                string state;
                switch (m.State)
                {
                    case 0x1000:
                        state = "Commit";
                        break;
                    case 0x2000:
                        state = "Reserve";
                        break;
                    case 0x10000:
                        state = "Free";
                        break;
                    default:
                        state = m.State.ToString();
                        break;
                }

                string type;
                switch (m.lType)
                {
                    case 0x20000:
                        type = "Private";
                        break;
                    case 0x40000:
                        type = "Mapped";
                        break;
                    case 0x1000000:
                        type = "Image";
                        break;
                    default:
                        type = "";
                        break;
                }

                string protect;
                switch (m.Protect)
                {

                    case 0x00:
                        protect = "";
                        break;
                    case 0x01:
                        protect = "No Access";
                        //Execute:N Read:N Write:N
                        break;
                    case 0x02:
                        protect = "Read Only";
                        //Execute:N Read:Y Write:N
                        break;
                    case 0x04:
                        protect = "Read+Write";
                        //Execute:N Read:Y Write:Y
                        break;
                    case 0x08:
                        protect = "Write Copy";
                        //Execute:N Read:Y Write:N WriteCopy:Y
                        break;
                    case 0x10:
                        protect = "Execute";
                        //Execute:Y Read:N Write:N
                        break;
                    case 0x20:
                        protect = "Execute+Read";
                        //Execute:Y Read:Y Write:N
                        break;
                    case 0x40:
                        protect = "Execute+Read+Write";
                        //Execute:Y Read:Y Write:Y
                        break;
                    case 0x80:
                        protect = "Execute+Write Copy";
                        //Execute:Y Read:Y Write:N WriteCopy:Y
                        break;
                    case 0x104:
                        protect = "Read+Write+Guard";
                        break;
                    default:
                        protect = "Unknown";
                        break;
                }


                //Add data to string
                first = Conversions.ToAddress(m.BaseAddress.ToString()) +
                   " - " + Conversions.ToAddress(Convert.ToString((UInt64)m.BaseAddress +
                   m.RegionSize));
                addInfo[0] = allocProtect;
                addInfo[1] = state;
                addInfo[2] = protect;
                addInfo[3] = type;
                addInfo[4] = Conversions.ToHex(m.RegionSize.ToString());

                //Add all data in string to the next item in list
                MemoryRegionsView.Items.Add(first).SubItems.AddRange(addInfo);

            } while (true);
        }

        #endregion

        private void MemoryRegions_Load(object sender, EventArgs e)
        {

        }

    }
}