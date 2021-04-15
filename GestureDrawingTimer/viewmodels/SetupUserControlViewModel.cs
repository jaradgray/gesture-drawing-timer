using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestureDrawingTimer.models;

namespace GestureDrawingTimer.viewmodels
{
    public class SetupUserControlViewModel : BaseINPC
    {
        // Properties and their backing fields
        private string _selectedFolderPath;
        public string SelectedFolderPath
        {
            get { return _selectedFolderPath; }
            private set
            {
                if (value == null || value.Equals(_selectedFolderPath)) return;
                _selectedFolderPath = value;
                OnPropertyChanged();
                // Update image-related members
                UpdateImagePaths();
            }
        }
        private int _numImages;
        public int NumImages
        {
            get { return _numImages; }
            private set
            {
                if (value == _numImages) return;
                _numImages = value;
                OnPropertyChanged();
            }
        }
        private int _numSubfolders;
        public int NumSubfolders
        {
            get { return _numSubfolders; }
            private set
            {
                if (value == _numSubfolders) return;
                _numSubfolders = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// The number of seconds an image will be displayed before switching to the next image
        /// </summary>
        public int Interval
        {
            get { return mSession.Interval; }
            set
            {
                if (value == mSession.Interval) return;
                mSession.Interval = value;
                OnPropertyChanged();
                // Persist interval value
                Properties.Settings.Default.ImageInterval = mSession.Interval;
                Properties.Settings.Default.Save(); // persist value across application sessions
            }
        }

        // Instance variables
        private Session mSession;

        // Constructor
        public SetupUserControlViewModel(Session session)
        {
            mSession = session;
            // Initialize properties from persisted data
            SelectedFolderPath = Properties.Settings.Default.SelectedFolderPath;
            Interval = Properties.Settings.Default.ImageInterval;
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

        /// <summary>
        /// Sets mImagePaths to contain paths of all image files in the selected folder and all
        /// of its subfolders, and updates NumImages and NumSubfolders properties.
        /// </summary>
        private void UpdateImagePaths()
        {
            // Get paths of all supported image files in the currently selected folder and all of its subfolders
            string[] validExtensions = { ".jpg", ".jpeg", ".png", ".bmp" };
            List<string> paths = GetFilePathsInDirectory(SelectedFolderPath, true, validExtensions);

            // Handle error
            if (paths == null)
            {
                // GetFilesPathsInDirectory() returned null, so the folder at SelectedFolderPath doesn't exist
                SelectedFolderPath = "";
                return;
            }

            // Update properties
            mSession.ImagePaths = paths;
            NumImages = mSession.ImagePaths.Count;
            string[] allSubfolders = System.IO.Directory.GetDirectories(SelectedFolderPath, "*", System.IO.SearchOption.AllDirectories);
            NumSubfolders = allSubfolders.Length;
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

            // Handle given directory doesn't exist
            if (!System.IO.Directory.Exists(dirPath))
            {
                return null;
            }

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
