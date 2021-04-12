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
        public MainWindow()
        {
            InitializeComponent();

            // Show the UserControl for the setup screen
            SetupUserControl setupUC = new SetupUserControl();
            setupUC.SetListener(this);
            contentControl.Content = setupUC;
        }


        // ISetupUserControlListener implementation
        public void StartSlideshow()
        {
            contentControl.Content = new SlideshowUserControl();
        }
    }
}
