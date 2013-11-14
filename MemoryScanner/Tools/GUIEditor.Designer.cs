namespace AecialEngine.Tools
{
    partial class GUIEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIEditor));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.IgnoreParentButton = new System.Windows.Forms.Button();
            this.IgnoreChildButton = new System.Windows.Forms.Button();
            this.GrabTypeComboBox = new System.Windows.Forms.ComboBox();
            this.ChildWFPExRB = new System.Windows.Forms.RadioButton();
            this.RealChildWFPRB = new System.Windows.Forms.RadioButton();
            this.ChildWFPRB = new System.Windows.Forms.RadioButton();
            this.ProcessIDLabel = new System.Windows.Forms.Label();
            this.ParentTextLabel = new System.Windows.Forms.Label();
            this.ChildTextLabel = new System.Windows.Forms.Label();
            this.ParentHandleLabel = new System.Windows.Forms.Label();
            this.ChildHandleLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mouseCoordsLabel = new System.Windows.Forms.Label();
            this.SendMsgButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SpecificMessageRB = new System.Windows.Forms.RadioButton();
            this.CommonMessageRB = new System.Windows.Forms.RadioButton();
            this.SpecificTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LParamTextBox = new System.Windows.Forms.TextBox();
            this.WParamTextBox = new System.Windows.Forms.TextBox();
            this.MessageComboBox = new System.Windows.Forms.ComboBox();
            this.EnableButton = new System.Windows.Forms.Button();
            this.ShowButton = new System.Windows.Forms.Button();
            this.ToFrontButton = new System.Windows.Forms.Button();
            this.MaximizeButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.DisableButton = new System.Windows.Forms.Button();
            this.HideButton = new System.Windows.Forms.Button();
            this.ToBackButton = new System.Windows.Forms.Button();
            this.MinimizeButton = new System.Windows.Forms.Button();
            this.RestoreButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.PanRightButton = new System.Windows.Forms.Button();
            this.PanDownButton = new System.Windows.Forms.Button();
            this.PanLeftButton = new System.Windows.Forms.Button();
            this.PanUpButton = new System.Windows.Forms.Button();
            this.HTextBox = new System.Windows.Forms.TextBox();
            this.WTextBox = new System.Windows.Forms.TextBox();
            this.YTextBox = new System.Windows.Forms.TextBox();
            this.XTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.GUIToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.PanHoldTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.IgnoreParentButton);
            this.groupBox1.Controls.Add(this.IgnoreChildButton);
            this.groupBox1.Controls.Add(this.GrabTypeComboBox);
            this.groupBox1.Controls.Add(this.ChildWFPExRB);
            this.groupBox1.Controls.Add(this.RealChildWFPRB);
            this.groupBox1.Controls.Add(this.ChildWFPRB);
            this.groupBox1.Controls.Add(this.ProcessIDLabel);
            this.groupBox1.Controls.Add(this.ParentTextLabel);
            this.groupBox1.Controls.Add(this.ChildTextLabel);
            this.groupBox1.Controls.Add(this.ParentHandleLabel);
            this.groupBox1.Controls.Add(this.ChildHandleLabel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(353, 175);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Window Details";
            // 
            // IgnoreParentButton
            // 
            this.IgnoreParentButton.Location = new System.Drawing.Point(217, 48);
            this.IgnoreParentButton.Name = "IgnoreParentButton";
            this.IgnoreParentButton.Size = new System.Drawing.Size(47, 23);
            this.IgnoreParentButton.TabIndex = 22;
            this.IgnoreParentButton.Text = "Ignore";
            this.IgnoreParentButton.UseVisualStyleBackColor = true;
            this.IgnoreParentButton.Click += new System.EventHandler(this.ignoreParent_Click);
            // 
            // IgnoreChildButton
            // 
            this.IgnoreChildButton.Location = new System.Drawing.Point(217, 22);
            this.IgnoreChildButton.Name = "IgnoreChildButton";
            this.IgnoreChildButton.Size = new System.Drawing.Size(47, 23);
            this.IgnoreChildButton.TabIndex = 21;
            this.IgnoreChildButton.Text = "Ignore";
            this.IgnoreChildButton.UseVisualStyleBackColor = true;
            this.IgnoreChildButton.Click += new System.EventHandler(this.ignoreChild_Click);
            // 
            // GrabTypeComboBox
            // 
            this.GrabTypeComboBox.FormattingEnabled = true;
            this.GrabTypeComboBox.Items.AddRange(new object[] {
            "Grab All",
            "Skip Invisible",
            "Skip Disabled",
            "Skip Transparent"});
            this.GrabTypeComboBox.Location = new System.Drawing.Point(219, 77);
            this.GrabTypeComboBox.Name = "GrabTypeComboBox";
            this.GrabTypeComboBox.Size = new System.Drawing.Size(113, 21);
            this.GrabTypeComboBox.TabIndex = 5;
            // 
            // ChildWFPExRB
            // 
            this.ChildWFPExRB.AutoSize = true;
            this.ChildWFPExRB.Checked = true;
            this.ChildWFPExRB.Location = new System.Drawing.Point(278, 58);
            this.ChildWFPExRB.Name = "ChildWFPExRB";
            this.ChildWFPExRB.Size = new System.Drawing.Size(60, 17);
            this.ChildWFPExRB.TabIndex = 4;
            this.ChildWFPExRB.TabStop = true;
            this.ChildWFPExRB.Text = "ChildEx";
            this.ChildWFPExRB.UseVisualStyleBackColor = true;
            this.ChildWFPExRB.CheckedChanged += new System.EventHandler(this.childWFPExRB_CheckedChanged);
            // 
            // RealChildWFPRB
            // 
            this.RealChildWFPRB.AutoSize = true;
            this.RealChildWFPRB.Location = new System.Drawing.Point(278, 35);
            this.RealChildWFPRB.Name = "RealChildWFPRB";
            this.RealChildWFPRB.Size = new System.Drawing.Size(70, 17);
            this.RealChildWFPRB.TabIndex = 4;
            this.RealChildWFPRB.Text = "RealChild";
            this.RealChildWFPRB.UseVisualStyleBackColor = true;
            this.RealChildWFPRB.CheckedChanged += new System.EventHandler(this.realChildWFPRB_CheckedChanged);
            // 
            // ChildWFPRB
            // 
            this.ChildWFPRB.AutoSize = true;
            this.ChildWFPRB.Location = new System.Drawing.Point(278, 12);
            this.ChildWFPRB.Name = "ChildWFPRB";
            this.ChildWFPRB.Size = new System.Drawing.Size(48, 17);
            this.ChildWFPRB.TabIndex = 4;
            this.ChildWFPRB.Text = "Child";
            this.ChildWFPRB.UseVisualStyleBackColor = true;
            this.ChildWFPRB.CheckedChanged += new System.EventHandler(this.childWFPRB_CheckedChanged);
            // 
            // ProcessIDLabel
            // 
            this.ProcessIDLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ProcessIDLabel.Location = new System.Drawing.Point(96, 79);
            this.ProcessIDLabel.Name = "ProcessIDLabel";
            this.ProcessIDLabel.Size = new System.Drawing.Size(105, 20);
            this.ProcessIDLabel.TabIndex = 3;
            // 
            // ParentTextLabel
            // 
            this.ParentTextLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ParentTextLabel.Location = new System.Drawing.Point(96, 141);
            this.ParentTextLabel.Name = "ParentTextLabel";
            this.ParentTextLabel.Size = new System.Drawing.Size(251, 20);
            this.ParentTextLabel.TabIndex = 3;
            // 
            // ChildTextLabel
            // 
            this.ChildTextLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ChildTextLabel.Location = new System.Drawing.Point(96, 110);
            this.ChildTextLabel.Name = "ChildTextLabel";
            this.ChildTextLabel.Size = new System.Drawing.Size(251, 20);
            this.ChildTextLabel.TabIndex = 3;
            // 
            // ParentHandleLabel
            // 
            this.ParentHandleLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ParentHandleLabel.Location = new System.Drawing.Point(96, 50);
            this.ParentHandleLabel.Name = "ParentHandleLabel";
            this.ParentHandleLabel.Size = new System.Drawing.Size(105, 20);
            this.ParentHandleLabel.TabIndex = 3;
            // 
            // ChildHandleLabel
            // 
            this.ChildHandleLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ChildHandleLabel.Location = new System.Drawing.Point(96, 22);
            this.ChildHandleLabel.Name = "ChildHandleLabel";
            this.ChildHandleLabel.Size = new System.Drawing.Size(105, 20);
            this.ChildHandleLabel.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Parent Handle:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(56, 81);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "PID:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Child Handle:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Parent Text:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Child Text:";
            // 
            // mouseCoordsLabel
            // 
            this.mouseCoordsLabel.AutoSize = true;
            this.mouseCoordsLabel.Location = new System.Drawing.Point(338, 9);
            this.mouseCoordsLabel.Name = "mouseCoordsLabel";
            this.mouseCoordsLabel.Size = new System.Drawing.Size(95, 13);
            this.mouseCoordsLabel.TabIndex = 2;
            this.mouseCoordsLabel.Text = "{X=0000, Y=0000}";
            // 
            // SendMsgButton
            // 
            this.SendMsgButton.Location = new System.Drawing.Point(201, 69);
            this.SendMsgButton.Name = "SendMsgButton";
            this.SendMsgButton.Size = new System.Drawing.Size(55, 23);
            this.SendMsgButton.TabIndex = 3;
            this.SendMsgButton.Text = "Send";
            this.SendMsgButton.UseVisualStyleBackColor = true;
            this.SendMsgButton.Click += new System.EventHandler(this.sendMsgButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(308, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Hold \'SHIFT\' and mouse over a windows form control to select it";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SpecificMessageRB);
            this.groupBox2.Controls.Add(this.CommonMessageRB);
            this.groupBox2.Controls.Add(this.SpecificTextBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.LParamTextBox);
            this.groupBox2.Controls.Add(this.WParamTextBox);
            this.groupBox2.Controls.Add(this.MessageComboBox);
            this.groupBox2.Controls.Add(this.SendMsgButton);
            this.groupBox2.Location = new System.Drawing.Point(15, 215);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(267, 100);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PostMessage";
            // 
            // SpecificMessageRB
            // 
            this.SpecificMessageRB.AutoSize = true;
            this.SpecificMessageRB.Location = new System.Drawing.Point(152, 19);
            this.SpecificMessageRB.Name = "SpecificMessageRB";
            this.SpecificMessageRB.Size = new System.Drawing.Size(109, 17);
            this.SpecificMessageRB.TabIndex = 6;
            this.SpecificMessageRB.Text = "Specific Message";
            this.SpecificMessageRB.UseVisualStyleBackColor = true;
            this.SpecificMessageRB.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // CommonMessageRB
            // 
            this.CommonMessageRB.AutoSize = true;
            this.CommonMessageRB.Checked = true;
            this.CommonMessageRB.Location = new System.Drawing.Point(9, 19);
            this.CommonMessageRB.Name = "CommonMessageRB";
            this.CommonMessageRB.Size = new System.Drawing.Size(111, 17);
            this.CommonMessageRB.TabIndex = 6;
            this.CommonMessageRB.TabStop = true;
            this.CommonMessageRB.Text = "Common message";
            this.CommonMessageRB.UseVisualStyleBackColor = true;
            this.CommonMessageRB.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // SpecificTextBox
            // 
            this.SpecificTextBox.Enabled = false;
            this.SpecificTextBox.Location = new System.Drawing.Point(190, 42);
            this.SpecificTextBox.Name = "SpecificTextBox";
            this.SpecificTextBox.Size = new System.Drawing.Size(66, 20);
            this.SpecificTextBox.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "w/lParam";
            // 
            // LParamTextBox
            // 
            this.LParamTextBox.Location = new System.Drawing.Point(127, 71);
            this.LParamTextBox.Name = "LParamTextBox";
            this.LParamTextBox.Size = new System.Drawing.Size(57, 20);
            this.LParamTextBox.TabIndex = 6;
            this.LParamTextBox.Text = "0";
            // 
            // WParamTextBox
            // 
            this.WParamTextBox.Location = new System.Drawing.Point(64, 71);
            this.WParamTextBox.Name = "WParamTextBox";
            this.WParamTextBox.Size = new System.Drawing.Size(57, 20);
            this.WParamTextBox.TabIndex = 6;
            this.WParamTextBox.Text = "0";
            // 
            // MessageComboBox
            // 
            this.MessageComboBox.FormattingEnabled = true;
            this.MessageComboBox.Location = new System.Drawing.Point(6, 42);
            this.MessageComboBox.Name = "MessageComboBox";
            this.MessageComboBox.Size = new System.Drawing.Size(178, 21);
            this.MessageComboBox.TabIndex = 4;
            // 
            // EnableButton
            // 
            this.EnableButton.Image = ((System.Drawing.Image)(resources.GetObject("EnableButton.Image")));
            this.EnableButton.Location = new System.Drawing.Point(6, 24);
            this.EnableButton.Name = "EnableButton";
            this.EnableButton.Size = new System.Drawing.Size(26, 26);
            this.EnableButton.TabIndex = 11;
            this.EnableButton.UseVisualStyleBackColor = true;
            this.EnableButton.Click += new System.EventHandler(this.enableButton_Click);
            // 
            // ShowButton
            // 
            this.ShowButton.Image = ((System.Drawing.Image)(resources.GetObject("ShowButton.Image")));
            this.ShowButton.Location = new System.Drawing.Point(70, 24);
            this.ShowButton.Name = "ShowButton";
            this.ShowButton.Size = new System.Drawing.Size(26, 26);
            this.ShowButton.TabIndex = 13;
            this.ShowButton.UseVisualStyleBackColor = true;
            this.ShowButton.Click += new System.EventHandler(this.showButton_Click);
            // 
            // ToFrontButton
            // 
            this.ToFrontButton.Image = ((System.Drawing.Image)(resources.GetObject("ToFrontButton.Image")));
            this.ToFrontButton.Location = new System.Drawing.Point(38, 24);
            this.ToFrontButton.Name = "ToFrontButton";
            this.ToFrontButton.Size = new System.Drawing.Size(26, 26);
            this.ToFrontButton.TabIndex = 12;
            this.ToFrontButton.UseVisualStyleBackColor = true;
            this.ToFrontButton.Click += new System.EventHandler(this.toFrontButton_Click);
            // 
            // MaximizeButton
            // 
            this.MaximizeButton.Image = ((System.Drawing.Image)(resources.GetObject("MaximizeButton.Image")));
            this.MaximizeButton.Location = new System.Drawing.Point(101, 24);
            this.MaximizeButton.Name = "MaximizeButton";
            this.MaximizeButton.Size = new System.Drawing.Size(26, 26);
            this.MaximizeButton.TabIndex = 14;
            this.MaximizeButton.UseVisualStyleBackColor = true;
            this.MaximizeButton.Click += new System.EventHandler(this.maximizeButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Image = ((System.Drawing.Image)(resources.GetObject("CloseButton.Image")));
            this.CloseButton.Location = new System.Drawing.Point(133, 24);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(26, 26);
            this.CloseButton.TabIndex = 15;
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // DisableButton
            // 
            this.DisableButton.Image = ((System.Drawing.Image)(resources.GetObject("DisableButton.Image")));
            this.DisableButton.Location = new System.Drawing.Point(6, 56);
            this.DisableButton.Name = "DisableButton";
            this.DisableButton.Size = new System.Drawing.Size(26, 26);
            this.DisableButton.TabIndex = 16;
            this.DisableButton.UseVisualStyleBackColor = true;
            this.DisableButton.Click += new System.EventHandler(this.disableButton_Click);
            // 
            // HideButton
            // 
            this.HideButton.Image = ((System.Drawing.Image)(resources.GetObject("HideButton.Image")));
            this.HideButton.Location = new System.Drawing.Point(70, 56);
            this.HideButton.Name = "HideButton";
            this.HideButton.Size = new System.Drawing.Size(26, 26);
            this.HideButton.TabIndex = 18;
            this.HideButton.UseVisualStyleBackColor = true;
            this.HideButton.Click += new System.EventHandler(this.hideButton_Click);
            // 
            // ToBackButton
            // 
            this.ToBackButton.Image = ((System.Drawing.Image)(resources.GetObject("ToBackButton.Image")));
            this.ToBackButton.Location = new System.Drawing.Point(37, 56);
            this.ToBackButton.Name = "ToBackButton";
            this.ToBackButton.Size = new System.Drawing.Size(26, 26);
            this.ToBackButton.TabIndex = 17;
            this.ToBackButton.UseVisualStyleBackColor = true;
            this.ToBackButton.Click += new System.EventHandler(this.toBackButton_Click);
            // 
            // MinimizeButton
            // 
            this.MinimizeButton.Image = ((System.Drawing.Image)(resources.GetObject("MinimizeButton.Image")));
            this.MinimizeButton.Location = new System.Drawing.Point(101, 56);
            this.MinimizeButton.Name = "MinimizeButton";
            this.MinimizeButton.Size = new System.Drawing.Size(26, 26);
            this.MinimizeButton.TabIndex = 19;
            this.MinimizeButton.UseVisualStyleBackColor = true;
            this.MinimizeButton.Click += new System.EventHandler(this.minimizeButton_Click);
            // 
            // RestoreButton
            // 
            this.RestoreButton.Image = ((System.Drawing.Image)(resources.GetObject("RestoreButton.Image")));
            this.RestoreButton.Location = new System.Drawing.Point(133, 56);
            this.RestoreButton.Name = "RestoreButton";
            this.RestoreButton.Size = new System.Drawing.Size(26, 26);
            this.RestoreButton.TabIndex = 20;
            this.RestoreButton.UseVisualStyleBackColor = true;
            this.RestoreButton.Click += new System.EventHandler(this.restoreButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.RestoreButton);
            this.groupBox3.Controls.Add(this.EnableButton);
            this.groupBox3.Controls.Add(this.DisableButton);
            this.groupBox3.Controls.Add(this.ShowButton);
            this.groupBox3.Controls.Add(this.HideButton);
            this.groupBox3.Controls.Add(this.CloseButton);
            this.groupBox3.Controls.Add(this.MinimizeButton);
            this.groupBox3.Controls.Add(this.ToBackButton);
            this.groupBox3.Controls.Add(this.MaximizeButton);
            this.groupBox3.Controls.Add(this.ToFrontButton);
            this.groupBox3.Location = new System.Drawing.Point(290, 215);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(168, 100);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Set";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.PanRightButton);
            this.groupBox4.Controls.Add(this.PanDownButton);
            this.groupBox4.Controls.Add(this.PanLeftButton);
            this.groupBox4.Controls.Add(this.PanUpButton);
            this.groupBox4.Controls.Add(this.HTextBox);
            this.groupBox4.Controls.Add(this.WTextBox);
            this.groupBox4.Controls.Add(this.YTextBox);
            this.groupBox4.Controls.Add(this.XTextBox);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Location = new System.Drawing.Point(371, 34);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(87, 175);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Location";
            // 
            // PanRightButton
            // 
            this.PanRightButton.Location = new System.Drawing.Point(55, 146);
            this.PanRightButton.Name = "PanRightButton";
            this.PanRightButton.Size = new System.Drawing.Size(24, 23);
            this.PanRightButton.TabIndex = 10;
            this.PanRightButton.Text = ">";
            this.PanRightButton.UseVisualStyleBackColor = true;
            this.PanRightButton.Click += new System.EventHandler(this.panRightButton_Click);
            this.PanRightButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanRightButton_MouseDown);
            this.PanRightButton.MouseLeave += new System.EventHandler(this.PanRightButton_MouseLeave);
            this.PanRightButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanRightButton_MouseUp);
            // 
            // PanDownButton
            // 
            this.PanDownButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.PanDownButton.Location = new System.Drawing.Point(32, 146);
            this.PanDownButton.Name = "PanDownButton";
            this.PanDownButton.Size = new System.Drawing.Size(24, 23);
            this.PanDownButton.TabIndex = 9;
            this.PanDownButton.Text = " v";
            this.PanDownButton.UseVisualStyleBackColor = true;
            this.PanDownButton.Click += new System.EventHandler(this.panDownButton_Click);
            this.PanDownButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanDownButton_MouseDown);
            this.PanDownButton.MouseLeave += new System.EventHandler(this.PanDownButton_MouseLeave);
            this.PanDownButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanDownButton_MouseUp);
            // 
            // PanLeftButton
            // 
            this.PanLeftButton.Location = new System.Drawing.Point(9, 146);
            this.PanLeftButton.Name = "PanLeftButton";
            this.PanLeftButton.Size = new System.Drawing.Size(24, 23);
            this.PanLeftButton.TabIndex = 8;
            this.PanLeftButton.Text = "<";
            this.PanLeftButton.UseVisualStyleBackColor = true;
            this.PanLeftButton.Click += new System.EventHandler(this.panLeftButton_Click);
            this.PanLeftButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanLeftButton_MouseDown);
            this.PanLeftButton.MouseLeave += new System.EventHandler(this.PanLeftButton_MouseLeave);
            this.PanLeftButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanLeftButton_MouseUp);
            // 
            // PanUpButton
            // 
            this.PanUpButton.Location = new System.Drawing.Point(32, 124);
            this.PanUpButton.Name = "PanUpButton";
            this.PanUpButton.Size = new System.Drawing.Size(24, 23);
            this.PanUpButton.TabIndex = 7;
            this.PanUpButton.Text = "^";
            this.PanUpButton.UseVisualStyleBackColor = true;
            this.PanUpButton.Click += new System.EventHandler(this.panUpButton_Click);
            this.PanUpButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanUpButton_MouseDown);
            this.PanUpButton.MouseLeave += new System.EventHandler(this.PanUpButton_MouseLeave);
            this.PanUpButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanUpButton_MouseUp);
            // 
            // HTextBox
            // 
            this.HTextBox.Location = new System.Drawing.Point(26, 90);
            this.HTextBox.Name = "HTextBox";
            this.HTextBox.Size = new System.Drawing.Size(49, 20);
            this.HTextBox.TabIndex = 6;
            this.HTextBox.TextChanged += new System.EventHandler(this.hTextBox_TextChanged);
            // 
            // WTextBox
            // 
            this.WTextBox.Location = new System.Drawing.Point(26, 66);
            this.WTextBox.Name = "WTextBox";
            this.WTextBox.Size = new System.Drawing.Size(49, 20);
            this.WTextBox.TabIndex = 5;
            this.WTextBox.TextChanged += new System.EventHandler(this.wTextBox_TextChanged);
            // 
            // YTextBox
            // 
            this.YTextBox.Location = new System.Drawing.Point(26, 42);
            this.YTextBox.Name = "YTextBox";
            this.YTextBox.Size = new System.Drawing.Size(49, 20);
            this.YTextBox.TabIndex = 4;
            this.YTextBox.TextChanged += new System.EventHandler(this.yTextBox_TextChanged);
            // 
            // XTextBox
            // 
            this.XTextBox.Location = new System.Drawing.Point(26, 18);
            this.XTextBox.Name = "XTextBox";
            this.XTextBox.Size = new System.Drawing.Size(49, 20);
            this.XTextBox.TabIndex = 3;
            this.XTextBox.TextChanged += new System.EventHandler(this.xTextBox_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 92);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "H";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(18, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "W";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Y";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "X";
            // 
            // PanHoldTimer
            // 
            this.PanHoldTimer.Tick += new System.EventHandler(this.PanHoldTimer_Tick);
            // 
            // GUIEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 328);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mouseCoordsLabel);
            this.Name = "GUIEditor";
            this.Text = "GUIEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GUIEditor_FormClosing);
            this.Load += new System.EventHandler(this.GUIEditor_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label ParentTextLabel;
        private System.Windows.Forms.Label ChildTextLabel;
        private System.Windows.Forms.Label ParentHandleLabel;
        private System.Windows.Forms.Label ChildHandleLabel;
        private System.Windows.Forms.Label mouseCoordsLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ProcessIDLabel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button SendMsgButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox MessageComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox LParamTextBox;
        private System.Windows.Forms.TextBox WParamTextBox;
        private System.Windows.Forms.TextBox SpecificTextBox;
        private System.Windows.Forms.RadioButton SpecificMessageRB;
        private System.Windows.Forms.RadioButton CommonMessageRB;
        private System.Windows.Forms.Button EnableButton;
        private System.Windows.Forms.Button ShowButton;
        private System.Windows.Forms.Button ToFrontButton;
        private System.Windows.Forms.Button MaximizeButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button DisableButton;
        private System.Windows.Forms.Button HideButton;
        private System.Windows.Forms.Button ToBackButton;
        private System.Windows.Forms.Button MinimizeButton;
        private System.Windows.Forms.Button RestoreButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton ChildWFPRB;
        private System.Windows.Forms.ComboBox GrabTypeComboBox;
        private System.Windows.Forms.RadioButton ChildWFPExRB;
        private System.Windows.Forms.RadioButton RealChildWFPRB;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox HTextBox;
        private System.Windows.Forms.TextBox WTextBox;
        private System.Windows.Forms.TextBox YTextBox;
        private System.Windows.Forms.TextBox XTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button PanRightButton;
        private System.Windows.Forms.Button PanDownButton;
        private System.Windows.Forms.Button PanLeftButton;
        private System.Windows.Forms.Button PanUpButton;
        private System.Windows.Forms.Button IgnoreParentButton;
        private System.Windows.Forms.Button IgnoreChildButton;
        private System.Windows.Forms.ToolTip GUIToolTip;
        private System.Windows.Forms.Timer PanHoldTimer;
    }
}