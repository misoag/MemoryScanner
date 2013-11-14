namespace AecialEngine
{
    partial class MemoryEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemoryEditor));
            this.StartAddressTextBox = new System.Windows.Forms.TextBox();
            this.EndAddressTextBox = new System.Windows.Forms.TextBox();
            this.ScanValueTextBox = new System.Windows.Forms.TextBox();
            this.ScanValueRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.convertToHexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToDecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScanDataTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.selectedProcessLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.IsHexCB = new System.Windows.Forms.CheckBox();
            this.AddressCount = new System.Windows.Forms.Label();
            this.UpdateFoundTimer = new System.Windows.Forms.Timer(this.components);
            this.WriteTimer = new System.Windows.Forms.Timer(this.components);
            this.GUIMenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cPUInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gUIEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hexEdtiorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.macrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dLLInjectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoryRegionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoryViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TableListView = new System.Windows.Forms.ListView();
            this.CheckBoxHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DescriptionHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AddressHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValueHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AddressListView = new System.Windows.Forms.ListView();
            this._AddressHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._ValueHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AddressListViewRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addItemToTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showItemsAsSignedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TypeCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.ProtectionCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.AllignmentRB = new System.Windows.Forms.RadioButton();
            this.LastDigitsRB = new System.Windows.Forms.RadioButton();
            this.OptimizeScanVal = new System.Windows.Forms.NumericUpDown();
            this.ScanSecondValueTextBox = new System.Windows.Forms.TextBox();
            this.ScanCompareTypeComboBox = new System.Windows.Forms.ComboBox();
            this.CompareTypeLabel = new System.Windows.Forms.Label();
            this.RoundedRB = new System.Windows.Forms.RadioButton();
            this.TruncatedRB = new System.Windows.Forms.RadioButton();
            this.GUIToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ScanTimeLabel = new System.Windows.Forms.Label();
            this.MainToolStrip = new System.Windows.Forms.ToolStrip();
            this.OpenATButton = new System.Windows.Forms.ToolStripButton();
            this.MergeATButton = new System.Windows.Forms.ToolStripButton();
            this.SaveATButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.AddSelectedButton = new System.Windows.Forms.ToolStripButton();
            this.AddSpecificButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.ClearTableButton = new System.Windows.Forms.ToolStripButton();
            this.UndoTableDeleteButton = new System.Windows.Forms.ToolStripButton();
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.SelectProcessButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ScanHistoryButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.NewScanButton = new System.Windows.Forms.ToolStripButton();
            this.StartScanButton = new System.Windows.Forms.ToolStripButton();
            this.NextScanButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.UndoScanButton = new System.Windows.Forms.ToolStripButton();
            this.AbortScanButton = new System.Windows.Forms.ToolStripButton();
            this.label3 = new System.Windows.Forms.Label();
            this.CompareToFirstCB = new System.Windows.Forms.CheckBox();
            this.OptimizeScanCB = new System.Windows.Forms.CheckBox();
            this.ScanValueRightClickMenu.SuspendLayout();
            this.GUIMenuStrip.SuspendLayout();
            this.AddressListViewRightClickMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OptimizeScanVal)).BeginInit();
            this.MainToolStrip.SuspendLayout();
            this.ScanToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartAddressTextBox
            // 
            this.StartAddressTextBox.Location = new System.Drawing.Point(42, 13);
            this.StartAddressTextBox.MaxLength = 8;
            this.StartAddressTextBox.Name = "StartAddressTextBox";
            this.StartAddressTextBox.Size = new System.Drawing.Size(132, 20);
            this.StartAddressTextBox.TabIndex = 0;
            this.StartAddressTextBox.Text = "00000000";
            // 
            // EndAddressTextBox
            // 
            this.EndAddressTextBox.Location = new System.Drawing.Point(42, 30);
            this.EndAddressTextBox.MaxLength = 8;
            this.EndAddressTextBox.Name = "EndAddressTextBox";
            this.EndAddressTextBox.Size = new System.Drawing.Size(132, 20);
            this.EndAddressTextBox.TabIndex = 1;
            this.EndAddressTextBox.Text = "7FFFFFFF";
            // 
            // ScanValueTextBox
            // 
            this.ScanValueTextBox.ContextMenuStrip = this.ScanValueRightClickMenu;
            this.ScanValueTextBox.Location = new System.Drawing.Point(281, 70);
            this.ScanValueTextBox.Name = "ScanValueTextBox";
            this.ScanValueTextBox.Size = new System.Drawing.Size(214, 20);
            this.ScanValueTextBox.TabIndex = 0;
            // 
            // ScanValueRightClickMenu
            // 
            this.ScanValueRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertToHexToolStripMenuItem,
            this.convertToDecToolStripMenuItem});
            this.ScanValueRightClickMenu.Name = "ScanValueRightClickMenu";
            this.ScanValueRightClickMenu.Size = new System.Drawing.Size(154, 48);
            // 
            // convertToHexToolStripMenuItem
            // 
            this.convertToHexToolStripMenuItem.Name = "convertToHexToolStripMenuItem";
            this.convertToHexToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.convertToHexToolStripMenuItem.Text = "Convert to Hex";
            this.convertToHexToolStripMenuItem.Click += new System.EventHandler(this.convertToHexToolStripMenuItem_Click);
            // 
            // convertToDecToolStripMenuItem
            // 
            this.convertToDecToolStripMenuItem.Name = "convertToDecToolStripMenuItem";
            this.convertToDecToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.convertToDecToolStripMenuItem.Text = "Convert to Dec";
            this.convertToDecToolStripMenuItem.Click += new System.EventHandler(this.convertToDecToolStripMenuItem_Click);
            // 
            // ScanDataTypeComboBox
            // 
            this.ScanDataTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ScanDataTypeComboBox.FormattingEnabled = true;
            this.ScanDataTypeComboBox.Items.AddRange(new object[] {
            "Binary",
            "Byte",
            "Int 16",
            "Int 32",
            "Int 64",
            "Single",
            "Double",
            "Text",
            "Array of Bytes",
            "All (Byte to Double)"});
            this.ScanDataTypeComboBox.Location = new System.Drawing.Point(281, 117);
            this.ScanDataTypeComboBox.Name = "ScanDataTypeComboBox";
            this.ScanDataTypeComboBox.Size = new System.Drawing.Size(160, 21);
            this.ScanDataTypeComboBox.TabIndex = 3;
            this.ScanDataTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.ScanTypeComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(7, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "To";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(241, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Data:";
            // 
            // ProgressBar
            // 
            this.ProgressBar.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ProgressBar.ForeColor = System.Drawing.Color.SteelBlue;
            this.ProgressBar.Location = new System.Drawing.Point(0, 27);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(214, 16);
            this.ProgressBar.Step = 1;
            this.ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.ProgressBar.TabIndex = 17;
            // 
            // selectedProcessLabel
            // 
            this.selectedProcessLabel.AutoSize = true;
            this.selectedProcessLabel.Location = new System.Drawing.Point(219, 6);
            this.selectedProcessLabel.Name = "selectedProcessLabel";
            this.selectedProcessLabel.Size = new System.Drawing.Size(107, 13);
            this.selectedProcessLabel.TabIndex = 19;
            this.selectedProcessLabel.Text = "No Process Selected";
            this.selectedProcessLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(223, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Compare:";
            // 
            // IsHexCB
            // 
            this.IsHexCB.AutoSize = true;
            this.IsHexCB.Location = new System.Drawing.Point(222, 72);
            this.IsHexCB.Name = "IsHexCB";
            this.IsHexCB.Size = new System.Drawing.Size(56, 17);
            this.IsHexCB.TabIndex = 7;
            this.IsHexCB.Text = "Value:";
            this.IsHexCB.UseVisualStyleBackColor = true;
            // 
            // AddressCount
            // 
            this.AddressCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AddressCount.Location = new System.Drawing.Point(0, 272);
            this.AddressCount.Name = "AddressCount";
            this.AddressCount.Size = new System.Drawing.Size(214, 17);
            this.AddressCount.TabIndex = 33;
            this.AddressCount.Text = "Items: 0";
            // 
            // UpdateFoundTimer
            // 
            this.UpdateFoundTimer.Enabled = true;
            this.UpdateFoundTimer.Interval = 513;
            this.UpdateFoundTimer.Tick += new System.EventHandler(this.updateFoundTimer_Tick);
            // 
            // WriteTimer
            // 
            this.WriteTimer.Enabled = true;
            this.WriteTimer.Interval = 193;
            this.WriteTimer.Tick += new System.EventHandler(this.WriteTimer_Tick);
            // 
            // GUIMenuStrip
            // 
            this.GUIMenuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.GUIMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.ProcessToolStripMenuItem,
            this.ToolsToolStripMenuItem});
            this.GUIMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.GUIMenuStrip.Name = "GUIMenuStrip";
            this.GUIMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.GUIMenuStrip.Size = new System.Drawing.Size(561, 24);
            this.GUIMenuStrip.TabIndex = 46;
            this.GUIMenuStrip.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem,
            this.dieToolStripMenuItem,
            this.openToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileToolStripMenuItem.Text = "File";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.testToolStripMenuItem.Text = "New";
            // 
            // dieToolStripMenuItem
            // 
            this.dieToolStripMenuItem.Name = "dieToolStripMenuItem";
            this.dieToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.dieToolStripMenuItem.Text = "Save";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // ProcessToolStripMenuItem
            // 
            this.ProcessToolStripMenuItem.Name = "ProcessToolStripMenuItem";
            this.ProcessToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.ProcessToolStripMenuItem.Text = "Process";
            // 
            // ToolsToolStripMenuItem
            // 
            this.ToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cPUInfoToolStripMenuItem,
            this.gUIEditorToolStripMenuItem,
            this.hexEdtiorToolStripMenuItem,
            this.macrosToolStripMenuItem,
            this.dLLInjectorToolStripMenuItem,
            this.memoryRegionsToolStripMenuItem,
            this.memoryViewerToolStripMenuItem});
            this.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem";
            this.ToolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.ToolsToolStripMenuItem.Text = "Tools";
            // 
            // cPUInfoToolStripMenuItem
            // 
            this.cPUInfoToolStripMenuItem.Name = "cPUInfoToolStripMenuItem";
            this.cPUInfoToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.cPUInfoToolStripMenuItem.Text = "CPU Info";
            this.cPUInfoToolStripMenuItem.Click += new System.EventHandler(this.CPUInfoToolStripMenuItem_Click);
            // 
            // gUIEditorToolStripMenuItem
            // 
            this.gUIEditorToolStripMenuItem.Name = "gUIEditorToolStripMenuItem";
            this.gUIEditorToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.gUIEditorToolStripMenuItem.Text = "GUI Editor";
            this.gUIEditorToolStripMenuItem.Click += new System.EventHandler(this.GUIEditorToolStripMenuItem_Click);
            // 
            // hexEdtiorToolStripMenuItem
            // 
            this.hexEdtiorToolStripMenuItem.Name = "hexEdtiorToolStripMenuItem";
            this.hexEdtiorToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.hexEdtiorToolStripMenuItem.Text = "Hex Edtior";
            // 
            // macrosToolStripMenuItem
            // 
            this.macrosToolStripMenuItem.Name = "macrosToolStripMenuItem";
            this.macrosToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.macrosToolStripMenuItem.Text = "Macros";
            this.macrosToolStripMenuItem.Click += new System.EventHandler(this.MacrosToolStripMenuItem_Click);
            // 
            // dLLInjectorToolStripMenuItem
            // 
            this.dLLInjectorToolStripMenuItem.Name = "dLLInjectorToolStripMenuItem";
            this.dLLInjectorToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.dLLInjectorToolStripMenuItem.Text = "DLL Injector";
            this.dLLInjectorToolStripMenuItem.Click += new System.EventHandler(this.DLLInjectorToolStripMenuItem_Click);
            // 
            // memoryRegionsToolStripMenuItem
            // 
            this.memoryRegionsToolStripMenuItem.Name = "memoryRegionsToolStripMenuItem";
            this.memoryRegionsToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.memoryRegionsToolStripMenuItem.Text = "Memory Regions";
            this.memoryRegionsToolStripMenuItem.Click += new System.EventHandler(this.MemoryRegionsToolStripMenuItem_Click);
            // 
            // memoryViewerToolStripMenuItem
            // 
            this.memoryViewerToolStripMenuItem.Name = "memoryViewerToolStripMenuItem";
            this.memoryViewerToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.memoryViewerToolStripMenuItem.Text = "Memory Viewer";
            this.memoryViewerToolStripMenuItem.Click += new System.EventHandler(this.MemoryViewerToolStripMenuItem_Click);
            // 
            // TableListView
            // 
            this.TableListView.BackColor = System.Drawing.SystemColors.Control;
            this.TableListView.CheckBoxes = true;
            this.TableListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CheckBoxHeader,
            this.DescriptionHeader,
            this.AddressHeader,
            this.TypeHeader,
            this.ValueHeader});
            this.TableListView.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TableListView.FullRowSelect = true;
            this.TableListView.Location = new System.Drawing.Point(0, 312);
            this.TableListView.Name = "TableListView";
            this.TableListView.ShowGroups = false;
            this.TableListView.Size = new System.Drawing.Size(561, 289);
            this.TableListView.TabIndex = 47;
            this.TableListView.UseCompatibleStateImageBehavior = false;
            this.TableListView.View = System.Windows.Forms.View.Details;
            this.TableListView.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.addedListView_ItemCheck);
            this.TableListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.addedListView_MouseDoubleClick);
            // 
            // CheckBoxHeader
            // 
            this.CheckBoxHeader.Text = "";
            this.CheckBoxHeader.Width = 24;
            // 
            // DescriptionHeader
            // 
            this.DescriptionHeader.Text = "Description";
            this.DescriptionHeader.Width = 146;
            // 
            // AddressHeader
            // 
            this.AddressHeader.Text = "Address";
            this.AddressHeader.Width = 93;
            // 
            // TypeHeader
            // 
            this.TypeHeader.Text = "Type";
            this.TypeHeader.Width = 70;
            // 
            // ValueHeader
            // 
            this.ValueHeader.Text = "Value";
            this.ValueHeader.Width = 204;
            // 
            // AddressListView
            // 
            this.AddressListView.BackColor = System.Drawing.SystemColors.Control;
            this.AddressListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._AddressHeader,
            this._ValueHeader});
            this.AddressListView.ContextMenuStrip = this.AddressListViewRightClickMenu;
            this.AddressListView.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddressListView.FullRowSelect = true;
            this.AddressListView.Location = new System.Drawing.Point(0, 46);
            this.AddressListView.Name = "AddressListView";
            this.AddressListView.Size = new System.Drawing.Size(214, 227);
            this.AddressListView.TabIndex = 48;
            this.AddressListView.UseCompatibleStateImageBehavior = false;
            this.AddressListView.View = System.Windows.Forms.View.Details;
            this.AddressListView.VirtualMode = true;
            this.AddressListView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.addressListView_RetrieveVirtualItem);
            this.AddressListView.DoubleClick += new System.EventHandler(this.addressListView_DoubleClick);
            this.AddressListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AddressListView_MouseDown);
            // 
            // _AddressHeader
            // 
            this._AddressHeader.Text = "Address";
            this._AddressHeader.Width = 72;
            // 
            // _ValueHeader
            // 
            this._ValueHeader.Text = "Value";
            this._ValueHeader.Width = 134;
            // 
            // AddressListViewRightClickMenu
            // 
            this.AddressListViewRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addItemToTableToolStripMenuItem,
            this.showItemsAsSignedToolStripMenuItem});
            this.AddressListViewRightClickMenu.Name = "AddressListViewRightClickMenu";
            this.AddressListViewRightClickMenu.Size = new System.Drawing.Size(189, 48);
            // 
            // addItemToTableToolStripMenuItem
            // 
            this.addItemToTableToolStripMenuItem.Name = "addItemToTableToolStripMenuItem";
            this.addItemToTableToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.addItemToTableToolStripMenuItem.Text = "Add Item to Table";
            this.addItemToTableToolStripMenuItem.Click += new System.EventHandler(this.addItemToTableToolStripMenuItem_Click);
            // 
            // showItemsAsSignedToolStripMenuItem
            // 
            this.showItemsAsSignedToolStripMenuItem.Name = "showItemsAsSignedToolStripMenuItem";
            this.showItemsAsSignedToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.showItemsAsSignedToolStripMenuItem.Text = "Show Items as Signed";
            this.showItemsAsSignedToolStripMenuItem.Click += new System.EventHandler(this.showItemsAsSignedToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.TypeCheckedListBox);
            this.groupBox1.Controls.Add(this.ProtectionCheckedListBox);
            this.groupBox1.Controls.Add(this.OptimizeScanCB);
            this.groupBox1.Controls.Add(this.AllignmentRB);
            this.groupBox1.Controls.Add(this.LastDigitsRB);
            this.groupBox1.Controls.Add(this.OptimizeScanVal);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.StartAddressTextBox);
            this.groupBox1.Controls.Add(this.EndAddressTextBox);
            this.groupBox1.Location = new System.Drawing.Point(222, 171);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(330, 118);
            this.groupBox1.TabIndex = 49;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Advanced Options";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(212, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 63;
            this.label7.Text = "Memory Type";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 13);
            this.label6.TabIndex = 63;
            this.label6.Text = "Memory Protection";
            // 
            // TypeCheckedListBox
            // 
            this.TypeCheckedListBox.FormattingEnabled = true;
            this.TypeCheckedListBox.Items.AddRange(new object[] {
            "Private",
            "Image",
            "Mapped"});
            this.TypeCheckedListBox.Location = new System.Drawing.Point(180, 78);
            this.TypeCheckedListBox.Name = "TypeCheckedListBox";
            this.TypeCheckedListBox.Size = new System.Drawing.Size(144, 34);
            this.TypeCheckedListBox.TabIndex = 7;
            // 
            // ProtectionCheckedListBox
            // 
            this.ProtectionCheckedListBox.FormattingEnabled = true;
            this.ProtectionCheckedListBox.Items.AddRange(new object[] {
            "No Access",
            "Read Only",
            "Read+Write",
            "WriteCopy",
            "Executable",
            "Executable+Read",
            "Executable+Read+Write",
            "Executable+WriteCopy"});
            this.ProtectionCheckedListBox.Location = new System.Drawing.Point(9, 78);
            this.ProtectionCheckedListBox.Name = "ProtectionCheckedListBox";
            this.ProtectionCheckedListBox.Size = new System.Drawing.Size(165, 34);
            this.ProtectionCheckedListBox.TabIndex = 6;
            // 
            // AllignmentRB
            // 
            this.AllignmentRB.AutoSize = true;
            this.AllignmentRB.Checked = true;
            this.AllignmentRB.Location = new System.Drawing.Point(187, 26);
            this.AllignmentRB.Name = "AllignmentRB";
            this.AllignmentRB.Size = new System.Drawing.Size(71, 17);
            this.AllignmentRB.TabIndex = 4;
            this.AllignmentRB.TabStop = true;
            this.AllignmentRB.Text = "Alignment";
            this.AllignmentRB.UseVisualStyleBackColor = true;
            // 
            // LastDigitsRB
            // 
            this.LastDigitsRB.AutoSize = true;
            this.LastDigitsRB.Location = new System.Drawing.Point(187, 40);
            this.LastDigitsRB.Name = "LastDigitsRB";
            this.LastDigitsRB.Size = new System.Drawing.Size(74, 17);
            this.LastDigitsRB.TabIndex = 5;
            this.LastDigitsRB.Text = "Last Digits";
            this.LastDigitsRB.UseVisualStyleBackColor = true;
            // 
            // OptimizeScanVal
            // 
            this.OptimizeScanVal.Hexadecimal = true;
            this.OptimizeScanVal.Location = new System.Drawing.Point(261, 32);
            this.OptimizeScanVal.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.OptimizeScanVal.Name = "OptimizeScanVal";
            this.OptimizeScanVal.Size = new System.Drawing.Size(63, 20);
            this.OptimizeScanVal.TabIndex = 3;
            this.OptimizeScanVal.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // ScanSecondValueTextBox
            // 
            this.ScanSecondValueTextBox.ContextMenuStrip = this.ScanValueRightClickMenu;
            this.ScanSecondValueTextBox.Location = new System.Drawing.Point(391, 70);
            this.ScanSecondValueTextBox.Name = "ScanSecondValueTextBox";
            this.ScanSecondValueTextBox.Size = new System.Drawing.Size(104, 20);
            this.ScanSecondValueTextBox.TabIndex = 1;
            this.ScanSecondValueTextBox.Visible = false;
            // 
            // ScanCompareTypeComboBox
            // 
            this.ScanCompareTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ScanCompareTypeComboBox.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScanCompareTypeComboBox.FormattingEnabled = true;
            this.ScanCompareTypeComboBox.Items.AddRange(new object[] {
            "=",
            "Ø",
            "±",
            "≠",
            "+",
            "-",
            "+x",
            "-x",
            "<",
            ">",
            "≤",
            "≥",
            "> <",
            "≥ ≤",
            "??"});
            this.ScanCompareTypeComboBox.Location = new System.Drawing.Point(281, 93);
            this.ScanCompareTypeComboBox.Name = "ScanCompareTypeComboBox";
            this.ScanCompareTypeComboBox.Size = new System.Drawing.Size(48, 23);
            this.ScanCompareTypeComboBox.TabIndex = 2;
            this.ScanCompareTypeComboBox.Tag = "VALUES ALTERED BY METHODS";
            this.ScanCompareTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.ScanCompareTypeComboBox_SelectedIndexChanged);
            // 
            // CompareTypeLabel
            // 
            this.CompareTypeLabel.AutoSize = true;
            this.CompareTypeLabel.Location = new System.Drawing.Point(331, 97);
            this.CompareTypeLabel.Name = "CompareTypeLabel";
            this.CompareTypeLabel.Size = new System.Drawing.Size(112, 13);
            this.CompareTypeLabel.TabIndex = 55;
            this.CompareTypeLabel.Text = "Greater Than or Equal";
            // 
            // RoundedRB
            // 
            this.RoundedRB.AutoSize = true;
            this.RoundedRB.Location = new System.Drawing.Point(447, 116);
            this.RoundedRB.Name = "RoundedRB";
            this.RoundedRB.Size = new System.Drawing.Size(69, 17);
            this.RoundedRB.TabIndex = 5;
            this.RoundedRB.Text = "Rounded";
            this.RoundedRB.UseVisualStyleBackColor = true;
            // 
            // TruncatedRB
            // 
            this.TruncatedRB.AutoSize = true;
            this.TruncatedRB.Checked = true;
            this.TruncatedRB.Location = new System.Drawing.Point(447, 102);
            this.TruncatedRB.Name = "TruncatedRB";
            this.TruncatedRB.Size = new System.Drawing.Size(74, 17);
            this.TruncatedRB.TabIndex = 4;
            this.TruncatedRB.TabStop = true;
            this.TruncatedRB.Text = "Truncated";
            this.TruncatedRB.UseVisualStyleBackColor = true;
            // 
            // ScanTimeLabel
            // 
            this.ScanTimeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ScanTimeLabel.Location = new System.Drawing.Point(123, 272);
            this.ScanTimeLabel.Name = "ScanTimeLabel";
            this.ScanTimeLabel.Size = new System.Drawing.Size(91, 17);
            this.ScanTimeLabel.TabIndex = 58;
            this.ScanTimeLabel.Text = "Time: 0";
            // 
            // MainToolStrip
            // 
            this.MainToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.MainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenATButton,
            this.MergeATButton,
            this.SaveATButton,
            this.toolStripSeparator3,
            this.AddSelectedButton,
            this.AddSpecificButton,
            this.toolStripSeparator5,
            this.ClearTableButton,
            this.UndoTableDeleteButton});
            this.MainToolStrip.Location = new System.Drawing.Point(0, 288);
            this.MainToolStrip.Name = "MainToolStrip";
            this.MainToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.MainToolStrip.Size = new System.Drawing.Size(176, 25);
            this.MainToolStrip.TabIndex = 60;
            // 
            // OpenATButton
            // 
            this.OpenATButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenATButton.Image = ((System.Drawing.Image)(resources.GetObject("OpenATButton.Image")));
            this.OpenATButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenATButton.Name = "OpenATButton";
            this.OpenATButton.Size = new System.Drawing.Size(23, 22);
            this.OpenATButton.Click += new System.EventHandler(this.OpenATButton_Click);
            // 
            // MergeATButton
            // 
            this.MergeATButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MergeATButton.Image = ((System.Drawing.Image)(resources.GetObject("MergeATButton.Image")));
            this.MergeATButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MergeATButton.Name = "MergeATButton";
            this.MergeATButton.Size = new System.Drawing.Size(23, 22);
            this.MergeATButton.Click += new System.EventHandler(this.MergeATButton_Click);
            // 
            // SaveATButton
            // 
            this.SaveATButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveATButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveATButton.Image")));
            this.SaveATButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveATButton.Name = "SaveATButton";
            this.SaveATButton.Size = new System.Drawing.Size(23, 22);
            this.SaveATButton.Click += new System.EventHandler(this.SaveATButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // AddSelectedButton
            // 
            this.AddSelectedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddSelectedButton.Image = ((System.Drawing.Image)(resources.GetObject("AddSelectedButton.Image")));
            this.AddSelectedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddSelectedButton.Name = "AddSelectedButton";
            this.AddSelectedButton.Size = new System.Drawing.Size(23, 22);
            this.AddSelectedButton.Click += new System.EventHandler(this.AddSelectedButton_Click);
            // 
            // AddSpecificButton
            // 
            this.AddSpecificButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddSpecificButton.Image = ((System.Drawing.Image)(resources.GetObject("AddSpecificButton.Image")));
            this.AddSpecificButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddSpecificButton.Name = "AddSpecificButton";
            this.AddSpecificButton.Size = new System.Drawing.Size(23, 22);
            this.AddSpecificButton.Click += new System.EventHandler(this.AddSpecificButton_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // ClearTableButton
            // 
            this.ClearTableButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ClearTableButton.Enabled = false;
            this.ClearTableButton.Image = ((System.Drawing.Image)(resources.GetObject("ClearTableButton.Image")));
            this.ClearTableButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClearTableButton.Name = "ClearTableButton";
            this.ClearTableButton.Size = new System.Drawing.Size(23, 22);
            this.ClearTableButton.Click += new System.EventHandler(this.ClearTableButton_Click);
            // 
            // UndoTableDeleteButton
            // 
            this.UndoTableDeleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UndoTableDeleteButton.Enabled = false;
            this.UndoTableDeleteButton.Image = ((System.Drawing.Image)(resources.GetObject("UndoTableDeleteButton.Image")));
            this.UndoTableDeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UndoTableDeleteButton.Name = "UndoTableDeleteButton";
            this.UndoTableDeleteButton.Size = new System.Drawing.Size(23, 22);
            this.UndoTableDeleteButton.Click += new System.EventHandler(this.UndoTableDeleteButton_Click);
            // 
            // ScanToolStrip
            // 
            this.ScanToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.ScanToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectProcessButton,
            this.toolStripSeparator4,
            this.ScanHistoryButton,
            this.toolStripSeparator1,
            this.NewScanButton,
            this.StartScanButton,
            this.NextScanButton,
            this.toolStripSeparator2,
            this.UndoScanButton,
            this.AbortScanButton});
            this.ScanToolStrip.Location = new System.Drawing.Point(281, 46);
            this.ScanToolStrip.Name = "ScanToolStrip";
            this.ScanToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanToolStrip.Size = new System.Drawing.Size(182, 25);
            this.ScanToolStrip.TabIndex = 61;
            this.ScanToolStrip.Text = "toolStrip1";
            // 
            // SelectProcessButton
            // 
            this.SelectProcessButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SelectProcessButton.Image = ((System.Drawing.Image)(resources.GetObject("SelectProcessButton.Image")));
            this.SelectProcessButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SelectProcessButton.Name = "SelectProcessButton";
            this.SelectProcessButton.Size = new System.Drawing.Size(23, 22);
            this.SelectProcessButton.Click += new System.EventHandler(this.SelectProcessButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // ScanHistoryButton
            // 
            this.ScanHistoryButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ScanHistoryButton.Image = ((System.Drawing.Image)(resources.GetObject("ScanHistoryButton.Image")));
            this.ScanHistoryButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ScanHistoryButton.Name = "ScanHistoryButton";
            this.ScanHistoryButton.Size = new System.Drawing.Size(23, 22);
            this.ScanHistoryButton.Click += new System.EventHandler(this.ScanHistoryButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // NewScanButton
            // 
            this.NewScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewScanButton.Enabled = false;
            this.NewScanButton.Image = ((System.Drawing.Image)(resources.GetObject("NewScanButton.Image")));
            this.NewScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewScanButton.Name = "NewScanButton";
            this.NewScanButton.Size = new System.Drawing.Size(23, 22);
            this.NewScanButton.Click += new System.EventHandler(this.NewScanButton_Click);
            // 
            // StartScanButton
            // 
            this.StartScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StartScanButton.Image = ((System.Drawing.Image)(resources.GetObject("StartScanButton.Image")));
            this.StartScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StartScanButton.Name = "StartScanButton";
            this.StartScanButton.Size = new System.Drawing.Size(23, 22);
            this.StartScanButton.Click += new System.EventHandler(this.StartScanButton_Click);
            // 
            // NextScanButton
            // 
            this.NextScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NextScanButton.Enabled = false;
            this.NextScanButton.Image = ((System.Drawing.Image)(resources.GetObject("NextScanButton.Image")));
            this.NextScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NextScanButton.Name = "NextScanButton";
            this.NextScanButton.Size = new System.Drawing.Size(23, 22);
            this.NextScanButton.Click += new System.EventHandler(this.NextScanButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // UndoScanButton
            // 
            this.UndoScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UndoScanButton.Enabled = false;
            this.UndoScanButton.Image = ((System.Drawing.Image)(resources.GetObject("UndoScanButton.Image")));
            this.UndoScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UndoScanButton.Name = "UndoScanButton";
            this.UndoScanButton.Size = new System.Drawing.Size(23, 22);
            // 
            // AbortScanButton
            // 
            this.AbortScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AbortScanButton.Enabled = false;
            this.AbortScanButton.Image = ((System.Drawing.Image)(resources.GetObject("AbortScanButton.Image")));
            this.AbortScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AbortScanButton.Name = "AbortScanButton";
            this.AbortScanButton.Size = new System.Drawing.Size(23, 22);
            this.AbortScanButton.Click += new System.EventHandler(this.AbortScanButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(256, 293);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 62;
            this.label3.Text = "Table";
            // 
            // CompareToFirstCB
            // 
            this.CompareToFirstCB.AutoSize = true;
            this.CompareToFirstCB.Location = new System.Drawing.Point(281, 148);
            this.CompareToFirstCB.Name = "CompareToFirstCB";
            this.CompareToFirstCB.Size = new System.Drawing.Size(130, 17);
            this.CompareToFirstCB.TabIndex = 6;
            this.CompareToFirstCB.Text = "Compare to First Scan";
            this.CompareToFirstCB.UseVisualStyleBackColor = true;
            // 
            // OptimizeScanCB
            // 
            this.OptimizeScanCB.AutoSize = true;
            this.OptimizeScanCB.Checked = true;
            this.OptimizeScanCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.OptimizeScanCB.Location = new System.Drawing.Point(180, 12);
            this.OptimizeScanCB.Name = "OptimizeScanCB";
            this.OptimizeScanCB.Size = new System.Drawing.Size(94, 17);
            this.OptimizeScanCB.TabIndex = 2;
            this.OptimizeScanCB.Text = "Optimize Scan";
            this.OptimizeScanCB.UseVisualStyleBackColor = true;
            this.OptimizeScanCB.CheckedChanged += new System.EventHandler(this.OptimizeScanCB_CheckedChanged);
            // 
            // MemoryEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 604);
            this.Controls.Add(this.CompareToFirstCB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ScanTimeLabel);
            this.Controls.Add(this.AddressCount);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.AddressListView);
            this.Controls.Add(this.TableListView);
            this.Controls.Add(this.selectedProcessLabel);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.TruncatedRB);
            this.Controls.Add(this.RoundedRB);
            this.Controls.Add(this.CompareTypeLabel);
            this.Controls.Add(this.IsHexCB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ScanCompareTypeComboBox);
            this.Controls.Add(this.ScanDataTypeComboBox);
            this.Controls.Add(this.ScanSecondValueTextBox);
            this.Controls.Add(this.ScanValueTextBox);
            this.Controls.Add(this.GUIMenuStrip);
            this.Controls.Add(this.ScanToolStrip);
            this.Controls.Add(this.MainToolStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MemoryEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MemoryEditor_Load);
            this.ScanValueRightClickMenu.ResumeLayout(false);
            this.GUIMenuStrip.ResumeLayout(false);
            this.GUIMenuStrip.PerformLayout();
            this.AddressListViewRightClickMenu.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OptimizeScanVal)).EndInit();
            this.MainToolStrip.ResumeLayout(false);
            this.MainToolStrip.PerformLayout();
            this.ScanToolStrip.ResumeLayout(false);
            this.ScanToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox StartAddressTextBox;
        private System.Windows.Forms.TextBox EndAddressTextBox;
        private System.Windows.Forms.TextBox ScanValueTextBox;
        private System.Windows.Forms.ComboBox ScanDataTypeComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Label selectedProcessLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox IsHexCB;
        private System.Windows.Forms.Label AddressCount;
        private System.Windows.Forms.Timer UpdateFoundTimer;
        private System.Windows.Forms.Timer WriteTimer;
        private System.Windows.Forms.MenuStrip GUIMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dieToolStripMenuItem;
        private System.Windows.Forms.ListView TableListView;
        private System.Windows.Forms.ColumnHeader CheckBoxHeader;
        private System.Windows.Forms.ColumnHeader DescriptionHeader;
        private System.Windows.Forms.ColumnHeader AddressHeader;
        private System.Windows.Forms.ColumnHeader TypeHeader;
        private System.Windows.Forms.ColumnHeader ValueHeader;
        private System.Windows.Forms.ListView AddressListView;
        private System.Windows.Forms.ColumnHeader _AddressHeader;
        private System.Windows.Forms.ColumnHeader _ValueHeader;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cPUInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gUIEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hexEdtiorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem macrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dLLInjectorToolStripMenuItem;
        private System.Windows.Forms.RadioButton LastDigitsRB;
        private System.Windows.Forms.RadioButton AllignmentRB;
        private System.Windows.Forms.NumericUpDown OptimizeScanVal;
        private System.Windows.Forms.TextBox ScanSecondValueTextBox;
        private System.Windows.Forms.ComboBox ScanCompareTypeComboBox;
        private System.Windows.Forms.Label CompareTypeLabel;
        private System.Windows.Forms.RadioButton RoundedRB;
        private System.Windows.Forms.RadioButton TruncatedRB;
        private System.Windows.Forms.ToolTip GUIToolTip;
        private System.Windows.Forms.ToolStripMenuItem memoryRegionsToolStripMenuItem;
        private System.Windows.Forms.Label ScanTimeLabel;
        private System.Windows.Forms.ToolStrip MainToolStrip;
        private System.Windows.Forms.ToolStripButton OpenATButton;
        private System.Windows.Forms.ToolStripButton MergeATButton;
        private System.Windows.Forms.ToolStripButton SaveATButton;
        private System.Windows.Forms.ToolStrip ScanToolStrip;
        private System.Windows.Forms.ToolStripButton StartScanButton;
        private System.Windows.Forms.ToolStripButton NextScanButton;
        private System.Windows.Forms.ToolStripButton UndoScanButton;
        private System.Windows.Forms.ToolStripButton AbortScanButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton NewScanButton;
        private System.Windows.Forms.ToolStripButton ScanHistoryButton;
        private System.Windows.Forms.ToolStripButton SelectProcessButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.CheckedListBox ProtectionCheckedListBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem memoryViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton AddSelectedButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton AddSpecificButton;
        private System.Windows.Forms.ContextMenuStrip ScanValueRightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem convertToHexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToDecToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton ClearTableButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckedListBox TypeCheckedListBox;
        private System.Windows.Forms.ToolStripButton UndoTableDeleteButton;
        private System.Windows.Forms.CheckBox CompareToFirstCB;
        private System.Windows.Forms.ContextMenuStrip AddressListViewRightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem addItemToTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showItemsAsSignedToolStripMenuItem;
        private System.Windows.Forms.CheckBox OptimizeScanCB;
    }
}