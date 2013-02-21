using Wide.Interfaces;
using Wide.Interfaces.Events;
using Wide.Interfaces.Services;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using Microsoft.Win32;

namespace Wide.Core.Services
{
    internal sealed class OpenFileService : IOpenFileService
    {
        private IUnityContainer _container;
        private IEventAggregator _eventAggregator;
        private ILoggerService _logger;

        public OpenFileService(IUnityContainer container, IEventAggregator eventAggregator,ILoggerService logger)
        {
            _container = container;
            _eventAggregator = eventAggregator;
            _logger = logger;
        }

        public ContentViewModel Open(object location = null)
        {
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();

            OpenFileDialog dialog = new OpenFileDialog();
            bool? result = false;

            if(location == null)
            {
                result = dialog.ShowDialog();
                location = dialog.FileName;
            }
            else
            {
                result = true;
            }

            if (result == true && !string.IsNullOrWhiteSpace(location.ToString()))
            {
                IContentHandlerRegistry handler = _container.Resolve<IContentHandlerRegistry>();
                
                //Let the handler figure out which view model to return
                ContentViewModel openValue = handler.GetViewModel(location);

                if (openValue != null)
                {
                    //Check if the document is already open
                    foreach (ContentViewModel contentViewModel in workspace.Documents)
                    {
                        if (contentViewModel.Model.Location.Equals(openValue.Model.Location))
                        {
                            _logger.Log("Document " + contentViewModel.Model.Location.ToString() + "already open - making it active", LogCategory.Info, LogPriority.Low);
                            workspace.ActiveDocument = contentViewModel;
                            return contentViewModel;
                        }
                    }

                    _logger.Log("Opening file" + location.ToString() + " !!", LogCategory.Info, LogPriority.Low);

                    // Publish the event to the Application - subscribers can use this object
                    _eventAggregator.GetEvent<OpenContentEvent>().Publish(openValue);

                    //Add it to the actual workspace
                    workspace.Documents.Add(openValue);

                    //Make it the active document
                    workspace.ActiveDocument = openValue;
                }
                else
                {
                    _logger.Log("Unable to find a IContentHandler to open " + location.ToString(), LogCategory.Error, LogPriority.High);
                }
            }
            else
            {
                _logger.Log("Canceled out of open file dialog", LogCategory.Info, LogPriority.Low);
            }
            return null;
        }

        public ContentViewModel OpenFromID(string contentID)
        {
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            IContentHandlerRegistry handler = _container.Resolve<IContentHandlerRegistry>();
                
            //Let the handler figure out which view model to return
            ContentViewModel openValue = handler.GetViewModelFromContentId(contentID);

            if (openValue != null)
            {
                //Check if the document is already open
                foreach (ContentViewModel contentViewModel in workspace.Documents)
                {
                    if (contentViewModel.Model.Location.Equals(openValue.Model.Location))
                    {
                        _logger.Log("Document " + contentViewModel.Model.Location.ToString() + "already open.", LogCategory.Info, LogPriority.Low);
                        return contentViewModel;
                    }
                }

                _logger.Log("Opening content with " + contentID + " !!", LogCategory.Info, LogPriority.Low);

                // Publish the event to the Application - subscribers can use this object
                _eventAggregator.GetEvent<OpenContentEvent>().Publish(openValue);

                workspace.Documents.Add(openValue);
                
                return openValue;
            }
            else
            {
                _logger.Log("Unable to find a IContentHandler to open content with ID = " + contentID, LogCategory.Error, LogPriority.High);
            }
            return null;
        }
    }
}
