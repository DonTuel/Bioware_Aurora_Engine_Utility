
namespace Bioware_Aurora_Engine_Utility
{
    partial class AuroraEngine_Utility
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.layoutERFTab = new System.Windows.Forms.TableLayoutPanel();
            this.lboxERF = new System.Windows.Forms.ListBox();
            this.ERFTab = new System.Windows.Forms.TabControl();
            this.tpERF1 = new System.Windows.Forms.TabPage();
            this.txtERF1 = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnuColumns = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnu4Columns = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnu8Columns = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnu16Columns = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnu32Columns = new System.Windows.Forms.ToolStripMenuItem();
            this.tpERF2 = new System.Windows.Forms.TabPage();
            this.txtERF2 = new System.Windows.Forms.RichTextBox();
            this.tpERF3 = new System.Windows.Forms.TabPage();
            this.txtERF3 = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblERFStructure = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.layoutERFTab.SuspendLayout();
            this.ERFTab.SuspendLayout();
            this.tpERF1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tpERF2.SuspendLayout();
            this.tpERF3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1054, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileOpen,
            this.toolStripMenuItem1,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(103, 22);
            this.mnuFileOpen.Text = "Open";
            this.mnuFileOpen.Click += new System.EventHandler(this.mnuFileOpen_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(100, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(103, 22);
            this.mnuFileExit.Text = "Exit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 461);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1054, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // layoutERFTab
            // 
            this.layoutERFTab.ColumnCount = 2;
            this.layoutERFTab.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.layoutERFTab.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.67F));
            this.layoutERFTab.Controls.Add(this.ERFTab, 1, 0);
            this.layoutERFTab.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.layoutERFTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutERFTab.Location = new System.Drawing.Point(0, 24);
            this.layoutERFTab.Name = "layoutERFTab";
            this.layoutERFTab.RowCount = 2;
            this.layoutERFTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutERFTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutERFTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutERFTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutERFTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutERFTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutERFTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutERFTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutERFTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutERFTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutERFTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutERFTab.Size = new System.Drawing.Size(1054, 437);
            this.layoutERFTab.TabIndex = 0;
            // 
            // lboxERF
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.lboxERF, 2);
            this.lboxERF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lboxERF.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lboxERF.FormattingEnabled = true;
            this.lboxERF.ItemHeight = 18;
            this.lboxERF.Location = new System.Drawing.Point(3, 67);
            this.lboxERF.Name = "lboxERF";
            this.lboxERF.Size = new System.Drawing.Size(339, 361);
            this.lboxERF.TabIndex = 0;
            this.lboxERF.SelectedIndexChanged += new System.EventHandler(this.lboxERF_SelectedIndexChanged);
            // 
            // ERFTab
            // 
            this.ERFTab.Controls.Add(this.tpERF1);
            this.ERFTab.Controls.Add(this.tpERF2);
            this.ERFTab.Controls.Add(this.tpERF3);
            this.ERFTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ERFTab.Location = new System.Drawing.Point(354, 3);
            this.ERFTab.Name = "ERFTab";
            this.layoutERFTab.SetRowSpan(this.ERFTab, 2);
            this.ERFTab.SelectedIndex = 0;
            this.ERFTab.Size = new System.Drawing.Size(697, 431);
            this.ERFTab.TabIndex = 2;
            // 
            // tpERF1
            // 
            this.tpERF1.Controls.Add(this.txtERF1);
            this.tpERF1.Location = new System.Drawing.Point(4, 24);
            this.tpERF1.Name = "tpERF1";
            this.tpERF1.Padding = new System.Windows.Forms.Padding(3);
            this.tpERF1.Size = new System.Drawing.Size(689, 403);
            this.tpERF1.TabIndex = 0;
            this.tpERF1.Text = "...";
            this.tpERF1.UseVisualStyleBackColor = true;
            // 
            // txtERF1
            // 
            this.txtERF1.ContextMenuStrip = this.contextMenuStrip1;
            this.txtERF1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtERF1.Location = new System.Drawing.Point(3, 3);
            this.txtERF1.Name = "txtERF1";
            this.txtERF1.ReadOnly = true;
            this.txtERF1.Size = new System.Drawing.Size(683, 397);
            this.txtERF1.TabIndex = 1;
            this.txtERF1.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuColumns});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(123, 26);
            // 
            // cmnuColumns
            // 
            this.cmnuColumns.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnu4Columns,
            this.cmnu8Columns,
            this.cmnu16Columns,
            this.cmnu32Columns});
            this.cmnuColumns.Name = "cmnuColumns";
            this.cmnuColumns.Size = new System.Drawing.Size(122, 22);
            this.cmnuColumns.Text = "Columns";
            // 
            // cmnu4Columns
            // 
            this.cmnu4Columns.Name = "cmnu4Columns";
            this.cmnu4Columns.Size = new System.Drawing.Size(137, 22);
            this.cmnu4Columns.Tag = "4";
            this.cmnu4Columns.Text = "4 Columns";
            this.cmnu4Columns.Click += new System.EventHandler(this.cmnuColumns_Click);
            // 
            // cmnu8Columns
            // 
            this.cmnu8Columns.Name = "cmnu8Columns";
            this.cmnu8Columns.Size = new System.Drawing.Size(137, 22);
            this.cmnu8Columns.Tag = "8";
            this.cmnu8Columns.Text = "8 Columns";
            this.cmnu8Columns.Click += new System.EventHandler(this.cmnuColumns_Click);
            // 
            // cmnu16Columns
            // 
            this.cmnu16Columns.Name = "cmnu16Columns";
            this.cmnu16Columns.Size = new System.Drawing.Size(137, 22);
            this.cmnu16Columns.Tag = "16";
            this.cmnu16Columns.Text = "16 Columns";
            this.cmnu16Columns.Click += new System.EventHandler(this.cmnuColumns_Click);
            // 
            // cmnu32Columns
            // 
            this.cmnu32Columns.Name = "cmnu32Columns";
            this.cmnu32Columns.Size = new System.Drawing.Size(137, 22);
            this.cmnu32Columns.Tag = "32";
            this.cmnu32Columns.Text = "32 Columns";
            this.cmnu32Columns.Click += new System.EventHandler(this.cmnuColumns_Click);
            // 
            // tpERF2
            // 
            this.tpERF2.Controls.Add(this.txtERF2);
            this.tpERF2.Location = new System.Drawing.Point(4, 24);
            this.tpERF2.Name = "tpERF2";
            this.tpERF2.Padding = new System.Windows.Forms.Padding(3);
            this.tpERF2.Size = new System.Drawing.Size(689, 403);
            this.tpERF2.TabIndex = 1;
            this.tpERF2.Text = "tabPage2";
            this.tpERF2.UseVisualStyleBackColor = true;
            // 
            // txtERF2
            // 
            this.txtERF2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtERF2.Location = new System.Drawing.Point(3, 3);
            this.txtERF2.Name = "txtERF2";
            this.txtERF2.Size = new System.Drawing.Size(683, 397);
            this.txtERF2.TabIndex = 0;
            this.txtERF2.Text = "";
            // 
            // tpERF3
            // 
            this.tpERF3.Controls.Add(this.txtERF3);
            this.tpERF3.Location = new System.Drawing.Point(4, 24);
            this.tpERF3.Name = "tpERF3";
            this.tpERF3.Size = new System.Drawing.Size(689, 403);
            this.tpERF3.TabIndex = 2;
            this.tpERF3.Text = "tabPage3";
            this.tpERF3.UseVisualStyleBackColor = true;
            // 
            // txtERF3
            // 
            this.txtERF3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtERF3.Location = new System.Drawing.Point(0, 0);
            this.txtERF3.Name = "txtERF3";
            this.txtERF3.Size = new System.Drawing.Size(689, 403);
            this.txtERF3.TabIndex = 0;
            this.txtERF3.Text = "";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Controls.Add(this.lboxERF, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblERFStructure, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.progressBar1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.layoutERFTab.SetRowSpan(this.tableLayoutPanel1, 2);
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(345, 431);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // lblERFStructure
            // 
            this.lblERFStructure.AutoSize = true;
            this.lblERFStructure.BackColor = System.Drawing.SystemColors.Window;
            this.lblERFStructure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.lblERFStructure, 2);
            this.lblERFStructure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblERFStructure.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblERFStructure.Location = new System.Drawing.Point(3, 32);
            this.lblERFStructure.Name = "lblERFStructure";
            this.lblERFStructure.Size = new System.Drawing.Size(339, 32);
            this.lblERFStructure.TabIndex = 1;
            this.lblERFStructure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Location = new System.Drawing.Point(288, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 26);
            this.button1.TabIndex = 2;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.ForeColor = System.Drawing.Color.Green;
            this.progressBar1.Location = new System.Drawing.Point(4, 3);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4, 3, 2, 3);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(279, 26);
            this.progressBar1.TabIndex = 3;
            // 
            // AuroraEngine_Utility
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 483);
            this.Controls.Add(this.layoutERFTab);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AuroraEngine_Utility";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AuroraEngine_Utility_FormClosing);
            this.Resize += new System.EventHandler(this.AuroraEngine_Utility_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.layoutERFTab.ResumeLayout(false);
            this.ERFTab.ResumeLayout(false);
            this.tpERF1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tpERF2.ResumeLayout(false);
            this.tpERF3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TableLayoutPanel layoutERFTab;
        private System.Windows.Forms.ListBox lboxERF;
        private System.Windows.Forms.RichTextBox txtERF1;
        private System.Windows.Forms.TabControl ERFTab;
        private System.Windows.Forms.TabPage tpERF1;
        private System.Windows.Forms.TabPage tpERF2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cmnuColumns;
        private System.Windows.Forms.ToolStripMenuItem cmnu4Columns;
        private System.Windows.Forms.ToolStripMenuItem cmnu8Columns;
        private System.Windows.Forms.ToolStripMenuItem cmnu16Columns;
        private System.Windows.Forms.ToolStripMenuItem cmnu32Columns;
        private System.Windows.Forms.TabPage tpERF3;
        private System.Windows.Forms.RichTextBox txtERF2;
        private System.Windows.Forms.RichTextBox txtERF3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblERFStructure;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

