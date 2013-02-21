using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wide.Interfaces;
using Wide.Interfaces.Services;

namespace Wide.Tools.Logger
{
    class LoggerModel : INotifyPropertyChanged
    {
        private string _text;

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddLog(ILoggerService logger)
        {
            _text += logger.Message + "\n";
            RaisePropertyChanged("Text");
        }

        public string Text
        {
            get { return _text; }
        }

        /// <summary>
        /// Should be called when a property value has changed
        /// </summary>
        /// <param name="propertyName">The property name</param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
