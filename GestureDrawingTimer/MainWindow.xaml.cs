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
using GestureDrawingTimer.views;
using static GestureDrawingTimer.views.SetupUserControl;

namespace GestureDrawingTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ISetupUserControlListener
    {
        // Instance variables
        private MainWindowViewModel mViewModel;

        // Constructor
        public MainWindow()
        {
            InitializeComponent();

            mViewModel = new MainWindowViewModel();

            // Handle changes to viewModel's properties
            mViewModel.PropertyChanged += (sender, args) =>
            {
                switch (args.PropertyName)
                {
                    case "ActiveContentViewModel":
                        ActiveContentViewModel_Change(mViewModel.ActiveContentViewModel);
                        break;
                }
            };

            // Initialize to viewModel's properties
            ActiveContentViewModel_Change(mViewModel.ActiveContentViewModel);
        }


        // ISetupUserControlListener implementation
        public void StartSlideshow()
        {
            contentControl.Content = new SlideshowUserControl();
        }


        // Private methods

        private void ActiveContentViewModel_Change(object viewModel)
        {
            // Set contentControl's Content based on runtime type of parameter
            Type t = viewModel.GetType();
            if (t == typeof(SetupUserControlViewModel))
            {
                contentControl.Content = new SetupUserControl((SetupUserControlViewModel)viewModel);
            }
        }
    }
}
