using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GestureDrawingTimer.components;
using GestureDrawingTimer.models;

namespace GestureDrawingTimer.viewmodels
{
    public class SessionViewModel : BaseINPC
    {
        // Listener interface to notify other app components of relevant events
        public interface ISessionViewListener
        {
            void Back_Action();
        }
        private ISessionViewListener mListener;
        public void SetListener(ISessionViewListener listener) { mListener = listener; }

        // Properties
        private int _currentImageIndex;
        // TODO Session doesn't need this property...
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
        private string _currentImagePath;
        public string CurrentImagePath
        {
            get { return _currentImagePath; }
            private set
            {
                if (value.Equals(_currentImagePath)) return;
                _currentImagePath = value;
                OnPropertyChanged();
                // start/restart timer
                mTimer.Restart();
                SessionState = Session.SessionState.Started;
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
        public Session.SessionState SessionState
        {
            get { return mSession.State; }
            private set
            {
                if (value == mSession.State) return;
                mSession.State = value;
                OnPropertyChanged();
            }
        }

        // Instance variables
        private Session mSession;
        private List<string> mShuffledImagePaths;
        private SecondsTimer mTimer;
        private SoundManager mSoundManager;

        // Constructor
        public SessionViewModel(Session session)
        {
            // Set instance variables
            mSession = session;
            mTimer = new SecondsTimer(mSession.Interval);
            mSoundManager = new SoundManager();
            // shuffle Session's list of image paths
            Random randy = new Random();
            mShuffledImagePaths = mSession.ImagePaths.OrderBy(path => randy.Next()).ToList();

            // Handle changes to SecondsTimer's properties
            mTimer.PropertyChanged += (sender, args) =>
            {
                switch (args.PropertyName)
                {
                    case "State":
                        IntervalTimerState_Change(mTimer.State);
                        break;
                    case "RemainingSeconds":
                        IntervalTimerRemainingSeconds_Change(mTimer.RemainingSeconds);
                        break;
                }
            };

            // Handle changes to Session's properties
            mSession.PropertyChanged += (sender, args) =>
            {
                switch (args.PropertyName)
                {
                    case "State":
                        SessionState_Change(mSession.State);
                        break;
                }
            };

            // Initialize view to SecondsTimer's properties
            IntervalTimerState_Change(mTimer.State);
            IntervalTimerRemainingSeconds_Change(mTimer.RemainingSeconds);

            // Initialize view to Session's properties
            SessionState_Change(mSession.State);

            // Display first image
            CurrentImageIndex = 0;
            CurrentImagePath = mShuffledImagePaths[CurrentImageIndex];

            // Start the interval timer by setting SessionState to Started
            SessionState = Session.SessionState.Started;
        }

        // Public methods
        public void Back_Action()
        {
            // Notify listener if it exists
            if (mListener != null)
            {
                mListener.Back_Action();
            }
        }

        public void KeyDown_Action(KeyEventArgs args)
        {
            switch (args.Key)
            {
                case Key.Left:
                    ShowPreviousImage();
                    break;
                case Key.Right:
                    ShowNextImage();
                    break;
                case Key.Space:
                    PauseResumeButton_Click(); // handle as if the pause/resume button was clicked, since we already have the method defined
                    break;
            }
        }

        public void PrevButton_Click() { ShowPreviousImage(); }

        public void NextButton_Click() { ShowNextImage(); }

        public void PauseResumeButton_Click()
        {
            switch (mSession.State)
            {
                case Session.SessionState.Started:
                    SessionState = Session.SessionState.Paused;
                    break;
                case Session.SessionState.Paused:
                    SessionState = Session.SessionState.Started;
                    break;
            }
        }

        public void OpenImageFolder()
        {
            // Open Windows Explorer to the current image's folder, with the current image file selected
            System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{CurrentImagePath}\""); // see this answer: https://stackoverflow.com/a/13680458
        }

        // Private methods
        private void ShowNextImage()
        {
            CurrentImageIndex = Mod(CurrentImageIndex + 1, mShuffledImagePaths.Count);
            CurrentImagePath = mShuffledImagePaths[CurrentImageIndex];
        }

        private void ShowPreviousImage()
        {
            CurrentImageIndex = Mod(CurrentImageIndex - 1, mShuffledImagePaths.Count);
            CurrentImagePath = mShuffledImagePaths[CurrentImageIndex];
        }

        /// <summary>
        /// Nifty little helper function implementing expected modulo (%) behavior (i.e. result is always positive)
        /// From https://stackoverflow.com/a/23214321
        /// </summary>
        /// <param name="k"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        private int Mod(int k, int m)
        {
            return (k %= m) < 0 ? k + m : k;
        }

        private void IntervalTimerState_Change(SecondsTimer.TimerState state)
        {
            switch (state)
            {
                case SecondsTimer.TimerState.Started:
                    break;
                case SecondsTimer.TimerState.Elapsed:
                    ShowNextImage();
                    break;
            }
        }

        private void IntervalTimerRemainingSeconds_Change(int seconds)
        {
            this.RemainingSeconds = seconds;
            // Play beeps on final seconds
            // TODO add ability to enable/disable beeps
            if (seconds == 0)
            {
                mSoundManager.Play(SoundManager.Sound.Beep1);
            } else if (seconds < 4)
            {
                mSoundManager.Play(SoundManager.Sound.Beep2);
            }
        }

        private void SessionState_Change(Session.SessionState state)
        {
            switch (state)
            {
                case Session.SessionState.Started:
                    mTimer.Start();
                    break;
                case Session.SessionState.Paused:
                    mTimer.Pause();
                    break;
            }
        }
    }
}
