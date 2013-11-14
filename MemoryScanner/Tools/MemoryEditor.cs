using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using Aecial.MemoryScanner;
using Aecial.Conversions;

namespace AecialEngine
{
    public partial class MemoryEditor : Form
    {
        #region Variables & Initialization

        private Stopwatch ScanTimeStopwatch;

        private AecialEngine.ScanHistory ScanHistory = new ScanHistory();
        private List<TableEntry> TableData = new List<TableEntry>();
        private List<TableEntry> DeletedData = new List<TableEntry>(); //Allows user to undo if they're retarded

        private ProcessMemoryEditor ProcessMemoryEditor = new ProcessMemoryEditor();
        private ScanMemory Scan;
        private ScanDataType SelectedScanType;

        private const UInt64 SystemMaxAddress = 0x7FFFFFFF; //0x7FFFFFFF is the highest for usermode memory

        private bool Canceled = false;

        private MemoryMappedFile CurrentScanAddresses;
        private MemoryMappedViewAccessor CurrentAddressAccessor;

        private bool DisplayFoundAsSigned = false;
        private int StartSelectionIndex = -1;
        private int EndSelectionIndex = -1;
        private int CurrentScrollVal = 0;
        private string ExecutablePath = Path.GetDirectoryName(Application.ExecutablePath);
        private Process TargetProcess;
        public Process Process
        {
            set
            {
                TargetProcess = value;
                selectedProcessLabel.Text = Conversions.ToAddress(Convert.ToString(TargetProcess.Id)) + " - " + TargetProcess.ProcessName;
                ProcessMemoryEditor.OpenProcess(TargetProcess.Id);
            }
            get
            { return TargetProcess; }

        }

        public MemoryEditor()
        {
            InitializeComponent();
        }

        //Form load event (Startup code)
        private void MemoryEditor_Load(object sender, EventArgs e)
        {
            //Enable double buffering to significantly reduce/prevent flicker of frequenly updated controls
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            //Create necessary scan save files if they dont exist
            if (!File.Exists(@ExecutablePath + @"/Scan Data/FirstScanAddresses"))
                File.Create(@ExecutablePath + @"/Scan Data/FirstScanAddresses");
            if (!File.Exists(@ExecutablePath + @"/Scan Data/FirstScanValues"))
                File.Create(@ExecutablePath + @"/Scan Data/FirstScanValues");
            if (!File.Exists(@ExecutablePath + @"/Scan Data/CurrentScanAddresses"))
                File.Create(@ExecutablePath + @"/Scan Data/CurrentScanAddresses");
            if (!File.Exists(@ExecutablePath + @"/Scan Data/CurrentScanValues"))
                File.Create(@ExecutablePath + @"/Scan Data/CurrentScanValues");
            if (!File.Exists(@ExecutablePath + @"/Scan Data/LastScanAddresses"))
                File.Create(@ExecutablePath + @"/Scan Data/LastScanAddresses");
            if (!File.Exists(@ExecutablePath + @"/Scan Data/LastScanValues"))
                File.Create(@ExecutablePath + @"/Scan Data/LastScanValues");

            ShowFirstScanOptions();
            //Widths are unequal to make it obvious in GUI, but that is corrected here.
            ScanCompareTypeComboBox.Width = ScanCompareTypeComboBox.Width;
            //Set to 4 bytes scan
            ScanDataTypeComboBox.SelectedIndex = 3;
            ProtectionCheckedListBox.SetItemChecked(2, true);
            //ProtectionCheckedListBox.SetItemChecked(3, true);
            ProtectionCheckedListBox.SetItemChecked(6, true);
            //ProtectionCheckedListBox.SetItemChecked(7, true);
            TypeCheckedListBox.SetItemChecked(0, true);
            TypeCheckedListBox.SetItemChecked(1, true);

            SetToolTips();
        }

        //Sets tool tips for all vital GUI elements
        private void SetToolTips()
        {
            SelectProcessButton.Text = "Select a target process.";
            OpenATButton.Text = "Opens an Aecial Table.";
            MergeATButton.Text = "Opens an Aecial Table and merges it with current table.";
            SaveATButton.Text = "Saves the current Aecial Table to the specified location.";
            StartScanButton.Text = "Starts a new scan based on settings.";
            NextScanButton.Text = "Start the next scan.";
            UndoScanButton.Text = "Go back to the results of last scan.";
            AbortScanButton.Text = "Cancels the current scan.";
            ScanHistoryButton.Text = "Shows history of scans since startup.";
            AddSelectedButton.Text = "Adds the selected addresses above to table.";
            AddSpecificButton.Text = "Adds a specific address to the table.";
            ClearTableButton.Text = "Deletes all data in table.";

            GUIToolTip.SetToolTip(ProtectionCheckedListBox, "Scanner will only scan checked memory protection types.");
            GUIToolTip.SetToolTip(ScanCompareTypeComboBox, "Determines how to compare to value being searched for.");
            GUIToolTip.SetToolTip(ScanDataTypeComboBox, "Data type to search for.");
            GUIToolTip.SetToolTip(StartAddressTextBox, "Lower bound of addresses to search in.");
            GUIToolTip.SetToolTip(EndAddressTextBox, "Upper bound of addresses to search in.");
            GUIToolTip.SetToolTip(AllignmentRB, "Only adds addresses alligned to this value (4 is standard).");
            GUIToolTip.SetToolTip(LastDigitsRB, "Only adds addresses that end in the specified digits.");
            GUIToolTip.SetToolTip(OptimizeScanCB, "Significantly improves speed, can result in scan data loss.");
            GUIToolTip.SetToolTip(IsHexCB, "Check this if values entered are in hex.");
        }

        #endregion

