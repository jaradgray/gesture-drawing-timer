using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestureDrawingTimer.models;

namespace GestureDrawingTimer.viewmodels
{
    class MainWindowViewModel : BaseINPC
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

        // Constructor
        public MainWindowViewModel()
        {
            // Create a Session, initialize from persisted data
            Session = new Session();
            Session.Interval = Properties.Settings.Default.ImageInterval;

            ActiveContentViewModel = new SetupViewModel(Session);
        }
    }
}
