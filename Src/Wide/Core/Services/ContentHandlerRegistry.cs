#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using Wide.Core.Attributes;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using System.Windows.Input;

namespace Wide.Core.Services
{
    /// <summary>
    /// The content handler registry which manages different content handlers
    /// </summary>
    internal sealed class ContentHandlerRegistry : IContentHandlerRegistry
    {
        /// <summary>
        /// List of content handlers
        /// </summary>
        private readonly List<IContentHandler> _contentHandlers;

        /// <summary>
        /// The container
        /// </summary>
        private readonly IUnityContainer _container;

        /// <summary>
        /// The new document command
        /// </summary>
        public ICommand NewCommand { get; protected set; }

        /// <summary>
        /// Constructor of content handler registry
        /// </summary>
        /// <param name="container">The injected container of the application</param>
        public ContentHandlerRegistry(IUnityContainer container)
        {
            _contentHandlers = new List<IContentHandler>();
            _container = container;
             this.NewCommand = new DelegateCommand(NewDocument, CanExecuteNewCommand);
        }

        /// <summary>
        /// List of content handlers
        /// </summary>
        public List<IContentHandler> ContentHandlers
        {
            get { return _contentHandlers; }
        }

        #region IContentHandlerRegistry Members

        /// <summary>
        /// Register a content handler with the registry
        /// </summary>
        /// <param name="handler">The content handler</param>
        /// <returns>true, if successful - false, otherwise</returns>
        public bool Register(IContentHandler handler)
        {
            ContentHandlers.Add(handler);
            return true;
        }

        /// <summary>
        /// Unregisters a content handler
        /// </summary>
        /// <param name="handler">The handler to remove</param>
        /// <returns></returns>
        public bool Unregister(IContentHandler handler)
        {
            return ContentHandlers.Remove(handler);
        }

        #endregion

        #region Get the view model

        /// <summary>
        /// Returns a content view model for the specified object which needs to be displayed as a document
        /// The object could be anything - based on the handlers, a content view model is returned
        /// </summary>
        /// <param name="info">The object which needs to be displayed as a document in Wide</param>
        /// <returns>The content view model for the given info</returns>
        public ContentViewModel GetViewModel(object info)
        {
            for (int i = ContentHandlers.Count - 1; i >= 0; i--)
            {
                var opener = ContentHandlers[i];
                if (opener.ValidateContentType(info))
                {
                    ContentViewModel vm = opener.OpenContent(info);
                    vm.Handler = opener;
                    return vm;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns a content view model for the specified contentID which needs to be displayed as a document
        /// The contentID is the ID used in AvalonDock
        /// </summary>
        /// <param name="contentId">The contentID which needs to be displayed as a document in Wide</param>
        /// <returns>The content view model for the given info</returns>
        public ContentViewModel GetViewModelFromContentId(string contentId)
        {
            for (int i = ContentHandlers.Count - 1; i >= 0; i--)
            {
                var opener = ContentHandlers[i];
                if (opener.ValidateContentFromId(contentId))
                {
                    ContentViewModel vm = opener.OpenContentFromId(contentId);
                    vm.Handler = opener;
                    return vm;
                }
            }
            return null;
        }

        #endregion

        #region New Command
        private bool CanExecuteNewCommand()
        {
            return true;
        }

        private void NewDocument()
        {
            var contentHandler = _container.Resolve<IContentHandlerRegistry>() as ContentHandlerRegistry;
            var workspace = _container.Resolve<AbstractWorkspace>();
            Dictionary<NewContentAttribute,IContentHandler> _dictionary = new Dictionary<NewContentAttribute, IContentHandler>();
            List<NewContentAttribute> attributes = new List<NewContentAttribute>();

            if (contentHandler != null)
            {
                foreach (IContentHandler handler in _contentHandlers)
                {
                    NewContentAttribute[] handlerAttributes = (NewContentAttribute[]) (handler.GetType()).GetCustomAttributes(typeof (NewContentAttribute), true);
                    attributes.AddRange(handlerAttributes);
                    foreach (NewContentAttribute newContentAttribute in handlerAttributes)
                    {
                        _dictionary.Add(newContentAttribute, handler);
                    }
                }
                
                attributes.Sort((attribute, contentAttribute) => attribute.Priority - contentAttribute.Priority);

                if (attributes.Count == 1)
                {
                    IContentHandler handler = _dictionary[attributes[0]];
                    var openValue = handler.NewContent(attributes[0]);
                    workspace.Documents.Add(openValue);
                    workspace.ActiveDocument = openValue;
                }
                else
                {
                    //TODO: This is where we need to show a new pop-up similar to VS and get the input from user
                    
                }
            }
        }
        #endregion
    }
}