        #region Form Events

        private void OptimizeScanCB_CheckedChanged(object sender, EventArgs e)
        {
            if (OptimizeScanCB.Checked)
            {
                OptimizeScanVal.Enabled = true;
                AllignmentRB.Enabled = true;
                LastDigitsRB.Enabled = true;
            }
            else
            {
                OptimizeScanVal.Enabled = false;
                AllignmentRB.Enabled = false;
                LastDigitsRB.Enabled = false;
            }
        }

        private void ScanTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Set to false here & overwrite later if needed
            TruncatedRB.Visible = false;
            RoundedRB.Visible = false;

            ScanDataType _ScanValueType = (ScanDataType)ScanDataTypeComboBox.SelectedIndex;
            switch (_ScanValueType)
            {
                case ScanDataType.Binary:
                case ScanDataType.Byte:
                    OptimizeScanVal.Value = 1;
                    break;
                case ScanDataType.Int16:
                    OptimizeScanVal.Value = 2;
                    break;
                case ScanDataType.Single:
                case ScanDataType.Double:
                    TruncatedRB.Visible = true;
                    RoundedRB.Visible = true;
                    OptimizeScanVal.Value = 4;
                    break;
                case ScanDataType.Int32:
                case ScanDataType.Int64:
                    OptimizeScanVal.Value = 4;
                    break;
            }
        }

