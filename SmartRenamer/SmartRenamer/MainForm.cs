using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using gma.Drawing.ImageInfo;

namespace SmartRenamer
{
    public partial class Renamer : Form
    {
        struct FileItem
        {
            public string Path;
            public DateTime CreationDate;

            public override string ToString()
            {
                return string.Format( "{0} - {1}", CreationDate.ToString( "dd-MM-yyyy" ), System.IO.Path.GetFileName( Path ) );
            }
        }

        private List<FileItem> _fileitems;
        private IEnumerable<IGrouping<string, FileItem>> _groupedFileItems;

        public Renamer()
        {
            InitializeComponent();
        }

        private void lb_Files_Click(object sender, EventArgs e)
        {
            lb_Files.Items.Clear();

            try
            {
                if ( ofd_Files.ShowDialog() == DialogResult.OK )
                {
                    var filenames = new List<string>( ofd_Files.FileNames );

                    _fileitems = new List<FileItem>();
                    foreach ( var filename in filenames )
                    {
                        DateTime creationDate = DateTime.MinValue;

                        try
                        {
                            using ( Info info = new Info( filename ) )
                            {
                                creationDate = info.DTOrig;
                            }
                        }
                        catch
                        {
                            string text = string.Format( "The metadata of the file {0} does not contain the original creation date.\nDo you want to use the general file creation date instead ({1})?", filename, File.GetCreationTime( filename ) );
                            if ( MessageBox.Show( text, "Error...", MessageBoxButtons.YesNo, MessageBoxIcon.Error ) == DialogResult.Yes )
                            {
                                creationDate = File.GetCreationTime( filename );
                            }
                            else
                            {
                                return;
                            }
                        }

                        _fileitems.Add( new FileItem { Path = filename, CreationDate = creationDate} );
                    }

                    _fileitems.Sort( ( f1, f2 ) => f1.CreationDate.CompareTo( f2.CreationDate ) );

                    foreach ( var fileitem in _fileitems )
                    {
                        lb_Files.Items.Add( fileitem );
                    }

                    _groupedFileItems = from fi in _fileitems group fi by fi.CreationDate.ToString( "dd-MM-yyyy" );

                    lbl_Hint.Visible = false;
                    bt_Rename.Enabled = true;
                }
                else
                {
                    lbl_Hint.Visible = true;
                    bt_Rename.Enabled = false;
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show( this, ex.ToString(), "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        struct RenameFileItem
        {
            public string OldPath;
            public string NewPath;
        }

        private void bt_Rename_Click(object sender, EventArgs e)
        {
            List<RenameFileItem> renameFileItems = new List<RenameFileItem>();

            try
            {
                foreach (var fileitems in _groupedFileItems)
                {
                    int i = 1;
                    foreach (var fileitem in fileitems)
                    {
                        string newPath = Path.Combine(Path.GetDirectoryName(fileitem.Path), string.Format("{0}_{1}{2}", fileitems.Key, i.ToString("000"), Path.GetExtension(fileitem.Path)));

                        string tempPath = Path.Combine( Path.GetDirectoryName( fileitem.Path ), Guid.NewGuid().ToString() + Path.GetExtension( fileitem.Path ) );

                        File.Move( fileitem.Path, tempPath );

                        renameFileItems.Add( new RenameFileItem { OldPath = tempPath, NewPath = newPath } );  

                        i++;
                    }
                }

                List<string> alredyExistsFilenames = new List<string>();

                foreach ( var renameFileItem in renameFileItems )
                {
                    if ( File.Exists( renameFileItem.NewPath ) )
                    {
                        alredyExistsFilenames.Add( renameFileItem.NewPath );
                    }
                }
                
                if ( alredyExistsFilenames.Count > 0 )
                {
                    string messageText = string.Empty;

                    alredyExistsFilenames.ForEach( filename => messageText += filename + "\n" );

                    if ( MessageBox.Show( string.Format( "The following files already exists:\n{0}Do you want to replace them?", messageText ), "Replacement...", MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.No )
                    {
                        return; 
                    }
                }

                foreach ( var renameFileItem in renameFileItems )
                {
                    if ( renameFileItem.NewPath != renameFileItem.OldPath )
                    {
                        if ( File.Exists( renameFileItem.NewPath ) )
                        {
                            File.Delete( renameFileItem.NewPath );
                        }

                        File.Move( renameFileItem.OldPath, renameFileItem.NewPath );
                    }
                }

                MessageBox.Show( this, "Renaming successful completed.", "Info...", MessageBoxButtons.OK, MessageBoxIcon.Information);

                lb_Files.Items.Clear();
                lbl_Hint.Visible = true;
                bt_Rename.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show( this, ex.ToString(), "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Renamer_SizeChanged ( object sender, EventArgs e )
        {
            Size lblSize = lbl_Hint.Size;
            Point lbLoc = lb_Files.Location;
            Size lbSize = lb_Files.Size;

            int lbHalfHeight = lbSize.Height / 2;
            int lbHalfWidth = lbSize.Width / 2;

            int lblHalfHeight = lblSize.Height / 2;
            int lblHalfWidth = lblSize.Width / 2;

            lbl_Hint.Location = new Point( lbLoc.X + lbHalfWidth - lblHalfWidth, lbLoc.Y + lbHalfHeight - lblHalfHeight );
        }
    }
}
