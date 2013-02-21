using System.ComponentModel;

namespace Wide.Interfaces
{
    /// <summary>
    /// The view model base class
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Should be called when a property value has changed
        /// </summary>
        /// <param name="propertyName">The property name</param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        
        /// <summary>
        /// Event handler that gets triggered when a property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}