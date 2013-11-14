namespace AecialEngine
{
    partial class ScanHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanHistory));
            this.ScanHistoryListView = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ScanSessionComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.GUIToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // ScanHistoryListView
            // 
            this.ScanHistoryListView.BackColor = System.Drawing.SystemColors.Control;
            this.ScanHistoryListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7});
            this.ScanHistoryListView.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScanHistoryListView.FullRowSelect = true;
            this.ScanHistoryListView.Location = new System.Drawing.Point(0, 40);
            this.ScanHistoryListView.Name = "ScanHistoryListView";
            this.ScanHistoryListView.Size = new System.Drawing.Size(256, 168);
            this.ScanHistoryListView.TabIndex = 49;
            this.ScanHistoryListView.UseCompatibleStateImageBehavior = false;
            this.ScanHistoryListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Type";
            this.columnHeader6.Width = 44;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Value";
            this.columnHeader7.Width = 175;
            // 
            // ScanSessionComboBox
            // 
            this.ScanSessionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ScanSessionComboBox.FormattingEnabled = true;
            this.ScanSessionComboBox.Location = new System.Drawing.Point(53, 12);
            this.ScanSessionComboBox.Name = "ScanSessionComboBox";
            this.ScanSessionComboBox.Size = new System.Drawing.Size(135, 21);
            this.ScanSessionComboBox.TabIndex = 50;
            this.ScanSessionComboBox.SelectedIndexChanged += new System.EventHandler(this.ScanSessionComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "Session:";
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(194, 10);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(54, 23);
            this.RefreshButton.TabIndex = 52;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // ScanHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 208);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ScanSessionComboBox);
            this.Controls.Add(this.ScanHistoryListView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ScanHistory";
            this.Text = "Scan History";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScanHistory_FormClosing);
            this.Load += new System.EventHandler(this.ScanHistory_Load);
            this.VisibleChanged += new System.EventHandler(this.ScanHistory_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView ScanHistoryListView;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ComboBox ScanSessionComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.ToolTip GUIToolTip;
    }
}