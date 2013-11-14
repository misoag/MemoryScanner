namespace AecialEngine
{
    partial class DescriptionWindow
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
            this.DescriptionTextBox = new System.Windows.Forms.TextBox();
            this.AcceptDescriptionButton = new System.Windows.Forms.Button();
            this.CancelDescriptionButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.GUIToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // DescriptionTextBox
            // 
            this.DescriptionTextBox.Location = new System.Drawing.Point(49, 43);
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.Size = new System.Drawing.Size(196, 20);
            this.DescriptionTextBox.TabIndex = 0;
            this.DescriptionTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AcceptDescriptionButton
            // 
            this.AcceptDescriptionButton.Location = new System.Drawing.Point(49, 91);
            this.AcceptDescriptionButton.Name = "AcceptDescriptionButton";
            this.AcceptDescriptionButton.Size = new System.Drawing.Size(75, 23);
            this.AcceptDescriptionButton.TabIndex = 1;
            this.AcceptDescriptionButton.Text = "Accept";
            this.AcceptDescriptionButton.UseVisualStyleBackColor = true;
            this.AcceptDescriptionButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // CancelDescriptionButton
            // 
            this.CancelDescriptionButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelDescriptionButton.Location = new System.Drawing.Point(170, 91);
            this.CancelDescriptionButton.Name = "CancelDescriptionButton";
            this.CancelDescriptionButton.Size = new System.Drawing.Size(75, 23);
            this.CancelDescriptionButton.TabIndex = 2;
            this.CancelDescriptionButton.Text = "Cancel";
            this.CancelDescriptionButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(96, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Change Description:";
            // 
            // DescriptionWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 126);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CancelDescriptionButton);
            this.Controls.Add(this.AcceptDescriptionButton);
            this.Controls.Add(this.DescriptionTextBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DescriptionWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DescriptionWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox DescriptionTextBox;
        private System.Windows.Forms.Button AcceptDescriptionButton;
        private System.Windows.Forms.Button CancelDescriptionButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip GUIToolTip;
    }
}