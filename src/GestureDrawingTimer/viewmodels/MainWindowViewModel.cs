using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GestureDrawingTimer.models;
using static GestureDrawingTimer.viewmodels.SessionViewModel;
using static GestureDrawingTimer.viewmodels.SetupViewModel;

namespace GestureDrawingTimer.viewmodels
{
    class MainWindowViewModel : BaseINPC, ISetupViewListener, ISessionViewListener
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
        private SessionViewModel mSessionVM;

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
            mSessionVM = new SessionViewModel(Session);
            mSessionVM.SetListener(this);
            ActiveContentViewModel = mSessionVM;
        }

        // ISessionViewListener implementation
        public void Back_Action()
        {
            // TODO dispose mSessionVM ???
            mSessionVM = null;
            ActiveContentViewModel = mSetupVM;
        }

        // Public methods
        public void KeyDown_Action(KeyEventArgs args)
        {
            // Forward event to active viewModel
            if (ActiveContentViewModel == mSessionVM)
            {
                mSessionVM.KeyDown_Action(args);
            }
        }
    }
}
