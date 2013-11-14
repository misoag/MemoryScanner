namespace AecialEngine
{
    partial class ValueWindow
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
            this.CancelValueButton = new System.Windows.Forms.Button();
            this.AcceptValueButton = new System.Windows.Forms.Button();
            this.ValueTextBox = new System.Windows.Forms.TextBox();
            this.IsHexCB = new System.Windows.Forms.CheckBox();
            this.ValueRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.convertToHexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToDecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ValueRightClickMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Change Value:";
            // 
            // CancelValueButton
            // 
            this.CancelValueButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelValueButton.Location = new System.Drawing.Point(150, 83);
            this.CancelValueButton.Name = "CancelValueButton";
            this.CancelValueButton.Size = new System.Drawing.Size(75, 23);
            this.CancelValueButton.TabIndex = 3;
            this.CancelValueButton.Text = "Cancel";
            this.CancelValueButton.UseVisualStyleBackColor = true;
            // 
            // AcceptValueButton
            // 
            this.AcceptValueButton.Location = new System.Drawing.Point(12, 83);
            this.AcceptValueButton.Name = "AcceptValueButton";
            this.AcceptValueButton.Size = new System.Drawing.Size(75, 23);
            this.AcceptValueButton.TabIndex = 2;
            this.AcceptValueButton.Text = "Accept";
            this.AcceptValueButton.UseVisualStyleBackColor = true;
            this.AcceptValueButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // ValueTextBox
            // 
            this.ValueTextBox.ContextMenuStrip = this.ValueRightClickMenu;
            this.ValueTextBox.Location = new System.Drawing.Point(12, 34);
            this.ValueTextBox.Name = "ValueTextBox";
            this.ValueTextBox.Size = new System.Drawing.Size(213, 20);
            this.ValueTextBox.TabIndex = 0;
            this.ValueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // IsHexCB
            // 
            this.IsHexCB.AutoSize = true;
            this.IsHexCB.Location = new System.Drawing.Point(231, 36);
            this.IsHexCB.Name = "IsHexCB";
            this.IsHexCB.Size = new System.Drawing.Size(45, 17);
            this.IsHexCB.TabIndex = 1;
            this.IsHexCB.Text = "Hex";
            this.IsHexCB.UseVisualStyleBackColor = true;
            // 
            // ValueRightClickMenu
            // 
            this.ValueRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertToHexToolStripMenuItem,
            this.convertToDecToolStripMenuItem});
            this.ValueRightClickMenu.Name = "ScanValueRightClickMenu";
            this.ValueRightClickMenu.Size = new System.Drawing.Size(154, 48);
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
            // ValueWindow
            // 
            this.AcceptButton = this.AcceptValueButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelValueButton;
            this.ClientSize = new System.Drawing.Size(320, 116);
            this.Controls.Add(this.IsHexCB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CancelValueButton);
            this.Controls.Add(this.AcceptValueButton);
            this.Controls.Add(this.ValueTextBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ValueWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Value";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ValueWindow_FormClosing);
            this.ValueRightClickMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CancelValueButton;
        private System.Windows.Forms.Button AcceptValueButton;
        private System.Windows.Forms.TextBox ValueTextBox;
        private System.Windows.Forms.CheckBox IsHexCB;
        private System.Windows.Forms.ContextMenuStrip ValueRightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem convertToHexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToDecToolStripMenuItem;
    }
}