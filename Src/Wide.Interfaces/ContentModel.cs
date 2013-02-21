using System.ComponentModel;

namespace Wide.Interfaces
{
    public abstract class ContentModel
    {
        protected bool _isDirty;

        /// <summary>
        /// The document location - could be a file location/server object etc.
        /// </summary>
        public virtual object Location { get; set; }

        /// <summary>
        /// Is the document dirty - does it need to be saved?
        /// </summary>
        public virtual bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                _isDirty = value;
                RaisePropertyChanged("IsDirty");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}