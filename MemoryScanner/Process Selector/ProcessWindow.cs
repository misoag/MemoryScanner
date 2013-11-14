using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Aecial.Conversions;

/*
 * Perhaps look into effective icon grabbing methods -- resource
 * editors seem to be adept at grabbing icons -- however their
 * methods may be too slow for our intentions
 * 
 * Also note there are many implemented icon extraction methods but only
 * one is currently being utilized.
 * 
 * Further testing is needed to determine if our one method isn't
 * effectively grabbing icons
 */

/// <summary>
/// Used to select a running process to modify
/// </summary>
namespace AecialEngine
{
    public partial class ProcessWindow : Form
    {
        #region Variables

        private List<Process> ProcessList;
        public Process TargetProcess;
        private int[] IDsList;

        #endregion

        #region Initialization / Functions

        public ProcessWindow()
        {
            InitializeComponent();
        }

        private void ProcessWindow_Load(object sender, EventArgs e)
        {
            UpdateProcessInfo();
            SetToolTips();
        }

        private void SetToolTips()
        {
            GUIToolTip.SetToolTip(AcceptProcessButton, "Sets selected process as the target process.");
            GUIToolTip.SetToolTip(RefreshButton, "Clears list and searches again.");
            GUIToolTip.SetToolTip(CloseProcessButton, "Closes this window and leaves target process unchanged.");
        }

        private unsafe void UpdateProcessInfo()
        {
            ProcessListView.SuspendLayout();
            ProcessListView.BeginUpdate();
            ProcessListView.Items.Clear();

            ProcessList = new List<Process>(Process.GetProcesses());
            ProcessList.Sort(ProcessComparer.Reverse);
            IDsList = new int[ProcessList.Count];
            ImageList ImageListSmall = new ImageList();
            Icon Ico;
            int ImageCount = 0;

            for (int ecx = 0; ecx < ProcessList.Count; ecx++)
            {
                Ico = null;

                try
                {
                    //Grabbing icons from files in sys32 is disallowed (depending on icon grab
                    //method) for security purposes, so don't bother trying
                    if (!ProcessList[ecx].MainModule.FileName.Contains("system32"))
                    {
                        //Try to grab icon
                        Ico = GetIcon(ProcessList[ecx].MainModule.FileName, 0); //Fast
                        //Ico = ExtractIconFromExe(ProcessList[ecx].MainModule.FileName, false); //Medium
                        //Ico = SHGetFileIcon(ProcessList[ecx].MainModule.FileName, 0, false); //Fast
                    }
                }
                catch { }

                if (ProcessList[ecx].MainWindowTitle != "")
                {
                    //Include title window name
                    ProcessListView.Items.Add(
                        Conversions.ToAddress(Convert.ToString(ProcessList[ecx].Id)) + " - " +
                        ProcessList[ecx].ProcessName + " - (" + ProcessList[ecx].MainWindowTitle + ")");
                }
                else
                {
                    //No name, just add process name
                    ProcessListView.Items.Add(
                        Conversions.ToAddress(Convert.ToString(ProcessList[ecx].Id)) + " - " +
                        ProcessList[ecx].ProcessName);
                }

                IDsList[ecx] = ProcessList[ecx].Id;
                if (Ico != null)
                {
                    //Add the new Icon to the list
                    ImageListSmall.Images.Add(Ico);
                    //Set the image index to the corresponding image
                    ProcessListView.Items[ecx].ImageIndex = ImageCount++;
                }
            }

            ProcessListView.SmallImageList = ImageListSmall;
            ProcessListView.ResumeLayout();
            ProcessListView.EndUpdate();
        }

        public unsafe static Icon ExtractIconFromExe(string file, bool large)
        {
            int readIconCount = 0;
            IntPtr[] hDummy = new IntPtr[1] { IntPtr.Zero };
            IntPtr[] hIconEx = new IntPtr[1] { IntPtr.Zero };

            try
            {
                if (large)
                    readIconCount = (int)Icons.ExtractIconEx(file, 0, hIconEx, hDummy, 1);
                else
                    readIconCount = (int)Icons.ExtractIconEx(file, 0, hDummy, hIconEx, 1);

                if (readIconCount > 0 && hIconEx[0] != IntPtr.Zero)
                {
                    //Get first extracted icon
                    Icon extractedIcon = (Icon)Icon.FromHandle(hIconEx[0]).Clone();

                    return extractedIcon;
                }
                else //No icons found
                    return null;
            }
            catch (Exception ex)
            {
                //Error extracting icon
                throw new ApplicationException("Could not extract icon", ex);
            }
            finally
            {
                //Release resources
                foreach (IntPtr ptr in hIconEx)
                    if (ptr != IntPtr.Zero)
                        Icons.DestroyIcon(ptr);

                foreach (IntPtr ptr in hDummy)
                    if (ptr != IntPtr.Zero)
                        Icons.DestroyIcon(ptr);
            }
        }

        private Icon GetIcon(string fileName, int iconID)
        {
            //if (System.IO.File.Exists(fileName))
            try
            {
                IntPtr hc = Icons.ExtractIcon(this.Handle, fileName, iconID);
                if (!hc.Equals(IntPtr.Zero))
                    return (Icon.FromHandle(hc));
            }
            catch { }

            return null;
        }

