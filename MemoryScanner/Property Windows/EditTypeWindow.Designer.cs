namespace AecialEngine
{
    partial class TypeWindow
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
            this.CancelTypeButton = new System.Windows.Forms.Button();
            this.AcceptTypeButton = new System.Windows.Forms.Button();
            this.ValueTypeComboBox = new System.Windows.Forms.ComboBox();
            this.GUIToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Change Type:";
            // 
            // CancelTypeButton
            // 
            this.CancelTypeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelTypeButton.Location = new System.Drawing.Point(169, 88);
            this.CancelTypeButton.Name = "CancelTypeButton";
            this.CancelTypeButton.Size = new System.Drawing.Size(75, 23);
            this.CancelTypeButton.TabIndex = 2;
            this.CancelTypeButton.Text = "Cancel";
            this.CancelTypeButton.UseVisualStyleBackColor = true;
            this.CancelTypeButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // AcceptTypeButton
            // 
            this.AcceptTypeButton.Location = new System.Drawing.Point(48, 88);
            this.AcceptTypeButton.Name = "AcceptTypeButton";
            this.AcceptTypeButton.Size = new System.Drawing.Size(75, 23);
            this.AcceptTypeButton.TabIndex = 1;
            this.AcceptTypeButton.Text = "Accept";
            this.AcceptTypeButton.UseVisualStyleBackColor = true;
            this.AcceptTypeButton.Click += new System.EventHandler(this.acceptButton_Click);
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
            "Array of Bytes",
            "All (Byte to Double)"});
            this.ValueTypeComboBox.Location = new System.Drawing.Point(66, 43);
            this.ValueTypeComboBox.Name = "ValueTypeComboBox";
            this.ValueTypeComboBox.Size = new System.Drawing.Size(159, 21);
            this.ValueTypeComboBox.TabIndex = 0;
            // 
            // TypeWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 126);
            this.Controls.Add(this.ValueTypeComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CancelTypeButton);
            this.Controls.Add(this.AcceptTypeButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TypeWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Type";
            this.Load += new System.EventHandler(this.TypeWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CancelTypeButton;
        private System.Windows.Forms.Button AcceptTypeButton;
        private System.Windows.Forms.ComboBox ValueTypeComboBox;
        private System.Windows.Forms.ToolTip GUIToolTip;
    }
}