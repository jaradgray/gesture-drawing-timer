using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GestureDrawingTimer
{
    public class BaseINPC : INotifyPropertyChanged
    {
        // Implement notifying PropertyChanged functionality
        public event PropertyChangedEventHandler PropertyChanged; // required by INotifyPropertyChanged interface
        // The method we'll call when we want to raise the PropertyChanged event
        //  Note: the CallerMemberName attribute on the optional parameter will automatically
        //  set propertyName to the name of the caller's property
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
