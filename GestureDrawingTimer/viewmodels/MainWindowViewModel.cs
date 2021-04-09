using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestureDrawingTimer.viewmodels
{
    class MainWindowViewModel : ViewModelBase
    {
        // Properties and their backing fields

        private string _selectedFolderPath;
        public string SelectedFolderPath
        {
            get { return _selectedFolderPath; }
            private set
            {
                if (value != null && !value.Equals(_selectedFolderPath))
                {
                    _selectedFolderPath = value;
                    OnPropertyChanged();

                    // TODO update image-related members
                    //  - list of image paths
                    //  - number of images (elements in image paths list)
                    //  - number of subfolders
                    UpdateImagePaths();
                }
            }
        }


        // Private variables
        private List<string> mImagePaths = new List<string>();


        // Constructor
        public MainWindowViewModel()
        {
            // Initialize DirPath from persisted data
            SelectedFolderPath = Properties.Settings.Default.SelectedFolderPath;
        }


        // Public methods

        public void SelectFolderButton_Click()
        {
            // Show an OpenFileDialog (because .NET doesn't have a select folder dialog)
            //  and get the selected file's directory
            var ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.InitialDirectory = SelectedFolderPath; // set ofd's initial directory to persisted SelectedFolderPath
            if (ofd.ShowDialog() == true)
            {
                string path = System.IO.Path.GetDirectoryName(ofd.FileName);
                // persist selected folder's path
                Properties.Settings.Default.SelectedFolderPath = path;
                Properties.Settings.Default.Save(); // persist settings across application settings
                // update member
                SelectedFolderPath = path;
            }
        }


        // Private methods

        private void UpdateImagePaths()
        {
            // Get paths of all supported image files in the currently selected folder and all of its subfolders
            string[] validExtensions = { ".jpg", ".jpeg", ".png", ".bmp" };
            mImagePaths = GetFilePathsInDirectory(SelectedFolderPath, true, validExtensions);

            // TODO update members
            string msg = "Images found:\n";
            foreach (string str in mImagePaths)
            {
                msg += "\n" + str;
            }
            System.Windows.MessageBox.Show(msg);
        }

        /// <summary>
        /// Returns a List of all files in the directory at the given path whose extension matches one of those
        /// in @validExtensions, or all files if @validExtensions is null. Includes files from all subfolders
        /// if @doRecursive is true
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="doRecursive"></param>
        /// <param name="validExtensions"></param>
        /// <returns></returns>
        private List<string> GetFilePathsInDirectory(string dirPath, bool doRecursive, string[] validExtensions = null)
        {
            List<string> result = new List<string>();

            string[] allFiles = System.IO.Directory.GetFiles(dirPath, "*", doRecursive ? System.IO.SearchOption.AllDirectories : System.IO.SearchOption.TopDirectoryOnly);
            // add to result files whose extension is in the validExtensions array
            foreach (string file in allFiles)
            {
                foreach (string ext in validExtensions)
                {
                    if (ext.ToLowerInvariant().Equals(System.IO.Path.GetExtension(file).ToLowerInvariant()))
                    {
                        result.Add(file);
                        break;
                    }
                }
            }

            return result;
        }
    }
}
