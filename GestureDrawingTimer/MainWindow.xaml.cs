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

        // UI event handlers
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            mViewModel.KeyDown_Action(e);
        }


        // Private methods

        private void ActiveContentViewModel_Change(object viewModel)
        {
            // Set contentControl's Content based on runtime type of parameter
            Type t = viewModel.GetType();
            if (t == typeof(SetupViewModel))
            {
                contentControl.Content = new SetupUserControl((SetupViewModel)viewModel);
            }
            else if (t == typeof(SessionViewModel))
            {
                contentControl.Content = new SessionUserControl((SessionViewModel)viewModel);
            }
        }
    }
}
