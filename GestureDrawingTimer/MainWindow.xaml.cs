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
using GestureDrawingTimer.viewmodels;

namespace GestureDrawingTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Instance variables
        private MainWindowViewModel mViewModel;


        // Constructor
        public MainWindow()
        {
            InitializeComponent();

            // Get a ViewModel
            mViewModel = new MainWindowViewModel();

            // Handle changes to ViewModel's state
            mViewModel.PropertyChanged += (sender, args) =>
            {
                switch (args.PropertyName)
                {
                    case "SelectedFolderPath":
                        SyncSelectedFolderPath();
                        break;
                    case "NumImages":
                        imagesInFolderTextBlock.Text = $"Found {mViewModel.NumImages} {(mViewModel.NumImages == 1 ? "image" : "images")} in {mViewModel.NumSubfolders} {(mViewModel.NumSubfolders == 1 ? "subfolder" : "subfolders")}";
                        break;
                    case "NumSubfolders":
                        imagesInFolderTextBlock.Text = $"Found {mViewModel.NumImages} {(mViewModel.NumImages == 1 ? "image" : "images")} in {mViewModel.NumSubfolders} {(mViewModel.NumSubfolders == 1 ? "subfolder" : "subfolders")}";
                        break;
                    case "Interval":
                        SyncIntervalButtons();
                        break;
                }
            };

            // Initialize views to ViewModel's state
            SyncSelectedFolderPath();
            imagesInFolderTextBlock.Text = $"Found {mViewModel.NumImages} {(mViewModel.NumImages == 1 ? "image" : "images")} in {mViewModel.NumSubfolders} {(mViewModel.NumSubfolders == 1 ? "subfolder" : "subfolders")}";
            SyncIntervalButtons();
        }


        // UI event handlers

        private void SelectFolderButton_Click(object sender, RoutedEventArgs e)
        {
            mViewModel.SelectFolderButton_Click();
        }

        /// <summary>
        /// Sets ViewModel's Interval property based on the clicked interval Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IntervalButton_Click(object sender, RoutedEventArgs e)
        {
            // Get an interval value based on the clicked Button
            Button clickedButton = (Button)sender;
            int interval = (clickedButton == intervalButton_1m) ? 60 * 1 :
                clickedButton == intervalButton_2m ? 60 * 2 :
                clickedButton == intervalButton_3m ? 60 * 3 :
                clickedButton == intervalButton_5m ? 60 * 5 :
                clickedButton == intervalButton_7m ? 60 * 7 :
                clickedButton == intervalButton_10m ? 60 * 10 :
                -1;

            // Check for errors
            if (interval == -1)
            {
                string msg = "IntervalButton_Click(): clicked Button didn't match any Button references";
                Console.Error.WriteLine(msg);
                MessageBox.Show("IntervalButton_Click(): clicked Button didn't match any Button references");
                return;
            }

            // Set ViewModel's Interval property
            mViewModel.Interval = interval;
        }


        // Private methods

        private void SyncSelectedFolderPath()
        {
            string path = mViewModel.SelectedFolderPath;

            // Handle no folder selected
            if (path.Equals(""))
            {
                folderTextBlock.Text = "No folder selected";
                imagesInFolderTextBlock.Visibility = Visibility.Collapsed;
                return;
            }

            folderTextBlock.Text = $"Folder: {mViewModel.SelectedFolderPath}";
            // we'll restore imagesInFolderTextBlock's visibility here because this is the method that can change it to Collapsed
            imagesInFolderTextBlock.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Sets styles of interval Buttons based on mViewModel's Interval property
        /// </summary>
        private void SyncIntervalButtons()
        {
            // Get convenient references to all the buttons
            Button btnSelectedInterval = null;
            List<Button> buttons = new List<Button>();
            buttons.Add(intervalButton_1m);
            buttons.Add(intervalButton_2m);
            buttons.Add(intervalButton_3m);
            buttons.Add(intervalButton_5m);
            buttons.Add(intervalButton_7m);
            buttons.Add(intervalButton_10m);

            // Decide which button represents the currently set interval
            switch (mViewModel.Interval)
            {
                case 60 * 1:
                    btnSelectedInterval = intervalButton_1m;
                    break;
                case 60 * 2:
                    btnSelectedInterval = intervalButton_2m;
                    break;
                case 60 * 3:
                    btnSelectedInterval = intervalButton_3m;
                    break;
                case 60 * 5:
                    btnSelectedInterval = intervalButton_5m;
                    break;
                case 60 * 7:
                    btnSelectedInterval = intervalButton_7m;
                    break;
                case 60 * 10:
                    btnSelectedInterval = intervalButton_10m;
                    break;
            }

            // Set styles of interval buttons
            foreach (Button btn in buttons)
            {
                btn.Style = (btn == btnSelectedInterval) ? (Style)FindResource("Button.HighEmphasis") : (Style)FindResource("Button.Normal");
            }
        }
    }
}
