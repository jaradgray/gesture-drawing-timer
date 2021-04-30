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

        private void BottomControlsContainer_MouseEnter(object sender, MouseEventArgs e)
        {
            bottomControlsContainer.Opacity = 1;
        }

        private void BottomControlsContainer_MouseLeave(object sender, MouseEventArgs e)
        {
            bottomControlsContainer.Opacity = 0;
        }

        private void OpenImageFolderButton_Click(object sender, RoutedEventArgs e)
        {
            mViewModel.OpenImageFolder();
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
            // Set pauseResumeButton's icon by setting its Style property,
            //  and set remainingTimeTextBlock's Background
            Path p;
            switch (state)
            {
                case Session.SessionState.Started:
                    p = (Path)pauseResumeButton.Content;
                    p.Style = (Style)FindResource("Icon.Pause");
                    remainingTimeTextBlock.Background = new SolidColorBrush(Color.FromArgb(0x7f, 0, 0, 0));
                    break;
                case Session.SessionState.Paused:
                    p = (Path)pauseResumeButton.Content;
                    p.Style = (Style)FindResource("Icon.Play");
                    remainingTimeTextBlock.Background = new SolidColorBrush(Color.FromArgb(0x7f, 255, 0, 0));
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
