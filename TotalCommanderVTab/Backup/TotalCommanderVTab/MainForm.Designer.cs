namespace TotalCommanderVTab
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( MainForm ) );
            this.bt_Ok = new System.Windows.Forms.Button();
            this.bt_Abort = new System.Windows.Forms.Button();
            this.lv_Directories = new System.Windows.Forms.ListView();
            this.cb_OpenPathInNewTab = new System.Windows.Forms.CheckBox();
            this.cb_OpenPathInSourceTab = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // bt_Ok
            // 
            this.bt_Ok.Location = new System.Drawing.Point( 287, 230 );
            this.bt_Ok.Name = "bt_Ok";
            this.bt_Ok.Size = new System.Drawing.Size( 135, 29 );
            this.bt_Ok.TabIndex = 0;
            this.bt_Ok.Text = "OK";
            this.bt_Ok.UseVisualStyleBackColor = true;
            this.bt_Ok.Click += new System.EventHandler( this.bt_Ok_Click );
            // 
            // bt_Abort
            // 
            this.bt_Abort.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bt_Abort.Location = new System.Drawing.Point( 287, 265 );
            this.bt_Abort.Name = "bt_Abort";
            this.bt_Abort.Size = new System.Drawing.Size( 135, 29 );
            this.bt_Abort.TabIndex = 1;
            this.bt_Abort.Text = "Abort";
            this.bt_Abort.UseVisualStyleBackColor = true;
            this.bt_Abort.Click += new System.EventHandler( this.bt_Abort_Click );
            // 
            // lv_Directories
            // 
            this.lv_Directories.Font = new System.Drawing.Font( "Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
            this.lv_Directories.FullRowSelect = true;
            this.lv_Directories.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lv_Directories.HideSelection = false;
            this.lv_Directories.Location = new System.Drawing.Point( 12, 12 );
            this.lv_Directories.MultiSelect = false;
            this.lv_Directories.Name = "lv_Directories";
            this.lv_Directories.Size = new System.Drawing.Size( 410, 199 );
            this.lv_Directories.TabIndex = 2;
            this.lv_Directories.UseCompatibleStateImageBehavior = false;
            this.lv_Directories.View = System.Windows.Forms.View.Details;
            this.lv_Directories.SelectedIndexChanged += new System.EventHandler( this.lv_Directories_SelectedIndexChanged );
            // 
            // cb_OpenPathInNewTab
            // 
            this.cb_OpenPathInNewTab.AutoSize = true;
            this.cb_OpenPathInNewTab.Location = new System.Drawing.Point( 12, 272 );
            this.cb_OpenPathInNewTab.Name = "cb_OpenPathInNewTab";
            this.cb_OpenPathInNewTab.Size = new System.Drawing.Size( 133, 17 );
            this.cb_OpenPathInNewTab.TabIndex = 3;
            this.cb_OpenPathInNewTab.Text = "Open Path in new Tab";
            this.cb_OpenPathInNewTab.UseVisualStyleBackColor = true;
            this.cb_OpenPathInNewTab.CheckedChanged += new System.EventHandler( this.cb_OpenPathInNewTab_CheckedChanged );
            // 
            // cb_OpenPathInSourceTab
            // 
            this.cb_OpenPathInSourceTab.AutoSize = true;
            this.cb_OpenPathInSourceTab.Location = new System.Drawing.Point( 12, 237 );
            this.cb_OpenPathInSourceTab.Name = "cb_OpenPathInSourceTab";
            this.cb_OpenPathInSourceTab.Size = new System.Drawing.Size( 147, 17 );
            this.cb_OpenPathInSourceTab.TabIndex = 3;
            this.cb_OpenPathInSourceTab.Text = "Open Path in Source Tab";
            this.cb_OpenPathInSourceTab.UseVisualStyleBackColor = true;
            this.cb_OpenPathInSourceTab.CheckedChanged += new System.EventHandler( this.cb_OpenPathInSourceTab_CheckedChanged );
            // 
            // MainForm
            // 
            this.AcceptButton = this.bt_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bt_Abort;
            this.ClientSize = new System.Drawing.Size( 434, 306 );
            this.Controls.Add( this.cb_OpenPathInSourceTab );
            this.Controls.Add( this.cb_OpenPathInNewTab );
            this.Controls.Add( this.lv_Directories );
            this.Controls.Add( this.bt_Abort );
            this.Controls.Add( this.bt_Ok );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TotalCommanderVTab";
            this.TopMost = true;
            this.Load += new System.EventHandler( this.MainForm_Load );
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler( this.MainForm_FormClosed );
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.MainForm_KeyPress );
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_Ok;
        private System.Windows.Forms.Button bt_Abort;
        private System.Windows.Forms.ListView lv_Directories;
        private System.Windows.Forms.CheckBox cb_OpenPathInNewTab;
        private System.Windows.Forms.CheckBox cb_OpenPathInSourceTab;
    }
}