        private Icon SHGetFileIcon(string fileName, int iconID, bool large)
        {
            Icons.SHFILEINFO info = new Icons.SHFILEINFO();
            int cbFileInfo = Marshal.SizeOf(info);
            Icons.SHGFI flags;
            if (large)
                flags = Icons.SHGFI.Icon | Icons.SHGFI.LargeIcon | Icons.SHGFI.UseFileAttributes;
            else
                flags = Icons.SHGFI.Icon | Icons.SHGFI.SmallIcon | Icons.SHGFI.UseFileAttributes;

            Icons.SHGetFileInfo(fileName, 0x00000100, out info, (uint)cbFileInfo, flags);
            return Icon.FromHandle(info.hIcon);
        }

        #endregion

        #region Events

        private void refreshButton_Click(object sender, EventArgs e)
        {
            UpdateProcessInfo();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateProcessInfo();
        }

        private void acceptProcessButton_Click(object sender, EventArgs e)
        {
            //Make selection via accept button
            MakeSelection();
        }

        private void processListView_DoubleClick(object sender, EventArgs e)
        {
            //Make selection via double click
            MakeSelection();
        }


        private void openProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Make selection via right click menu
            MakeSelection();
        }

        private void MakeSelection()
        {
            //Make selection via double click
            try
            {
                TargetProcess = Process.GetProcessById(IDsList[ProcessListView.SelectedIndices[0]]);
                this.Close();
            }
            catch
            {
                MessageBox.Show("No process is selected.", "Selection error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void closeProcessSelect_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        //Temporary auto attach
        private void timer1_Tick(object sender, EventArgs e)
        {
            ProcessList = new List<Process>(Process.GetProcesses());

            foreach (Process pro in ProcessList)
            {
                if (pro.ProcessName == "MemoryEditTest")
                {
                    TargetProcess = Process.GetProcessesByName("MemoryEditTest")[0];
                    this.Close();
                }
            }
        }
    }

    //Class that can sort processes by time since execution
    class ProcessComparer : IComparer<Process>
    {
        public static readonly ProcessComparer Default = new ProcessComparer();
        public static readonly ProcessComparer Reverse = new ProcessComparer(true, false);

        private readonly bool _reverse;
        private readonly bool _ignoreCase;

        public ProcessComparer() : this(false, false) { }

        public ProcessComparer(bool reverse, bool ignoreCase)
        {
            this._reverse = reverse;
            this._ignoreCase = ignoreCase;
        }

        public int Compare(Process x, Process y)
        {
            sbyte reverse = 1;
            if (_reverse == true)
                reverse *= -1;

            if (x == y) return 0;

            if (x.BasePriority == 0 && y.BasePriority == 0)
                return 0;
            if (x.BasePriority == 0)
                return -1 * reverse;
            if (y.BasePriority == 0)
                return 1 * reverse;

            if (this._reverse)
                return DateTime.Compare(y.StartTime, x.StartTime);
            else
                return DateTime.Compare(x.StartTime, y.StartTime);
        }
    }

    //Useful p/invokes and structures for icon grabbing
    class Icons
    {

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] //260 max path
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)] //80 max type
            public string szTypeName;
        };

        [Flags]
        public enum SHGFI : int
        {
            /// <summary>get icon</summary>
            Icon = 0x000000100,
            /// <summary>get display name</summary>
            DisplayName = 0x000000200,
            /// <summary>get type name</summary>
            TypeName = 0x000000400,
            /// <summary>get attributes</summary>
            Attributes = 0x000000800,
            /// <summary>get icon location</summary>
            IconLocation = 0x000001000,
            /// <summary>return exe type</summary>
            ExeType = 0x000002000,
            /// <summary>get system icon index</summary>
            SysIconIndex = 0x000004000,
            /// <summary>put a link overlay on icon</summary>
            LinkOverlay = 0x000008000,
            /// <summary>show icon in selected state</summary>
            Selected = 0x000010000,
            /// <summary>get only specified attributes</summary>
            Attr_Specified = 0x000020000,
            /// <summary>get large icon</summary>
            LargeIcon = 0x000000000,
            /// <summary>get small icon</summary>
            SmallIcon = 0x000000001,
            /// <summary>get open icon</summary>
            OpenIcon = 0x000000002,
            /// <summary>get shell size icon</summary>
            ShellIconSize = 0x000000004,
            /// <summary>pszPath is a pidl</summary>
            PIDL = 0x000000008,
            /// <summary>use passed dwFileAttribute</summary>
            UseFileAttributes = 0x000000010,
            /// <summary>apply the appropriate overlays</summary>
            AddOverlays = 0x000000020,
            /// <summary>Get the index of the overlay in the upper 8 bits of the iIcon</summary>
            OverlayIndex = 0x000000040,
        }

        // P/Invokes to allow extraction of Icons from processes/files
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern uint ExtractIconEx(string szFileName, int nIconIndex,
           IntPtr[] phiconLarge, IntPtr[] phiconSmall, uint nIcons);

        [DllImport("shell32.dll", SetLastError = true)]
        public static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex);

        //[DllImport("shell32.dll", CharSet = CharSet.Auto)]
        //public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes,
        //    ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int SHGetFileInfo(string pszPath, int dwFileAttributes,
            out SHFILEINFO psfi, uint cbfileInfo, SHGFI uFlags);

        [DllImport("user32.dll", EntryPoint = "DestroyIcon", SetLastError = true)]
        public static extern int DestroyIcon(IntPtr hIcon);
    }
}