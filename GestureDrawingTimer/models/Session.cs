using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestureDrawingTimer.models
{
    // Represents a drawing session, during which images are displayed for a length of time
    public class Session : BaseINPC
    {
        public enum SessionState
        {
            Init,
            Started,
            Finished
        }


        // Properties and their backing fields
        private SessionState _state;
        public SessionState State
        {
            get { return _state; }
            private set
            {
                if (value == _state) return;
                _state = value;
                OnPropertyChanged();
            }
        }
        private int _interval;
        public int Interval
        {
            get { return _interval; }
            set
            {
                if (value == _interval) return;
                _interval = value;
                OnPropertyChanged();
            }
        }
        private List<string> _imagePaths;
        public List<string> ImagePaths
        {
            get { return _imagePaths; }
            set
            {
                if (value == _imagePaths) return;
                _imagePaths = value;
                OnPropertyChanged();
            }
        }
        private int _currentImageIndex;
        public int CurrentImageIndex
        {
            get { return _currentImageIndex; }
            private set
            {
                if (value == _currentImageIndex) return;
                _currentImageIndex = value;
                OnPropertyChanged();
            }
        }

        // Constructor
        public Session()
        {
            State = SessionState.Init;
            CurrentImageIndex = 0;
        }
    }
}
