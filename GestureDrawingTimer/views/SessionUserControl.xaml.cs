using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GestureDrawingTimer.models;
using GestureDrawingTimer.viewmodels;

namespace GestureDrawingTimer.views
{
    /// <summary>
    /// Interaction logic for SlideshowUserControl.xaml
    /// </summary>
    public partial class SessionUserControl : UserControl
    {
        // Instance variables
        private SessionViewModel mViewModel;

        // Constructor
        public SessionUserControl(SessionViewModel viewModel)
        {
            InitializeComponent();

            mViewModel = viewModel;

            // Handle changes to ViewModel's properties
            mViewModel.PropertyChanged += (sender, args) =>
            {
                switch (args.PropertyName)
                {
                    case "CurrentImagePath":
                        // UI will be updated and it could be from a background thread, execute on main thread
                        this.Dispatcher.Invoke(() => CurrentImagePath_Change(mViewModel.CurrentImagePath));
                        break;
                    case "RemainingSeconds":
                        // UI will be update (from another thread); must execute on main thread
                        this.Dispatcher.Invoke(() => RemainingSeconds_Change(mViewModel.RemainingSeconds));
                        break;
                    case "SessionState":
                        this.Dispatcher.Invoke(() => SessionState_Change(mViewModel.SessionState)); // might happen from a background thread, execute on the main thread
                        break;
                }
            };
            // Initialize view to ViewModel's properties
            CurrentImagePath_Change(mViewModel.CurrentImagePath);
            RemainingSeconds_Change(mViewModel.RemainingSeconds);
            SessionState_Change(mViewModel.SessionState);
        }

        // UI event handlers
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            mViewModel.Back_Action();
        }


        // Private methods
        private void CurrentImagePath_Change(string path)
        {
            BitmapImage img = new BitmapImage(new Uri(path));
            imageContainer.Source = img;
        }

        private void RemainingSeconds_Change(int seconds)
        {
            int min = seconds / 60;
            int sec = seconds % 60;
            remainingTimeTextBlock.Text = string.Format("{0:D2}:{1:D2}", min, sec);
        }

        private void SessionState_Change(Session.SessionState state)
        {
            switch (state)
            {
                // TODO set Content to image instead of text
                case Session.SessionState.Started:
                    pauseResumeButton.Content = "Pause";
                    break;
                case Session.SessionState.Paused:
                    pauseResumeButton.Content = "Resume";
                    break;
            }
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            mViewModel.PrevButton_Click();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            mViewModel.NextButton_Click();
        }

        private void PauseResumeButton_Click(object sender, RoutedEventArgs e)
        {
            mViewModel.PauseResumeButton_Click();
        }
    }
}
