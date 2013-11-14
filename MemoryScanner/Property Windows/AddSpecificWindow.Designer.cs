namespace AecialEngine
{
    partial class AddSpecificWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddSpecificWindow));
            this.AcceptSpecificButton = new System.Windows.Forms.Button();
            this.CancelSpecificButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DescriptionTextBox = new System.Windows.Forms.TextBox();
            this.AddressTextBox = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.ValueTypeComboBox = new System.Windows.Forms.ComboBox();
            this.IsHexCB = new System.Windows.Forms.CheckBox();
            this.GUIToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.AddressRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.convertToHexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToDecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddressRightClickMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // AcceptSpecificButton
            // 
            this.AcceptSpecificButton.Location = new System.Drawing.Point(12, 129);
            this.AcceptSpecificButton.Name = "AcceptSpecificButton";
            this.AcceptSpecificButton.Size = new System.Drawing.Size(75, 23);
            this.AcceptSpecificButton.TabIndex = 4;
            this.AcceptSpecificButton.Text = "Accept";
            this.AcceptSpecificButton.UseVisualStyleBackColor = true;
            this.AcceptSpecificButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // CancelSpecificButton
            // 
            this.CancelSpecificButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelSpecificButton.Location = new System.Drawing.Point(93, 129);
            this.CancelSpecificButton.Name = "CancelSpecificButton";
            this.CancelSpecificButton.Size = new System.Drawing.Size(75, 23);
            this.CancelSpecificButton.TabIndex = 5;
            this.CancelSpecificButton.Text = "Cancel";
            this.CancelSpecificButton.UseVisualStyleBackColor = true;
            this.CancelSpecificButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Address:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Description:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Type:";
            // 
            // DescriptionTextBox
            // 
            this.DescriptionTextBox.Location = new System.Drawing.Point(82, 32);
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.Size = new System.Drawing.Size(285, 20);
            this.DescriptionTextBox.TabIndex = 1;
            this.DescriptionTextBox.Text = "No Description";
            // 
            // AddressTextBox
            // 
            this.AddressTextBox.ContextMenuStrip = this.AddressRightClickMenu;
            this.AddressTextBox.Location = new System.Drawing.Point(82, 6);
            this.AddressTextBox.MaxLength = 8;
            this.AddressTextBox.Name = "AddressTextBox";
            this.AddressTextBox.Size = new System.Drawing.Size(159, 20);
            this.AddressTextBox.TabIndex = 0;
            this.AddressTextBox.Text = "0100579C";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(82, 94);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(145, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Pointer (not available yet)";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // ValueTypeComboBox
            // 
            this.ValueTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ValueTypeComboBox.FormattingEnabled = true;
            this.ValueTypeComboBox.Items.AddRange(new object[] {
            "Binary",
            "Byte",
            "Int 16",
            "Int 32",
            "Int 64",
            "Single",
            "Double",
            "Text",
            "Array of bytes"});
            this.ValueTypeComboBox.Location = new System.Drawing.Point(82, 58);
            this.ValueTypeComboBox.Name = "ValueTypeComboBox";
            this.ValueTypeComboBox.Size = new System.Drawing.Size(159, 21);
            this.ValueTypeComboBox.TabIndex = 2;
            // 
            // IsHexCB
            // 
            this.IsHexCB.AutoSize = true;
            this.IsHexCB.Checked = true;
            this.IsHexCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IsHexCB.Location = new System.Drawing.Point(247, 8);
            this.IsHexCB.Name = "IsHexCB";
            this.IsHexCB.Size = new System.Drawing.Size(45, 17);
            this.IsHexCB.TabIndex = 55;
            this.IsHexCB.Text = "Hex";
            this.IsHexCB.UseVisualStyleBackColor = true;
            // 
            // AddressRightClickMenu
            // 
            this.AddressRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertToHexToolStripMenuItem,
            this.convertToDecToolStripMenuItem});
            this.AddressRightClickMenu.Name = "ScanValueRightClickMenu";
            this.AddressRightClickMenu.Size = new System.Drawing.Size(154, 70);
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
            // AddSpecificWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 164);
            this.Controls.Add(this.IsHexCB);
            this.Controls.Add(this.ValueTypeComboBox);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.AddressTextBox);
            this.Controls.Add(this.DescriptionTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CancelSpecificButton);
            this.Controls.Add(this.AcceptSpecificButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddSpecificWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.AddSpecificWindow_Load);
            this.AddressRightClickMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AcceptSpecificButton;
        private System.Windows.Forms.Button CancelSpecificButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox DescriptionTextBox;
        private System.Windows.Forms.TextBox AddressTextBox;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox ValueTypeComboBox;
        private System.Windows.Forms.CheckBox IsHexCB;
        private System.Windows.Forms.ToolTip GUIToolTip;
        private System.Windows.Forms.ContextMenuStrip AddressRightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem convertToHexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToDecToolStripMenuItem;
    }
}