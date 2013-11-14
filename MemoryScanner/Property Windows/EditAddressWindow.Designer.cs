namespace AecialEngine
{
    partial class AddressWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.CancelAddressButton = new System.Windows.Forms.Button();
            this.AcceptAddressButton = new System.Windows.Forms.Button();
            this.AddressTextBox = new System.Windows.Forms.TextBox();
            this.IsHexCB = new System.Windows.Forms.CheckBox();
            this.GUIToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.AddressRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.convertToHexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToDecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddressRightClickMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(102, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Change Address:";
            // 
            // CancelAddressButton
            // 
            this.CancelAddressButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelAddressButton.Location = new System.Drawing.Point(156, 91);
            this.CancelAddressButton.Name = "CancelAddressButton";
            this.CancelAddressButton.Size = new System.Drawing.Size(75, 23);
            this.CancelAddressButton.TabIndex = 3;
            this.CancelAddressButton.Text = "Cancel";
            this.CancelAddressButton.UseVisualStyleBackColor = true;
            // 
            // AcceptAddressButton
            // 
            this.AcceptAddressButton.Location = new System.Drawing.Point(35, 91);
            this.AcceptAddressButton.Name = "AcceptAddressButton";
            this.AcceptAddressButton.Size = new System.Drawing.Size(75, 23);
            this.AcceptAddressButton.TabIndex = 2;
            this.AcceptAddressButton.Text = "Accept";
            this.AcceptAddressButton.UseVisualStyleBackColor = true;
            this.AcceptAddressButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // AddressTextBox
            // 
            this.AddressTextBox.ContextMenuStrip = this.AddressRightClickMenu;
            this.AddressTextBox.Location = new System.Drawing.Point(12, 40);
            this.AddressTextBox.Name = "AddressTextBox";
            this.AddressTextBox.Size = new System.Drawing.Size(217, 20);
            this.AddressTextBox.TabIndex = 0;
            this.AddressTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // IsHexCB
            // 
            this.IsHexCB.AutoSize = true;
            this.IsHexCB.Checked = true;
            this.IsHexCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IsHexCB.Location = new System.Drawing.Point(235, 42);
            this.IsHexCB.Name = "IsHexCB";
            this.IsHexCB.Size = new System.Drawing.Size(45, 17);
            this.IsHexCB.TabIndex = 1;
            this.IsHexCB.Text = "Hex";
            this.IsHexCB.UseVisualStyleBackColor = true;
            // 
            // AddressRightClickMenu
            // 
            this.AddressRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertToHexToolStripMenuItem,
            this.convertToDecToolStripMenuItem});
            this.AddressRightClickMenu.Name = "ScanValueRightClickMenu";
            this.AddressRightClickMenu.Size = new System.Drawing.Size(154, 48);
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
            // AddressWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 126);
            this.Controls.Add(this.IsHexCB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CancelAddressButton);
            this.Controls.Add(this.AcceptAddressButton);
            this.Controls.Add(this.AddressTextBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddressWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Address";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddressWindow_FormClosing);
            this.AddressRightClickMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CancelAddressButton;
        private System.Windows.Forms.Button AcceptAddressButton;
        private System.Windows.Forms.TextBox AddressTextBox;
        private System.Windows.Forms.CheckBox IsHexCB;
        private System.Windows.Forms.ToolTip GUIToolTip;
        private System.Windows.Forms.ContextMenuStrip AddressRightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem convertToHexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToDecToolStripMenuItem;
    }
}