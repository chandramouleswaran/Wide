using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wide.Interfaces.Events;
using Microsoft.Practices.Prism.Events;

namespace Wide.Splash
{
    public class SplashViewModel : INotifyPropertyChanged
    {
        #region Declarations
        private string _status;
        #endregion

        #region ctor
        public SplashViewModel(IEventAggregator eventAggregator_)
        {
            eventAggregator_.GetEvent<SplashMessageUpdateEvent>().Subscribe(e_ => UpdateMessage(e_.Message));
        }
        #endregion

        #region Public Properties
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }
        #endregion

        #region Private Methods
        private void UpdateMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            Status += string.Concat(Environment.NewLine, message, "...");
        }
        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName_)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName_));
            }
        }
        #endregion
    }
}
