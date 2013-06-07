#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Practices.Unity;
using Wide.Interfaces;
using Wide.Interfaces.Services;

namespace WideMD.Core
{
    internal class MDHandler : IContentHandler
    {
        /// <summary>
        /// The injected container
        /// </summary>
        private readonly IUnityContainer _container;

        /// <summary>
        /// The injected logger service
        /// </summary>
        private readonly ILoggerService _loggerService;

        public MDHandler(IUnityContainer container, ILoggerService loggerService)
        {
            _container = container;
            _loggerService = loggerService;
        }

        #region IContentHandler Members

        public ContentViewModel NewContent(object parameter)
        {
            throw new NotImplementedException();
        }

        public bool ValidateContentType(object info)
        {
            string location = info as string;
            string extension = "";

            if (location == null)
            {
                return false;
            }

            extension = Path.GetExtension(location);
            return File.Exists(location) && extension == ".md";
        }

        public ContentViewModel OpenContent(object info)
        {
            var location = info as string;
            if (location != null)
            {
                MDViewModel vm = _container.Resolve<MDViewModel>();
                var model = _container.Resolve<MDModel>();
                var view = _container.Resolve<MDView>();
                //TODO: This needs to change to MDView which hosts a pane to show browser

                //Model details
                model.PropertyChanged += vm.ModelOnPropertyChanged;
                model.SetLocation(info);
                try
                {
                    model.Document.Text = File.ReadAllText(location);
                    model.SetDirty(false);
                }
                catch (Exception exception)
                {
                    _loggerService.Log(exception.Message, LogCategory.Exception, LogPriority.High);
                    _loggerService.Log(exception.StackTrace, LogCategory.Exception, LogPriority.High);
                    return null;
                }

                //Clear the undo stack
                model.Document.UndoStack.ClearAll();

                //Set the model and view
                vm.SetModel(model);
                vm.SetView(view);
                vm.Title = Path.GetFileName(location);
                vm.View.DataContext = model;

                return vm;
            }
            return null;
        }

        public ContentViewModel OpenContentFromId(string contentId)
        {
            string[] split = Regex.Split(contentId, ":##:");
            if (split.Count() == 2)
            {
                string identifier = split[0];
                string path = split[1];
                if (identifier == "FILE" && File.Exists(path))
                {
                    return OpenContent(path);
                }
            }
            return null;
        }

        public bool SaveContent(ContentViewModel contentViewModel, bool saveAs = false)
        {
            throw new NotImplementedException();
        }

        public bool ValidateContentFromId(string contentId)
        {
            string[] split = Regex.Split(contentId, ":##:");
            if (split.Count() == 2)
            {
                string identifier = split[0];
                string path = split[1];
                if (identifier == "FILE" && ValidateContentType(path))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}