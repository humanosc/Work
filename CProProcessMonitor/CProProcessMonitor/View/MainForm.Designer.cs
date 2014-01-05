namespace CProProcessMonitor.View
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent ()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lbl_CPUDesc = new System.Windows.Forms.Label();
            this.lbl_MemoryDesc = new System.Windows.Forms.Label();
            this.lbl_CPU = new System.Windows.Forms.Label();
            this.lbl_Memory = new System.Windows.Forms.Label();
            this.lbl_CLRMemoryDesc = new System.Windows.Forms.Label();
            this.lbl_CLRMemory = new System.Windows.Forms.Label();
            this.tblp_View = new System.Windows.Forms.TableLayoutPanel();
            this.ni_MainForm = new System.Windows.Forms.NotifyIcon(this.components);
            this.cms_MainForm = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsm_ShowMonitor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_HideMonitor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tscb_TimerInterval = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsm_ShowLog = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_ClearLog = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_OpenLogfolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_CleanupLogfolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsm_GenerateDiagram = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_GenerateCpuDiagram = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_GenerateMemoryDiagram = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_GenerateClrMemoryDiagram = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsm_About = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsm_CloseMonitor = new System.Windows.Forms.ToolStripMenuItem();
            this.tblp_View.SuspendLayout();
            this.cms_MainForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_CPUDesc
            // 
            this.lbl_CPUDesc.AutoSize = true;
            this.lbl_CPUDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_CPUDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CPUDesc.Location = new System.Drawing.Point(4, 1);
            this.lbl_CPUDesc.Name = "lbl_CPUDesc";
            this.lbl_CPUDesc.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lbl_CPUDesc.Size = new System.Drawing.Size(57, 27);
            this.lbl_CPUDesc.TabIndex = 0;
            this.lbl_CPUDesc.Text = "CPU:";
            this.lbl_CPUDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_MemoryDesc
            // 
            this.lbl_MemoryDesc.AutoSize = true;
            this.lbl_MemoryDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_MemoryDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MemoryDesc.Location = new System.Drawing.Point(4, 29);
            this.lbl_MemoryDesc.Name = "lbl_MemoryDesc";
            this.lbl_MemoryDesc.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lbl_MemoryDesc.Size = new System.Drawing.Size(57, 31);
            this.lbl_MemoryDesc.TabIndex = 0;
            this.lbl_MemoryDesc.Text = "Memory:";
            this.lbl_MemoryDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_CPU
            // 
            this.lbl_CPU.AutoSize = true;
            this.lbl_CPU.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_CPU.Location = new System.Drawing.Point(68, 1);
            this.lbl_CPU.Name = "lbl_CPU";
            this.lbl_CPU.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lbl_CPU.Size = new System.Drawing.Size(39, 27);
            this.lbl_CPU.TabIndex = 1;
            this.lbl_CPU.Text = "0.00 %";
            this.lbl_CPU.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Memory
            // 
            this.lbl_Memory.AutoSize = true;
            this.lbl_Memory.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_Memory.Location = new System.Drawing.Point(68, 29);
            this.lbl_Memory.Name = "lbl_Memory";
            this.lbl_Memory.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lbl_Memory.Size = new System.Drawing.Size(47, 31);
            this.lbl_Memory.TabIndex = 2;
            this.lbl_Memory.Text = "0.00 MB";
            this.lbl_Memory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_CLRMemoryDesc
            // 
            this.lbl_CLRMemoryDesc.AutoSize = true;
            this.lbl_CLRMemoryDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_CLRMemoryDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CLRMemoryDesc.Location = new System.Drawing.Point(4, 61);
            this.lbl_CLRMemoryDesc.Name = "lbl_CLRMemoryDesc";
            this.lbl_CLRMemoryDesc.Size = new System.Drawing.Size(57, 32);
            this.lbl_CLRMemoryDesc.TabIndex = 0;
            this.lbl_CLRMemoryDesc.Text = "CLR Memory:";
            // 
            // lbl_CLRMemory
            // 
            this.lbl_CLRMemory.AutoSize = true;
            this.lbl_CLRMemory.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_CLRMemory.Location = new System.Drawing.Point(68, 61);
            this.lbl_CLRMemory.Name = "lbl_CLRMemory";
            this.lbl_CLRMemory.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lbl_CLRMemory.Size = new System.Drawing.Size(47, 32);
            this.lbl_CLRMemory.TabIndex = 2;
            this.lbl_CLRMemory.Text = "0.00 MB";
            this.lbl_CLRMemory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tblp_View
            // 
            this.tblp_View.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblp_View.ColumnCount = 2;
            this.tblp_View.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.83562F));
            this.tblp_View.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.16438F));
            this.tblp_View.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            this.tblp_View.Controls.Add(this.lbl_MemoryDesc, 0, 1);
            this.tblp_View.Controls.Add(this.lbl_CLRMemoryDesc, 0, 2);
            this.tblp_View.Controls.Add(this.lbl_CPUDesc, 0, 0);
            this.tblp_View.Controls.Add(this.lbl_CLRMemory, 1, 2);
            this.tblp_View.Controls.Add(this.lbl_CPU, 1, 0);
            this.tblp_View.Controls.Add(this.lbl_Memory, 1, 1);
            this.tblp_View.Location = new System.Drawing.Point(12, 12);
            this.tblp_View.Name = "tblp_View";
            this.tblp_View.RowCount = 3;
            this.tblp_View.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.77419F));
            this.tblp_View.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.22581F));
            this.tblp_View.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tblp_View.Size = new System.Drawing.Size(147, 94);
            this.tblp_View.TabIndex = 3;
            // 
            // ni_MainForm
            // 
            this.ni_MainForm.ContextMenuStrip = this.cms_MainForm;
            this.ni_MainForm.Icon = ((System.Drawing.Icon)(resources.GetObject("ni_MainForm.Icon")));
            this.ni_MainForm.Text = "CPro Process Monitor";
            this.ni_MainForm.Visible = true;
            // 
            // cms_MainForm
            // 
            this.cms_MainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_ShowMonitor,
            this.tsm_HideMonitor,
            this.toolStripMenuItem2,
            this.tscb_TimerInterval,
            this.toolStripMenuItem3,
            this.tsm_ShowLog,
            this.tsm_ClearLog,
            this.tsm_OpenLogfolder,
            this.tsm_CleanupLogfolder,
            this.toolStripMenuItem1,
            this.tsm_GenerateDiagram,
            this.tsm_GenerateCpuDiagram,
            this.tsm_GenerateMemoryDiagram,
            this.tsm_GenerateClrMemoryDiagram,
            this.toolStripSeparator1,
            this.tsm_About,
            this.toolStripMenuItem4,
            this.tsm_CloseMonitor});
            this.cms_MainForm.Name = "cms_MainForm";
            this.cms_MainForm.Size = new System.Drawing.Size(256, 325);
            // 
            // tsm_ShowMonitor
            // 
            this.tsm_ShowMonitor.Name = "tsm_ShowMonitor";
            this.tsm_ShowMonitor.Size = new System.Drawing.Size(255, 22);
            this.tsm_ShowMonitor.Text = "Show Monitor";
            // 
            // tsm_HideMonitor
            // 
            this.tsm_HideMonitor.Name = "tsm_HideMonitor";
            this.tsm_HideMonitor.Size = new System.Drawing.Size(255, 22);
            this.tsm_HideMonitor.Text = "Hide Monitor";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(252, 6);
            // 
            // tscb_TimerInterval
            // 
            this.tscb_TimerInterval.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tscb_TimerInterval.AutoToolTip = true;
            this.tscb_TimerInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscb_TimerInterval.DropDownWidth = 196;
            this.tscb_TimerInterval.Name = "tscb_TimerInterval";
            this.tscb_TimerInterval.Size = new System.Drawing.Size(195, 23);
            this.tscb_TimerInterval.ToolTipText = "Timer interval";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(252, 6);
            // 
            // tsm_ShowLog
            // 
            this.tsm_ShowLog.Name = "tsm_ShowLog";
            this.tsm_ShowLog.Size = new System.Drawing.Size(255, 22);
            this.tsm_ShowLog.Text = "Show Log";
            // 
            // tsm_ClearLog
            // 
            this.tsm_ClearLog.Name = "tsm_ClearLog";
            this.tsm_ClearLog.Size = new System.Drawing.Size(255, 22);
            this.tsm_ClearLog.Text = "Clear Log";
            // 
            // tsm_OpenLogfolder
            // 
            this.tsm_OpenLogfolder.Name = "tsm_OpenLogfolder";
            this.tsm_OpenLogfolder.Size = new System.Drawing.Size(255, 22);
            this.tsm_OpenLogfolder.Text = "Open Logfolder";
            // 
            // tsm_CleanupLogfolder
            // 
            this.tsm_CleanupLogfolder.Name = "tsm_CleanupLogfolder";
            this.tsm_CleanupLogfolder.Size = new System.Drawing.Size(255, 22);
            this.tsm_CleanupLogfolder.Text = "Cleanup Logfolder";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(252, 6);
            // 
            // tsm_GenerateDiagram
            // 
            this.tsm_GenerateDiagram.Name = "tsm_GenerateDiagram";
            this.tsm_GenerateDiagram.Size = new System.Drawing.Size(255, 22);
            this.tsm_GenerateDiagram.Text = "Generate Diagram";
            // 
            // tsm_GenerateCpuDiagram
            // 
            this.tsm_GenerateCpuDiagram.Name = "tsm_GenerateCpuDiagram";
            this.tsm_GenerateCpuDiagram.Size = new System.Drawing.Size(255, 22);
            this.tsm_GenerateCpuDiagram.Text = "Generate CPU Diagram";
            // 
            // tsm_GenerateMemoryDiagram
            // 
            this.tsm_GenerateMemoryDiagram.Name = "tsm_GenerateMemoryDiagram";
            this.tsm_GenerateMemoryDiagram.Size = new System.Drawing.Size(255, 22);
            this.tsm_GenerateMemoryDiagram.Text = "Generate Memory Diagram";
            // 
            // tsm_GenerateClrMemoryDiagram
            // 
            this.tsm_GenerateClrMemoryDiagram.Name = "tsm_GenerateClrMemoryDiagram";
            this.tsm_GenerateClrMemoryDiagram.Size = new System.Drawing.Size(255, 22);
            this.tsm_GenerateClrMemoryDiagram.Text = "Generate CLR-Memory Diagram";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(252, 6);
            // 
            // tsm_About
            // 
            this.tsm_About.Name = "tsm_About";
            this.tsm_About.Size = new System.Drawing.Size(255, 22);
            this.tsm_About.Text = "About";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(252, 6);
            // 
            // tsm_CloseMonitor
            // 
            this.tsm_CloseMonitor.Name = "tsm_CloseMonitor";
            this.tsm_CloseMonitor.Size = new System.Drawing.Size(255, 22);
            this.tsm_CloseMonitor.Text = "Close Monitor";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(171, 118);
            this.ControlBox = false;
            this.Controls.Add(this.tblp_View);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Opacity = 0.75D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "CPro Process Monitor";
            this.TopMost = true;
            this.tblp_View.ResumeLayout(false);
            this.tblp_View.PerformLayout();
            this.cms_MainForm.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.Label lbl_CPUDesc;
        private System.Windows.Forms.Label lbl_MemoryDesc;
        private System.Windows.Forms.Label lbl_CPU;
        private System.Windows.Forms.Label lbl_Memory;
        private System.Windows.Forms.Label lbl_CLRMemoryDesc;
        private System.Windows.Forms.Label lbl_CLRMemory;
        private System.Windows.Forms.TableLayoutPanel tblp_View;
        private System.Windows.Forms.NotifyIcon ni_MainForm;
        private System.Windows.Forms.ContextMenuStrip cms_MainForm;
        private System.Windows.Forms.ToolStripMenuItem tsm_ShowMonitor;
        private System.Windows.Forms.ToolStripMenuItem tsm_GenerateDiagram;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsm_CloseMonitor;
        private System.Windows.Forms.ToolStripMenuItem tsm_HideMonitor;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tsm_ShowLog;
        private System.Windows.Forms.ToolStripMenuItem tsm_OpenLogfolder;
        private System.Windows.Forms.ToolStripMenuItem tsm_CleanupLogfolder;
        private System.Windows.Forms.ToolStripMenuItem tsm_About;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem tsm_GenerateCpuDiagram;
        private System.Windows.Forms.ToolStripMenuItem tsm_GenerateMemoryDiagram;
        private System.Windows.Forms.ToolStripMenuItem tsm_GenerateClrMemoryDiagram;
        private System.Windows.Forms.ToolStripComboBox tscb_TimerInterval;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem tsm_ClearLog;
    }
}

