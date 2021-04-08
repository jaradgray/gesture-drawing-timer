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
                        folderTextBlock.Text = $"Folder: {mViewModel.SelectedFolderPath}";
                        break;
                }
            };

            // Initialize views to ViewModel's state
            folderTextBlock.Text = $"Folder: {mViewModel.SelectedFolderPath}";
        }


        // Private methods

        private void SelectFolderButton_Click(object sender, RoutedEventArgs e)
        {
            mViewModel.SelectFolderButton_Click();
        }
    }
}
