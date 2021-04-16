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
                        CurrentImagePath_Change(mViewModel.CurrentImagePath);
                        break;
                }
            };

            // Initialize view to ViewModel's properties
            CurrentImagePath_Change(mViewModel.CurrentImagePath);
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

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            mViewModel.PrevButton_Click();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            mViewModel.NextButton_Click();
        }
    }
}
