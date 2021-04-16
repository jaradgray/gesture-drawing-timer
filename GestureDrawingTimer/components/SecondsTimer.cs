using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace GestureDrawingTimer.components
{
    /// <summary>
    /// A timer that, after it's started, counts down a given number of seconds.
    /// </summary>
    public class SecondsTimer : BaseINPC
    {
        public enum TimerState
        {
            Initialized,
            Started,
            Paused,
            Elapsed
        }

        // Properties
        private TimerState _state;
        public TimerState State
        {
            get { return _state; }
            private set
            {
                if (value == _state) return;
                _state = value;
                OnPropertyChanged();
            }
        }
        private int _remainingSeconds;
        public int RemainingSeconds
        {
            get { return _remainingSeconds; }
            private set
            {
                if (value == _remainingSeconds) return;
                _remainingSeconds = value;
                OnPropertyChanged();
            }
        }

        // Instance variables
        private int mTotalSeconds;
        private Timer mTimer;

        // Constructor
        public SecondsTimer(int totalSeconds)
        {
            // Initialize instance variables
            mTotalSeconds = totalSeconds;
            mTimer = new Timer();
            mTimer.Interval = 1000;
            mTimer.AutoReset = true;
            mTimer.Elapsed += Timer_Tick;

            // Initialize properties
            RemainingSeconds = totalSeconds;
            State = TimerState.Initialized;
        }

        // Public methods
        public void Start()
        {
            mTimer.Enabled = true;
            State = TimerState.Started;
        }

        public void Restart()
        {
            mTimer.Enabled = false;
            RemainingSeconds = mTotalSeconds;
            Start();
        }

        public void Pause()
        {
            mTimer.Enabled = false;
            State = TimerState.Paused;
        }

        // Private methods
        private void Timer_Tick(object sender, ElapsedEventArgs args)
        {
            // Do stuff that should happen every second the timer ticks
            RemainingSeconds--;
            if (RemainingSeconds == 0)
            {
                mTimer.Enabled = false;
                State = TimerState.Elapsed;
            }
        }
    }
}
