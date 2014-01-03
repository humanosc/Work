namespace SmartRenamer
{
    partial class Renamer
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
            this.bt_Rename = new System.Windows.Forms.Button();
            this.lb_Files = new System.Windows.Forms.ListBox();
            this.lbl_Hint = new System.Windows.Forms.Label();
            this.ofd_Files = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // bt_Rename
            // 
            this.bt_Rename.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.bt_Rename.Enabled = false;
            this.bt_Rename.Location = new System.Drawing.Point( 573, 508 );
            this.bt_Rename.Margin = new System.Windows.Forms.Padding( 4, 5, 4, 5 );
            this.bt_Rename.Name = "bt_Rename";
            this.bt_Rename.Size = new System.Drawing.Size( 100, 41 );
            this.bt_Rename.TabIndex = 0;
            this.bt_Rename.Text = "Rename";
            this.bt_Rename.UseVisualStyleBackColor = true;
            this.bt_Rename.Click += new System.EventHandler( this.bt_Rename_Click );
            // 
            // lb_Files
            // 
            this.lb_Files.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.lb_Files.Font = new System.Drawing.Font( "Segoe Print", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
            this.lb_Files.FormattingEnabled = true;
            this.lb_Files.ItemHeight = 23;
            this.lb_Files.Location = new System.Drawing.Point( 16, 21 );
            this.lb_Files.Margin = new System.Windows.Forms.Padding( 4, 5, 4, 5 );
            this.lb_Files.Name = "lb_Files";
            this.lb_Files.Size = new System.Drawing.Size( 656, 441 );
            this.lb_Files.TabIndex = 1;
            this.lb_Files.Click += new System.EventHandler( this.lb_Files_Click );
            // 
            // lbl_Hint
            // 
            this.lbl_Hint.AutoSize = true;
            this.lbl_Hint.BackColor = System.Drawing.Color.White;
            this.lbl_Hint.Font = new System.Drawing.Font( "Segoe Print", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
            this.lbl_Hint.Location = new System.Drawing.Point( 245, 218 );
            this.lbl_Hint.Margin = new System.Windows.Forms.Padding( 4, 0, 4, 0 );
            this.lbl_Hint.Name = "lbl_Hint";
            this.lbl_Hint.Size = new System.Drawing.Size( 210, 28 );
            this.lbl_Hint.TabIndex = 2;
            this.lbl_Hint.Text = "Click here to select files...";
            this.lbl_Hint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Hint.Click += new System.EventHandler( this.lb_Files_Click );
            // 
            // ofd_Files
            // 
            this.ofd_Files.Filter = "All files|*.*";
            this.ofd_Files.Multiselect = true;
            this.ofd_Files.Title = "Please select files...";
            // 
            // Renamer
            // 
            this.AcceptButton = this.bt_Rename;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 8F, 23F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 689, 570 );
            this.Controls.Add( this.lbl_Hint );
            this.Controls.Add( this.lb_Files );
            this.Controls.Add( this.bt_Rename );
            this.Font = new System.Drawing.Font( "Segoe Print", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
            this.Margin = new System.Windows.Forms.Padding( 4, 5, 4, 5 );
            this.MinimumSize = new System.Drawing.Size( 705, 608 );
            this.Name = "Renamer";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Cherie\'s Smart Renamer";
            this.SizeChanged += new System.EventHandler( this.Renamer_SizeChanged );
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_Rename;
        private System.Windows.Forms.ListBox lb_Files;
        private System.Windows.Forms.Label lbl_Hint;
        private System.Windows.Forms.OpenFileDialog ofd_Files;
    }
}