        private void convertToHexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Converts target item from dec to hex
            ToolStripMenuItem MenuItem = sender as ToolStripMenuItem;
            if (MenuItem != null)
            {
                ContextMenuStrip RightClickMenu = MenuItem.Owner as ContextMenuStrip;

                if (RightClickMenu != null)
                {
                    TextBox ControlSelected = (TextBox)RightClickMenu.SourceControl;
                    ControlSelected.Text = Conversions.DecToHex(ControlSelected.Text, false);
                }
            }
        }

        private void convertToDecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Converts target item from hex to dec
            ToolStripMenuItem MenuItem = sender as ToolStripMenuItem;
            if (MenuItem != null)
            {
                ContextMenuStrip RightClickMenu = MenuItem.Owner as ContextMenuStrip;

                if (RightClickMenu != null)
                {
                    TextBox ControlSelected = (TextBox)RightClickMenu.SourceControl;
                    ControlSelected.Text = Conversions.HexToDec(ControlSelected.Text, true);
                }
            }
        }

        //These methods are pretty ghetto, but it works and makes things pretty easy.
        private void ShowFirstScanOptions()
        {
            ScanCompareTypeComboBox.Items.Clear();
            ScanCompareTypeComboBox.Items.Add("=");
            ScanCompareTypeComboBox.Items.Add("≠");
            ScanCompareTypeComboBox.Items.Add("<");
            ScanCompareTypeComboBox.Items.Add(">");
            ScanCompareTypeComboBox.Items.Add("≤");
            ScanCompareTypeComboBox.Items.Add("≥");
            ScanCompareTypeComboBox.Items.Add("> <");
            ScanCompareTypeComboBox.Items.Add("≥ ≤");
            ScanCompareTypeComboBox.Items.Add("??");
            ScanCompareTypeComboBox.SelectedIndex = 0;
        }

        private void ShowSecondScanOptions()
        {
            ScanCompareTypeComboBox.Items.Clear();
            ScanCompareTypeComboBox.Items.Add("=");
            ScanCompareTypeComboBox.Items.Add("Ø");
            ScanCompareTypeComboBox.Items.Add("±");
            ScanCompareTypeComboBox.Items.Add("≠");
            ScanCompareTypeComboBox.Items.Add("+");
            ScanCompareTypeComboBox.Items.Add("-");
            ScanCompareTypeComboBox.Items.Add("+x");
            ScanCompareTypeComboBox.Items.Add("-x");
            ScanCompareTypeComboBox.Items.Add("<");
            ScanCompareTypeComboBox.Items.Add(">");
            ScanCompareTypeComboBox.Items.Add("≤");
            ScanCompareTypeComboBox.Items.Add("≥");
            ScanCompareTypeComboBox.Items.Add("> <");
            ScanCompareTypeComboBox.Items.Add("≥ ≤");
            ScanCompareTypeComboBox.Items.Add("??");
            ScanCompareTypeComboBox.SelectedIndex = 0;
        }

        private string OldValueText = "";
        private string OldSecondValueText = "";
        private void ScanCompareTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Get scan compare type through item text comparison
            ScanCompareType RescanCompareType =
                Conversions.StringToScanType(ScanCompareTypeComboBox.Items[ScanCompareTypeComboBox.SelectedIndex].ToString());

            //Collect old values if applicable
            if (ScanValueTextBox.Enabled)
                OldValueText = ScanValueTextBox.Text;
            if (ScanSecondValueTextBox.Visible)
                OldSecondValueText = ScanSecondValueTextBox.Text;

            //Just take the enum name and replace underscores
            CompareTypeLabel.Text = RescanCompareType.ToString().Replace('_', ' ');

            //Update GUI based on type of scan being selected
            switch (RescanCompareType)
            {
                case ScanCompareType.Exact_Value:
                case ScanCompareType.Not_Equal_to_Value:
                case ScanCompareType.Increased_by_Value:
                case ScanCompareType.Decreased_by_Value:
                case ScanCompareType.Less_Than:
                case ScanCompareType.Greater_Than:
                    ScanSecondValueTextBox.Text = "";
                    ScanValueTextBox.Text = OldValueText;
                    ScanSecondValueTextBox.Visible = false;
                    ScanValueTextBox.Enabled = true;
                    ScanValueTextBox.Width = 214;
                    break;

                case ScanCompareType.Changed_Value:
                case ScanCompareType.Unchanged_Value:
                case ScanCompareType.Increased_Value:
                case ScanCompareType.Decreased_Value:
                case ScanCompareType.Unknown_Value:
                    ScanValueTextBox.Text = "";
                    ScanSecondValueTextBox.Text = "";
                    ScanSecondValueTextBox.Visible = false;
                    ScanValueTextBox.Enabled = false;
                    ScanValueTextBox.Width = 214;
                    break;

                case ScanCompareType.Between_Exclusive:
                    ScanSecondValueTextBox.Text = OldSecondValueText;
                    ScanSecondValueTextBox.Visible = true;
                    ScanValueTextBox.Visible = true;
                    ScanValueTextBox.Enabled = true;
                    ScanValueTextBox.Width = ScanSecondValueTextBox.Width;
                    break;

            }

        }

        #endregion

        #region Scan Events
        //Called when scan is aborted
        void scan_ScanCanceled(object sender, ScanCanceledEventArgs e)
        {
            Canceled = true;
            AddressListView.VirtualListSize = 0;
            UpdateFoundTimer.Enabled = false;
            AddressListView.Items.Clear();
            AddressCount.Text = "";
            ProgressBar.Value = 0;
            AbortScanButton.Enabled = false;
            StartScanButton.Enabled = false;
            AddressCount.Text = "Aborted Scan";

            MessageBox.Show("Scan canceled by the user.", "Scan canceled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //Called when scan is finished
        delegate void Completed(object sender, ScanCompletedEventArgs e);
        void scan_ScanCompleted(object sender, ScanCompletedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Completed completed = new Completed(scan_ScanCompleted);
                this.Invoke(completed, new object[] { sender, e });
            }
            else
            {
                if (!Canceled)
                {
                    //addressListView.EndUpdate();
                    //addressListView.ResumeLayout();
                    ProgressBar.Value = 0;

                    int TotalFound = (int)(e.TotalFoundSize / (Int64)DataTypeSize.Int64);

                    if (TotalFound > 0)
                    {
                        CurrentScanAddresses = MemoryMappedFile.CreateFromFile(@ExecutablePath + @"/Scan Data/CurrentScanAddresses", FileMode.Open, "CurrentScanAddresses", e.TotalFoundSize);
                        CurrentAddressAccessor = CurrentScanAddresses.CreateViewAccessor(0, e.TotalFoundSize);
                    }

                    AddressCount.Text = "Items: " + TotalFound.ToString();
                    AddressListView.VirtualListSize = TotalFound;

                    NewScanButton.Enabled = true;
                    AbortScanButton.Enabled = false;
                    UpdateFoundTimer.Enabled = true;
                    NextScanButton.Enabled = true;
                    UndoScanButton.Enabled = true;
                }
            }
        }

        delegate void Progress(object sender, ScanProgressChangedEventArgs e);
        void scan_ScanProgressChanged(object sender, ScanProgressChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Progress progress = new Progress(scan_ScanProgressChanged);
                this.Invoke(progress, new object[] { sender, e });
            }
            else
            {
                if (e.Progress > 100)
                    e.Progress = 100;
                ProgressBar.Value = e.Progress;
                if (e.Progress == 100)
                {
                    ScanTimeLabel.Text = "Time: " + ScanTimeStopwatch.ElapsedMilliseconds.ToString();
                    ScanTimeStopwatch.Stop();
                    AddressCount.Text = "Items: Sorting...";
                }
            }
        }

        #endregion

        #region Scan UI Events

        //Prepare for new scan
        private void NewScanButton_Click(object sender, EventArgs e)
        {
            //Clear found items & set up buttons

            AddressListView.VirtualListSize = 0;
            AddressListView.Items.Clear();
            CurrentAddressAccessor.Dispose();

            UpdateFoundTimer.Enabled = false;
            NextScanButton.Enabled = false;
            UndoScanButton.Enabled = false;
            NewScanButton.Enabled = false;
            StartScanButton.Enabled = false;
            ScanDataTypeComboBox.Enabled = true;
            StartAddressTextBox.Enabled = true;
            EndAddressTextBox.Enabled = true;
            StartScanButton.Enabled = true;

            ShowFirstScanOptions();

            AddressCount.Text = "";
        }

        //First scan
        private void StartScanButton_Click(object sender, EventArgs e)
        {

            //Address range specified by user
            UInt64 FirstAddress;
            UInt64 LastAddress;

            if (!CheckSyntax.Address(EndAddressTextBox.Text) || !CheckSyntax.Address(StartAddressTextBox.Text))
            {
                MessageBox.Show("Address range must be in hexadecimal format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FirstAddress = Conversions.HexToUInt64(StartAddressTextBox.Text);
            LastAddress = Conversions.HexToUInt64(EndAddressTextBox.Text);

            if ((FirstAddress < 0 || FirstAddress > SystemMaxAddress) || (LastAddress < 0 || LastAddress > SystemMaxAddress))
            {
                MessageBox.Show("Addresses must be between 0 and " + SystemMaxAddress.ToString() + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Swap addresses if end is before start
            if (LastAddress < FirstAddress)
            {
                MessageBox.Show("Invalid address range. Swapping values and attempting to proceed anyways.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                UInt64 Temp = FirstAddress;
                FirstAddress = LastAddress;
                LastAddress = Temp;
            }

            if (OptimizeScanCB.Checked && !LastDigitsRB.Checked)
            {
                //If the start address doesn't match up with the allignment, keep incrementing it
                while (FirstAddress % OptimizeScanVal.Value != 0)
                    FirstAddress++;
                //Same for final address, except decrement
                while (LastAddress % OptimizeScanVal.Value != 0)
                    LastAddress--;

                if ((FirstAddress < 0 || FirstAddress > SystemMaxAddress) || (LastAddress < 0 || LastAddress > SystemMaxAddress))
                {
                    MessageBox.Show("Specified allignment results in invalid address range.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            //Try to start a scan for the given address range
            if (!ScanButtonShared(false, FirstAddress, LastAddress))
                return;

            //Update necessary values & GUI elements
            StartAddressTextBox.Enabled = false;
            EndAddressTextBox.Enabled = false;
            ScanDataTypeComboBox.Enabled = false;
            StartScanButton.Enabled = false;
            Canceled = false;
            AbortScanButton.Enabled = true;
            AddressCount.Text = "Scanning";
            AddressListView.Items.Clear();
            //Add to scan history
            ScanHistory.Session.Add(new ScanHistory.ScanSession(ScanDataTypeComboBox.Text,
                ScanCompareTypeComboBox.Text, ScanValueTextBox.Text, ScanSecondValueTextBox.Text));
            ShowSecondScanOptions();
        }

        // Next Scan
        private void NextScanButton_Click(object sender, EventArgs e)
        {
            //Address range is between first and last found addresses
            UInt64 FirstAddress = 0;
            UInt64 LastAddress = 0;

            if (CurrentAddressAccessor.Capacity > 0)
                CurrentAddressAccessor.Read(0, out FirstAddress);

            if (CurrentAddressAccessor.Capacity > 0)
                CurrentAddressAccessor.Read(CurrentAddressAccessor.Capacity - (Int64)DataTypeSize.Int64, out LastAddress);

            //Try to start a rescan for the given address range
            if (!ScanButtonShared(true, FirstAddress, LastAddress))
                return;

            //Update necessary values & GUI elements
            Canceled = false;
            AbortScanButton.Enabled = true;
            AddressListView.SelectedIndices.Clear();
            //Add to scan history
            ScanHistory.Session[ScanHistory.MostRecent()].AddScanType(ScanCompareTypeComboBox.Text);
            ScanHistory.Session[ScanHistory.MostRecent()].AddScanValue(ScanValueTextBox.Text, ScanSecondValueTextBox.Text);
        }

        //Shared by First & Next scan
        private bool ScanButtonShared(bool IsRescan, UInt64 FirstAddress, UInt64 LastAddress)
        {
            //Object that holds data of variable type and amount we wish to scan for
            object ScanValue;
            object SecondScanValue;

            SelectedScanType = (ScanDataType)ScanDataTypeComboBox.SelectedIndex;

            #region Collect scan data from form and check for errors

            //Check to see if a process has been selected
            if (TargetProcess == null)
            {
                MessageBox.Show("Please select a process", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!CheckSyntax.Value(ScanValueTextBox.Text, (ScanDataType)ScanDataTypeComboBox.SelectedIndex, IsHexCB.Checked) && ScanValueTextBox.Enabled == true ||
                !CheckSyntax.Value(ScanSecondValueTextBox.Text, (ScanDataType)ScanDataTypeComboBox.SelectedIndex, IsHexCB.Checked) && ScanSecondValueTextBox.Visible == true)
            {
                MessageBox.Show("Invalid value format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (ScanValueTextBox.Enabled == true)
                ScanValue = Conversions.ToUnsigned(ScanValueTextBox.Text, SelectedScanType);
            else
                ScanValue = null;
            // ScanValue = Conversions.ToUnsigned("0", ScanType);

            if (ScanSecondValueTextBox.Visible == true)
                SecondScanValue = Conversions.ToUnsigned(ScanSecondValueTextBox.Text, SelectedScanType);
            else
                SecondScanValue = null;
            //SecondScanValue = Conversions.ToUnsigned("0", ScanType);

            #endregion

            #region Prescan events

            //Figure out what Protect types to scan based on check states
            uint[] Protect = new uint[8];
            int CheckedItems = ProtectionCheckedListBox.CheckedIndices.Count;
            for (int ecx = 0; ecx < CheckedItems; ecx++)
                Protect[ProtectionCheckedListBox.CheckedIndices[ecx]] = 1;
            uint[] Type = new uint[3];
            CheckedItems = TypeCheckedListBox.CheckedIndices.Count;
            for (int ecx = 0; ecx < CheckedItems; ecx++)
                Type[TypeCheckedListBox.CheckedIndices[ecx]] = 1;

            ScanOptimizationData ScanOptimizationData = new ScanOptimizationData(OptimizeScanCB.Checked, (UInt64)OptimizeScanVal.Value, LastDigitsRB.Checked);

            bool CompareToFirstScan = false;
            if (IsRescan && CompareToFirstCB.Checked == true)
                CompareToFirstScan = true;

            //Create new instance of scanner and pass it all necessary info
            Scan = new ScanMemory(TargetProcess, FirstAddress, LastAddress, SelectedScanType,
                Conversions.StringToScanType(ScanCompareTypeComboBox.Items[ScanCompareTypeComboBox.SelectedIndex].ToString()),
                ScanValue, SecondScanValue, Protect, Type, TruncatedRB.Checked, ScanOptimizationData, ExecutablePath, CompareToFirstScan);

            Scan.ScanProgressChanged += new ScanMemory.ScanProgressedEventHandler(scan_ScanProgressChanged);
            Scan.ScanCompleted += new ScanMemory.ScanCompletedEventHandler(scan_ScanCompleted);
            Scan.ScanCanceled += new ScanMemory.ScanCanceledEventHandler(scan_ScanCanceled);

            AddressListView.VirtualListSize = 0;
            AddressListView.Items.Clear();

            if (CurrentAddressAccessor != null)
                CurrentAddressAccessor.Dispose();
            if (CurrentScanAddresses != null)
                CurrentScanAddresses.Dispose();

            ProgressBar.Value = 0;

            #endregion

            #region Start scan

            ScanTimeStopwatch = Stopwatch.StartNew();

            try
            {
                Scan.StartScan(IsRescan);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Scan.CancelScan();
                return false;
            }
            return true;

            #endregion
        }

        //Abort scan
        private void AbortScanButton_Click(object sender, EventArgs e)
        {
            //Abort active threads & clear progress
            Scan.CancelScan();
            ProgressBar.Value = 0;

            //Call new scan event
            NewScanButton_Click(sender, e);
        }


        #endregion

        #region Found List Events

        private void AddCurrentSelectedRange()
        {
            for (int ecx = StartSelectionIndex; ecx <= EndSelectionIndex; ecx++)
            {
                //Add table data based on selected object
                AddTable("No Description", Conversions.HexToUInt64(Convert.ToString(AddressListView.Items[ecx].Text)),
                    SelectedScanType, false, false, false, "");
            }
        }

        private void addItemToTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCurrentSelectedRange();
        }

        private const int HeaderPixels = 27; //Extra random unaccounted for pixels
        //Set selection range based on click
        private void AddressListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (AddressListView.Items.Count <= 0)
                return;

            int Height = AddressListView.Items[0].Bounds.Height;
            int HeaderHeight = AddressListView.Items[0].Bounds.Top;

            int TargetIndex = (Cursor.Position.Y - this.Location.Y - HeaderHeight - AddressListView.Location.Y - HeaderPixels)
                / Height;

            if (ModifierKeys == Keys.Shift)
            {
                EndSelectionIndex = TargetIndex;
                if (StartSelectionIndex == -1)
                    StartSelectionIndex = EndSelectionIndex;
            }
            else
            {
                if (e.Button == MouseButtons.Left || TargetIndex < StartSelectionIndex || TargetIndex > EndSelectionIndex)
                {
                    StartSelectionIndex = TargetIndex;
                    EndSelectionIndex = StartSelectionIndex;
                }
            }

            //Swap if end is before start
            if (EndSelectionIndex < StartSelectionIndex)
            {
                int Temp = StartSelectionIndex;
                StartSelectionIndex = EndSelectionIndex;
                EndSelectionIndex = Temp;
            }

        }

        private void showItemsAsSignedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem MenuItem = sender as ToolStripMenuItem;
            MenuItem.Checked = !MenuItem.Checked;
            DisplayFoundAsSigned = MenuItem.Checked;
        }

        private bool GetScrollIndex = false;
        private void updateFoundTimer_Tick(object sender, EventArgs e)
        {

            AddressListView.BeginUpdate(); //May not help very much, supposed to reduce flicker

            GetScrollIndex = true;
            //Refresh list view, forcing the RetreiveVirtualItemEvent event to raise
            AddressListView.Refresh();
            GetScrollIndex = false;

            AddressListView.EndUpdate(); //Allow redrawing
        }


        private void addressListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            //Get the current scroll position if needed
            if (GetScrollIndex && e.ItemIndex != -1)
            {
                GetScrollIndex = false;
                CurrentScrollVal = e.ItemIndex;
            }

            //Find addresses of each currently viewed item in the addressListBox
            UInt64 FoundAddress;
            CurrentAddressAccessor.Read(e.ItemIndex * (Int64)DataTypeSize.Int64, out FoundAddress);
            ListViewItem lvi = new ListViewItem(Conversions.ToAddress(FoundAddress.ToString()));

            //Read value from address
            switch (SelectedScanType)
            {
                case ScanDataType.Binary:
                    lvi.SubItems.Add(ProcessMemoryEditor.ReadByte(FoundAddress).ToString());
                    break;
                case ScanDataType.Byte:
                    if (DisplayFoundAsSigned)
                        lvi.SubItems.Add(ProcessMemoryEditor.ReadSByte(FoundAddress).ToString());
                    else
                        lvi.SubItems.Add(ProcessMemoryEditor.ReadByte(FoundAddress).ToString());
                    break;
                case ScanDataType.Int16:
                    if (DisplayFoundAsSigned)
                        lvi.SubItems.Add(ProcessMemoryEditor.ReadInt16(FoundAddress).ToString());
                    else
                        lvi.SubItems.Add(ProcessMemoryEditor.ReadUInt16(FoundAddress).ToString());
                    break;
                case ScanDataType.Int32:
                    if (DisplayFoundAsSigned)
                        lvi.SubItems.Add(ProcessMemoryEditor.ReadInt32(FoundAddress).ToString());
                    else
                        lvi.SubItems.Add(ProcessMemoryEditor.ReadUInt32(FoundAddress).ToString());
                    break;
                case ScanDataType.Int64:
                    if (DisplayFoundAsSigned)
                        lvi.SubItems.Add(ProcessMemoryEditor.ReadInt64(FoundAddress).ToString());
                    else
                        lvi.SubItems.Add(ProcessMemoryEditor.ReadUInt64(FoundAddress).ToString());
                    break;
                case ScanDataType.Single:
                    lvi.SubItems.Add(ProcessMemoryEditor.ReadSingle(FoundAddress).ToString());
                    break;
                case ScanDataType.Double:
                    lvi.SubItems.Add(ProcessMemoryEditor.ReadDouble(FoundAddress).ToString());
                    break;
                case ScanDataType.Text:
                    lvi.SubItems.Add(ProcessMemoryEditor.ReadUInt32(FoundAddress).ToString());
                    break;
                case ScanDataType.AOB:
                    lvi.SubItems.Add(ProcessMemoryEditor.ReadUInt32(FoundAddress).ToString());
                    break;
                case ScanDataType.All:
                    lvi.SubItems.Add(ProcessMemoryEditor.ReadUInt32(FoundAddress).ToString());
                    break;
                default:
                    lvi.SubItems.Add("Unknown data type.");
                    break;
            }

            //Apply changes to the event item
            e.Item = lvi;
        }



        #endregion

        #region Table Events

        private void WriteTimer_Tick(object sender, EventArgs e)
        {
            //Update all items in our table
            int TableIndex = 0;

            foreach (TableEntry Table in TableData)
            {
                Table.CheckState = TableListView.Items[TableIndex].Checked;

                //Only read values if not trying to freeze them
                if (Table.CheckState == false)
                {
                    switch (Table.ScanType)
                    {
                        case ScanDataType.Binary:
                            Table.Value = ProcessMemoryEditor.ReadByte(Table.Address);
                            break;
                        case ScanDataType.Byte:
                            Table.Value = ProcessMemoryEditor.ReadByte(Table.Address);
                            break;
                        case ScanDataType.Int16:
                            Table.Value = ProcessMemoryEditor.ReadUInt16(Table.Address);
                            break;
                        case ScanDataType.Int32:
                            Table.Value = ProcessMemoryEditor.ReadUInt32(Table.Address);
                            break;
                        case ScanDataType.Int64:
                            Table.Value = ProcessMemoryEditor.ReadUInt64(Table.Address);
                            break;
                        case ScanDataType.Single:
                            Table.Value = ProcessMemoryEditor.ReadSingle(Table.Address);
                            break;
                        case ScanDataType.Double:
                            Table.Value = ProcessMemoryEditor.ReadDouble(Table.Address);
                            break;
                        case ScanDataType.Text:
                            //TODO: everything
                            Table.Value = ProcessMemoryEditor.ReadString(Table.Address, 20, 0);
                            break;
                        case ScanDataType.AOB:
                            break;
                        case ScanDataType.All:
                            break;
                        default:
                            TableListView.Items[TableIndex].SubItems[4].Text = "Error";
                            break;
                    } //end switch

                    //Only update text if necessary, to avoid flicker
                    if (TableListView.Items[TableIndex].SubItems[4].Text != Table.Value.ToString())
                        TableListView.Items[TableIndex].SubItems[4].Text = Table.Value.ToString();

                } //end checkstate compare

                TableWriteMemory(Table, Table.CheckState);
                TableIndex++;
            } //end foreach table

        }


        //Called repeatedly to write to frozen addresses
        //(also called when a value is reassigned to an address, regardless if frozen or not)
        private void TableWriteMemory(TableEntry Table, bool CheckState)
        {
            //Lets not try and freeze a value we can't even find
            if (Table.Value != null && CheckState == true)
            {
                //Figure out the type and write the memory accordingly
                switch (Table.ScanType)
                {
                    case ScanDataType.Binary:
                        ProcessMemoryEditor.Write(Table.Address, Convert.ToByte(Table.Value));
                        break;
                    case ScanDataType.Byte:
                        ProcessMemoryEditor.Write(Table.Address, Convert.ToByte(Table.Value));
                        break;
                    case ScanDataType.Int16:
                        ProcessMemoryEditor.Write(Table.Address, Convert.ToUInt16(Table.Value));
                        break;
                    case ScanDataType.Int32:
                        ProcessMemoryEditor.Write(Table.Address, Convert.ToUInt32(Table.Value));
                        break;
                    case ScanDataType.Int64:
                        ProcessMemoryEditor.Write(Table.Address, Convert.ToUInt64(Table.Value));
                        break;
                    case ScanDataType.Single:
                        ProcessMemoryEditor.Write(Table.Address, Convert.ToSingle(Table.Value));
                        break;
                    case ScanDataType.Double:
                        ProcessMemoryEditor.Write(Table.Address, Convert.ToDouble(Table.Value));
                        break;
                    case ScanDataType.Text:
                        //TODO: everything
                        ProcessMemoryEditor.Write(Table.Address, Convert.ToString(Table.Value), 0);
                        break;
                    case ScanDataType.AOB:
                        break;
                    case ScanDataType.All:
                        break;
                }
            }
        }


        private void ClearTableButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear all data in table?", "Clear Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                DeletedData.AddRange(TableData);
                TableListView.Items.Clear();
                TableData.Clear();
                UndoTableDeleteButton.Enabled = true;
                ClearTableButton.Enabled = false;
            }
        }

        private void UndoTableDeleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Revert to old table data? This will replace current data.", "Clear Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                TableData.Clear();
                TableListView.Items.Clear();
                for (int ecx = 0; ecx < DeletedData.Count; ecx++)
                {
                    AddTable(DeletedData[ecx].Description, DeletedData[ecx].Address, DeletedData[ecx].ScanType,
                        DeletedData[ecx].IsSigned, DeletedData[ecx].ValueAsHex, DeletedData[ecx].AddressAsHex,
                        DeletedData[ecx].ASMScript);
                }

                DeletedData.Clear();
                UndoTableDeleteButton.Enabled = false;
            }
        }


        private AddressWindow AddressWindow;
        private DescriptionWindow DescriptionWindow;
        private ValueWindow ValueWindow;
        private TypeWindow TypeWindow;

        //Adds specific address & data to our addedListView
        private void AddSpecificButton_Click(object sender, EventArgs e)
        {
            AddSpecificWindow AddSpecificAddress = new AddSpecificWindow(false, "", "", ScanDataType.All);
            AddSpecificAddress.ShowDialog();
            if (AddSpecificAddress.Description != null)
                AddTable(AddSpecificAddress.Description, Conversions.HexToUInt64(AddSpecificAddress.Address),
                    AddSpecificAddress.ScanType, false, false, false, "");

        }

        //Add selected address to our list of added addresses
        private void addressListView_DoubleClick(object sender, EventArgs e)
        {
            //Add table data based on selected object
            AddTable("No Description", Conversions.HexToUInt64(Convert.ToString(AddressListView.Items[AddressListView.SelectedIndices[0]].Text)),
                SelectedScanType, false, false, false, "");
        }

        private void AddSelectedButton_Click(object sender, EventArgs e)
        {
            AddCurrentSelectedRange();
        }

        public void AddTable(string Description, UInt64 Address, ScanDataType scantype, bool Signed, bool ValueAsHex, bool AddressAsHex, string ASMScript)
        {
            TableData.Add(new TableEntry(Description, Address, scantype, Signed, ValueAsHex, AddressAsHex, ASMScript));

            //Get index of last (most recently added) table
            int TableIndex = TableData.Count - 1;
            string[] AddInfo = new string[4]; //4 total parameters
            AddInfo[0] = TableData[TableIndex].Description;
            AddInfo[1] = Conversions.ToAddress((TableData[TableIndex].Address.ToString()));
            AddInfo[2] = Conversions.ScanTypeToName(TableData[TableIndex].ScanType);
            AddInfo[3] = "???";
            TableListView.Items.Add("").SubItems.AddRange(AddInfo);

            ClearTableButton.Enabled = true;
        }


        private const int PixelsBeforeCB = 9;
        private const int PixelsAfterCB = 21;

        // Checks an item in the table, ensuring that the checkbox was the item being clicked on
        private void addedListView_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int CursorRelativeToWindow = Cursor.Position.X - this.Location.X;
            //Listviews with checkboxes check the box when ANY location is clicked. Simply undo the
            //checkbox changes if the mouse is at a location further than it should be.
            if (CursorRelativeToWindow > TableListView.Location.X + PixelsAfterCB ||
                CursorRelativeToWindow < TableListView.Location.X + PixelsBeforeCB)
            {
                //Mouse out of desired bounds -- undo the checkbox changes
                if (e.NewValue == CheckState.Checked)
                    e.NewValue = CheckState.Unchecked;
                else
                    e.NewValue = CheckState.Checked;
            }
        }

        private void addedListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Rare occurance, but its possible to register a double click event whilst selecting nothing
            if (TableListView.SelectedItems.Count <= 0)
                return;

            //More fixing of these awful check boxes
            if (TableListView.SelectedItems[0].Checked == true)
                TableListView.SelectedItems[0].Checked = false;
            else
                TableListView.SelectedItems[0].Checked = true;

            TableData[TableListView.SelectedIndices[0]].CheckState = TableListView.SelectedItems[0].Checked;

            //index of selected item
            int index = TableListView.SelectedItems[0].Index;

            //Check if clicking description

            if (Cursor.Position.X - this.Location.X > TableListView.Items[index].SubItems[1].Bounds.Left &&
                Cursor.Position.X - this.Location.X < TableListView.Items[index].SubItems[1].Bounds.X + TableListView.Items[0].SubItems[1].Bounds.Width)
            {
                if (DescriptionWindow == null || DescriptionWindow.IsDisposed)
                {
                    DescriptionWindow = new DescriptionWindow(TableListView.Items[index].SubItems[1].Text);
                    if (DescriptionWindow.ShowDialog(this) == DialogResult.OK)
                    {
                        TableData[index].Description = DescriptionWindow.Description;
                        TableListView.Items[index].SubItems[1].Text = DescriptionWindow.Description;
                    }

                    DescriptionWindow.Dispose();
                }


            }
            //Check if clicking address
            else if (Cursor.Position.X - this.Location.X > TableListView.Items[TableListView.SelectedItems[0].Index].SubItems[2].Bounds.Left &&
                Cursor.Position.X - this.Location.X < TableListView.Items[index].SubItems[2].Bounds.X + TableListView.Items[0].SubItems[2].Bounds.Width)
            {
                if (AddressWindow == null || AddressWindow.IsDisposed)
                {
                    AddressWindow = new AddressWindow(TableListView.Items[index].SubItems[2].Text);
                    if (AddressWindow.ShowDialog(this) == DialogResult.OK)
                    {
                        TableData[index].Address = AddressWindow.Address;
                        TableListView.Items[index].SubItems[2].Text = Conversions.ToAddress(TableData[index].Address.ToString());
                    }
                    AddressWindow.Dispose();
                }

            }
            //Check if clicking type
            else if (Cursor.Position.X - this.Location.X > TableListView.Items[TableListView.SelectedItems[0].Index].SubItems[3].Bounds.Left &&
                Cursor.Position.X - this.Location.X < TableListView.Items[index].SubItems[3].Bounds.X + TableListView.Items[0].SubItems[3].Bounds.Width)
            {
                if (TypeWindow == null || TypeWindow.IsDisposed)
                {
                    TypeWindow = new TypeWindow(TableData[index].ScanType);
                    if (TypeWindow.ShowDialog(this) == DialogResult.OK)
                    {
                        //Get the string from the scan type selected in the window
                        string type = Conversions.ScanTypeToName(TypeWindow.ScanType);
                        TableData[index].ScanType = TypeWindow.ScanType;
                        TableListView.Items[index].SubItems[3].Text = type;
                    }

                    TypeWindow.Dispose();
                }
            }
            //Check if clicking value
            else if (Cursor.Position.X - this.Location.X > TableListView.Items[index].SubItems[4].Bounds.Left &&
                Cursor.Position.X - this.Location.X < TableListView.Items[index].SubItems[4].Bounds.X + TableListView.Items[0].SubItems[4].Bounds.Width)
            {
                if (ValueWindow == null || ValueWindow.IsDisposed)
                {
                    ValueWindow = new ValueWindow(TableData[index].ScanType, TableData[index].Value);
                    if (ValueWindow.ShowDialog(this) == DialogResult.OK)
                    {
                        TableData[index].Value = ValueWindow.Value;
                        TableWriteMemory(TableData[index], true);
                        TableListView.Items[index].SubItems[4].Text = ValueWindow.Value.ToString();
                    }
                    ValueWindow.Dispose();
                }
            }

        }

        #endregion

        #region Save/Open


        bool MergeTables = false;

        //Loads table data
        private void OpenATButton_Click(object sender, EventArgs e)
        {
            Stream OpenStream = null;
            OpenFileDialog OpenFile = new OpenFileDialog();

            OpenFile.InitialDirectory = "C:\\Users\\" + System.Security.Principal.WindowsIdentity.GetCurrent().User + "\\Documents\\My Aecial Tables";
            OpenFile.Filter = "Aecial Table|*.AT";
            if (!MergeTables)
                OpenFile.Title = "Open an Aecial Table";
            else
                OpenFile.Title = "Merge an Aecial Table";
            OpenFile.RestoreDirectory = true;

            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((OpenStream = OpenFile.OpenFile()) != null)
                    {
                        using (OpenStream)
                        {
                            if (!MergeTables)
                            {
                                TableData.Clear();
                                TableListView.Items.Clear();
                            }

                            //Open binary formatting reader
                            BinaryFormatter bformatter = new BinaryFormatter();
                            int TableCount = (int)bformatter.Deserialize(OpenStream);

                            for (int ecx = 0; ecx < TableCount; ecx++)
                            {
                                TableEntry Data = (TableEntry)bformatter.Deserialize(OpenStream);
                                AddTable(Data.Description, Data.Address, Data.ScanType, Data.IsSigned, Data.ValueAsHex, Data.AddressAsHex, Data.ASMScript);
                            }

                            OpenStream.Close();

                        }
                    }
                }
                catch (Exception ex)
                {
                    OpenStream.Close();
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

            //Reset variable
            MergeTables = false;
        }

        //Merges a table with current table
        private void MergeATButton_Click(object sender, EventArgs e)
        {
            //Set variable indicating we wish to merge
            MergeTables = true;

            //Call same code for opening a file
            OpenATButton_Click(sender, e);
        }

        //Save table data
        private void SaveATButton_Click(object sender, EventArgs e)
        {
            //Let user pick name of file to save as
            SaveFileDialog SaveFile = new SaveFileDialog();
            SaveFile.Filter = "Aecial Table|*.AT";
            SaveFile.Title = "Save your Aecial Table";

            if (SaveFile.ShowDialog() == DialogResult.OK)
            {
                //Open stream to the file
                Stream saveStream = File.Open(SaveFile.FileName, FileMode.Create);

                //Formatting is in binary (smaller files this way)
                BinaryFormatter BFormatter = new BinaryFormatter();

                //Save number of items
                BFormatter.Serialize(saveStream, TableData.Count);
                //Save our data (Yay for C# class serialization! I don't have to do any real work now)
                foreach (TableEntry Table in TableData)
                    BFormatter.Serialize(saveStream, Table);

                //Close our file stream
                saveStream.Close();
            }
        }

        #endregion

        #region Tools

        //Create process selector
        private void SelectProcessButton_Click(object sender, EventArgs e)
        {
            ProcessWindow SelectProcess = new ProcessWindow();
            SelectProcess.ShowDialog();
            if (SelectProcess.TargetProcess != null)
            {
                //Update our target process
                this.Process = SelectProcess.TargetProcess;

                //If our memory viewer is open, make sure it knows the process has changed
                if (MemoryRegions != null && !MemoryRegions.IsDisposed)
                    MemoryRegions.UpdateProcessHandle(SelectProcess.TargetProcess.Handle);
            }
        }

        private void ScanHistoryButton_Click(object sender, EventArgs e)
        {
            ScanHistory.Show();
        }

        Tools.CPUInfo CPUInfo;
        private void CPUInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CPUInfo == null || CPUInfo.IsDisposed)
                CPUInfo = new Tools.CPUInfo();
            CPUInfo.Show();
        }

        Tools.GUIEditor GUIEditor;
        private void GUIEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GUIEditor == null || GUIEditor.IsDisposed)
                GUIEditor = new Tools.GUIEditor();
            GUIEditor.Show();
        }

        Tools.Macro Macro;
        private void MacrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Macro == null || Macro.IsDisposed)
                Macro = new Tools.Macro();
            Macro.Show();
        }

        Tools.DLLInjector DLLInjector;
        private void DLLInjectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DLLInjector == null || DLLInjector.IsDisposed)
                DLLInjector = new Tools.DLLInjector();
            DLLInjector.Show();
        }

        MemoryRegions MemoryRegions;
        private void MemoryRegionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MemoryRegions == null || MemoryRegions.IsDisposed)
                if (Process != null)
                    MemoryRegions = new MemoryRegions(Process.Handle);
                else
                    MemoryRegions = new MemoryRegions((IntPtr)null);
            MemoryRegions.Show();
        }

        MemoryViewer MemoryViewer;
        private void MemoryViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: Allow multiple memory viewers open?
            if (MemoryViewer == null || MemoryViewer.IsDisposed)
                MemoryViewer = new MemoryViewer(this);
            MemoryViewer.Show();
        }

        #endregion


    }

    public enum ScanDataType
    {
        Binary = 0,
        Byte = 1,
        Int16 = 2,
        Int32 = 3,
        Int64 = 4,
        Single = 5,
        Double = 6,
        Text = 7,
        AOB = 8,
        All = 9
    }
}