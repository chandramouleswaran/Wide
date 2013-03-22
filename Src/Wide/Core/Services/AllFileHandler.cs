// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Practices.Unity;
using Wide.Core.TextDocument;
using Wide.Interfaces;
using Wide.Interfaces.Services;

namespace Wide.Core.Services
{
    internal class AllFileHandler : IContentHandler
    {
        private readonly IUnityContainer _container;
        private readonly ILoggerService _loggerService;

        public AllFileHandler(IUnityContainer container, ILoggerService loggerService, IOpenFileService openFileService)
        {
            _container = container;
            _loggerService = loggerService;
        }

        #region IContentHandler Members

        public bool ValidateContentType(object info)
        {
            var location = info as string;
            if (location != null)
            {
                return File.Exists(location);
            }
            return false;
        }


        public bool ValidateContentFromId(string contentId)
        {
            string[] split = Regex.Split(contentId, ":##:");
            if (split.Count() == 2)
            {
                string identifier = split[0];
                string path = split[1];
                if (identifier == "FILE" && File.Exists(path))
                {
                    return true;
                }
            }
            return false;
        }

        public ContentViewModel OpenContent(object info)
        {
            var location = info as string;
            if (location != null)
            {
                var vm = _container.Resolve<TextViewModel>();
                var model = _container.Resolve<TextModel>();
                var view = _container.Resolve<TextView>();

                //Model details
                model.PropertyChanged += vm.ModelOnPropertyChanged;
                model.Location = info;
                try
                {
                    model.Document.Text = File.ReadAllText(location);
                    model.IsDirty = false;
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
                vm.Model = model;
                vm.View = view;
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
            var textViewModel = contentViewModel as TextViewModel;

            if (textViewModel == null)
            {
                _loggerService.Log("ContentViewModel needs to be a TextViewModel to save details", LogCategory.Exception,
                                   LogPriority.High);
                throw new ArgumentException("ContentViewModel needs to be a TextViewModel to save details");
            }

            var textModel = textViewModel.Model as TextModel;

            if (textModel == null)
            {
                _loggerService.Log("TextViewModel does not have a TextModel which should have the text",
                                   LogCategory.Exception, LogPriority.High);
                throw new ArgumentException("TextViewModel does not have a TextModel which should have the text");
            }

            if (saveAs)
            {
                //TODO: Save as...?
            }
            else
            {
                //Regular save
                var location = textModel.Location as string;
                try
                {
                    File.WriteAllText(location, textModel.Document.Text);
                    textModel.IsDirty = false;
                    return true;
                }
                catch (Exception exception)
                {
                    _loggerService.Log(exception.Message, LogCategory.Exception, LogPriority.High);
                    _loggerService.Log(exception.StackTrace, LogCategory.Exception, LogPriority.High);
                    return false;
                }
            }

            return false;
        }

        #endregion
    }
}