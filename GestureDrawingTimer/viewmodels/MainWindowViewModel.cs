using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestureDrawingTimer.models;
using static GestureDrawingTimer.viewmodels.SetupViewModel;

namespace GestureDrawingTimer.viewmodels
{
    class MainWindowViewModel : BaseINPC, ISetupViewListener
    {
        // Properties
        public Session Session;
        private object _activeContentViewModel;
        public object ActiveContentViewModel
        {
            get { return _activeContentViewModel; }
            private set
            {
                if (value == _activeContentViewModel) return;
                _activeContentViewModel = value;
                OnPropertyChanged();
            }
        }

        // Instance variables
        private SetupViewModel mSetupVM;

        // Constructor
        public MainWindowViewModel()
        {
            // Create a Session, initialize from persisted data
            Session = new Session();
            Session.Interval = Properties.Settings.Default.ImageInterval;

            mSetupVM = new SetupViewModel(Session);
            mSetupVM.SetListener(this);
            ActiveContentViewModel = mSetupVM;
        }

        // ISetupViewListener implementation
        public void StartSlideshow()
        {
            System.Windows.MessageBox.Show("MainWindow detected start button click!");
        }
    }
}
