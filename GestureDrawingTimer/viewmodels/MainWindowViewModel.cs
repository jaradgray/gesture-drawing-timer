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
                    // TODO get images in selected folder and subfolders
                }
            }
        }


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
    }
}
