// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using Microsoft.Win32;
using Wide.Interfaces;
using Wide.Interfaces.Events;
using Wide.Interfaces.Services;

namespace Wide.Core.Services
{
    /// <summary>
    /// The open file service
    /// </summary>
    internal sealed class OpenFileService : IOpenFileService
    {
        /// <summary>
        /// The injected container
        /// </summary>
        private readonly IUnityContainer _container;
        /// <summary>
        /// The injected event aggregator
        /// </summary>
        private readonly IEventAggregator _eventAggregator;
        /// <summary>
        /// The injected logger
        /// </summary>
        private readonly ILoggerService _logger;

        /// <summary>
        /// Constructor for Open file service
        /// </summary>
        /// <param name="container">The injected container</param>
        /// <param name="eventAggregator">The injected event aggregator</param>
        /// <param name="logger">The injected logger</param>
        public OpenFileService(IUnityContainer container, IEventAggregator eventAggregator, ILoggerService logger)
        {
            _container = container;
            _eventAggregator = eventAggregator;
            _logger = logger;
        }

        #region IOpenFileService Members
        /// <summary>
        /// Opens the object - if object is null, show a open file dialog to select a file to open
        /// </summary>
        /// <param name="location">The optional object to open</param>
        /// <returns>A document which was added to the workspace as a content view model</returns>
        public ContentViewModel Open(object location = null)
        {
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();

            var dialog = new OpenFileDialog();
            bool? result;

            if (location == null)
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
                var handler = _container.Resolve<IContentHandlerRegistry>();

                //Let the handler figure out which view model to return
                ContentViewModel openValue = handler.GetViewModel(location);

                if (openValue != null)
                {
                    //Check if the document is already open
                    foreach (ContentViewModel contentViewModel in workspace.Documents)
                    {
                        if (contentViewModel.Model.Location.Equals(openValue.Model.Location))
                        {
                            _logger.Log(
                                "Document " + contentViewModel.Model.Location + "already open - making it active",
                                LogCategory.Info, LogPriority.Low);
                            workspace.ActiveDocument = contentViewModel;
                            return contentViewModel;
                        }
                    }

                    _logger.Log("Opening file" + location + " !!", LogCategory.Info, LogPriority.Low);

                    // Publish the event to the Application - subscribers can use this object
                    _eventAggregator.GetEvent<OpenContentEvent>().Publish(openValue);

                    //Add it to the actual workspace
                    workspace.Documents.Add(openValue);

                    //Make it the active document
                    workspace.ActiveDocument = openValue;
                }
                else
                {
                    _logger.Log("Unable to find a IContentHandler to open " + location, LogCategory.Error,
                                LogPriority.High);
                }
            }
            else
            {
                _logger.Log("Canceled out of open file dialog", LogCategory.Info, LogPriority.Low);
            }
            return null;
        }

        /// <summary>
        /// Opens the contentID
        /// </summary>
        /// <param name="contentID">The contentID to open</param>
        /// <returns>A document which was added to the workspace as a content view model</returns>
        public ContentViewModel OpenFromID(string contentID)
        {
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            var handler = _container.Resolve<IContentHandlerRegistry>();

            //Let the handler figure out which view model to return
            ContentViewModel openValue = handler.GetViewModelFromContentId(contentID);

            if (openValue != null)
            {
                //Check if the document is already open
                foreach (ContentViewModel contentViewModel in workspace.Documents)
                {
                    if (contentViewModel.Model.Location.Equals(openValue.Model.Location))
                    {
                        _logger.Log("Document " + contentViewModel.Model.Location + "already open.", LogCategory.Info,
                                    LogPriority.Low);
                        return contentViewModel;
                    }
                }

                _logger.Log("Opening content with " + contentID + " !!", LogCategory.Info, LogPriority.Low);

                // Publish the event to the Application - subscribers can use this object
                _eventAggregator.GetEvent<OpenContentEvent>().Publish(openValue);

                workspace.Documents.Add(openValue);

                return openValue;
            }

            _logger.Log("Unable to find a IContentHandler to open content with ID = " + contentID, LogCategory.Error, LogPriority.High);
            return null;
        }

        #endregion
    }
}