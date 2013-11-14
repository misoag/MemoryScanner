namespace AecialEngine
{
    partial class MemoryViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemoryViewer));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kernelToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateFoundTimer = new System.Windows.Forms.Timer(this.components);
            this.DataListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AssemblyListView = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.DataScrollBar = new System.Windows.Forms.VScrollBar();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.debugToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.kernelToolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(1153, 24);
            this.menuStrip1.TabIndex = 47;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem,
            this.dieToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.testToolStripMenuItem.Text = "Test";
            // 
            // dieToolStripMenuItem
            // 
            this.dieToolStripMenuItem.Name = "dieToolStripMenuItem";
            this.dieToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.dieToolStripMenuItem.Text = "Die";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.searchToolStripMenuItem.Text = "Search";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.debugToolStripMenuItem.Text = "Debug";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // kernelToolsToolStripMenuItem
            // 
            this.kernelToolsToolStripMenuItem.Name = "kernelToolsToolStripMenuItem";
            this.kernelToolsToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.kernelToolsToolStripMenuItem.Text = "Kernel Tools";
            // 
            // updateFoundTimer
            // 
            this.updateFoundTimer.Interval = 500;
            this.updateFoundTimer.Tick += new System.EventHandler(this.updateFoundTimer_Tick);
            // 
            // DataListView
            // 
            this.DataListView.BackColor = System.Drawing.SystemColors.Control;
            this.DataListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.DataListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataListView.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataListView.ForeColor = System.Drawing.SystemColors.WindowText;
            this.DataListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.DataListView.Location = new System.Drawing.Point(0, 368);
            this.DataListView.Name = "DataListView";
            this.DataListView.OwnerDraw = true;
            this.DataListView.Scrollable = false;
            this.DataListView.ShowGroups = false;
            this.DataListView.Size = new System.Drawing.Size(1153, 308);
            this.DataListView.TabIndex = 48;
            this.DataListView.UseCompatibleStateImageBehavior = false;
            this.DataListView.View = System.Windows.Forms.View.Details;
            this.DataListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.DataListView_DrawColumnHeader);
            this.DataListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.DataListView_DrawItem);
            this.DataListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.DataListView_DrawSubItem);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Address";
            this.columnHeader1.Width = 95;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Hex Dump";
            this.columnHeader2.Width = 382;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Ascii";
            this.columnHeader3.Width = 335;
            // 
            // AssemblyListView
            // 
            this.AssemblyListView.BackColor = System.Drawing.SystemColors.Control;
            this.AssemblyListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.AssemblyListView.Dock = System.Windows.Forms.DockStyle.Top;
            this.AssemblyListView.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AssemblyListView.FullRowSelect = true;
            this.AssemblyListView.Location = new System.Drawing.Point(0, 24);
            this.AssemblyListView.Name = "AssemblyListView";
            this.AssemblyListView.ShowGroups = false;
            this.AssemblyListView.Size = new System.Drawing.Size(1153, 334);
            this.AssemblyListView.TabIndex = 48;
            this.AssemblyListView.UseCompatibleStateImageBehavior = false;
            this.AssemblyListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Address";
            this.columnHeader4.Width = 95;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Bytes";
            this.columnHeader5.Width = 89;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Assembly Interpretation";
            this.columnHeader6.Width = 201;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Comment";
            this.columnHeader7.Width = 146;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 358);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1153, 10);
            this.splitter1.TabIndex = 49;
            this.splitter1.TabStop = false;
            // 
            // DataScrollBar
            // 
            this.DataScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.DataScrollBar.LargeChange = 8;
            this.DataScrollBar.Location = new System.Drawing.Point(1136, 368);
            this.DataScrollBar.Maximum = 10000;
            this.DataScrollBar.Name = "DataScrollBar";
            this.DataScrollBar.Size = new System.Drawing.Size(17, 308);
            this.DataScrollBar.TabIndex = 50;
            this.DataScrollBar.Value = 5000;
            this.DataScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataScrollBar_Scroll);
            // 
            // MemoryViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 676);
            this.Controls.Add(this.DataScrollBar);
            this.Controls.Add(this.DataListView);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.AssemblyListView);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MemoryViewer";
            this.Text = "Memory Viewer";
            this.Load += new System.EventHandler(this.Memory_Viewer_Load);
            this.ResizeEnd += new System.EventHandler(this.MemoryViewer_ResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MemoryViewer_KeyDown);
            this.Resize += new System.EventHandler(this.MemoryViewer_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kernelToolsToolStripMenuItem;
        private System.Windows.Forms.Timer updateFoundTimer;
        private System.Windows.Forms.ListView DataListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ListView AssemblyListView;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.VScrollBar DataScrollBar;
    }
}