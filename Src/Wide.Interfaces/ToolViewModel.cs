using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Wide.Interfaces
{
    /// <summary>
    /// The abstract class which has to be inherited if you want to create a tool
    /// </summary>
    public abstract class ToolViewModel : ViewModelBase
    {
        #region Members
        private bool _isVisible = true;

        protected string _title = null;
        protected string _contentId = null;
        protected bool _isSelected = false;
        protected bool _isActive = false;
        #endregion

        #region Property
        /// <summary>
        /// The name of the tool
        /// </summary>
        public string Name
        {
            get;
            protected set;
        }

        /// <summary>
        /// The visibility of the tool
        /// </summary>
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    RaisePropertyChanged("IsVisible");
                }
            }
        }

        /// <summary>
        /// The content model
        /// </summary>
        public virtual INotifyPropertyChanged Model { get; set; }

        /// <summary>
        /// The content view
        /// </summary>
        public virtual IContentView View { get; set; }

        /// <summary>
        /// The title of the document
        /// </summary>
        public virtual string Title
        {
            get { return _title; }
            protected set
            {
                if (_title != value)
                {
                    _title = value;
                    RaisePropertyChanged("Title");
                }
            }
        }

        /// <summary>
        /// The image souce that can be used as an icon in the tab
        /// </summary>
        public virtual ImageSource IconSource
        {
            get;
            protected set;
        }

        /// <summary>
        /// The content ID - unique value for each document
        /// </summary>
        public virtual string ContentId
        {
            get { return _contentId; }
            protected set
            {
                if (_contentId != value)
                {
                    _contentId = value;
                    RaisePropertyChanged("ContentId");
                }
            }
        }

        /// <summary>
        /// Is the document selected
        /// </summary>
        public virtual bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    RaisePropertyChanged("IsSelected");
                }
            }
        }

        /// <summary>
        /// Is the document active
        /// </summary>
        public virtual bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    RaisePropertyChanged("IsActive");
                }
            }
        }

        #endregion
    }
}
