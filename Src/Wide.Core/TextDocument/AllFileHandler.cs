using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using System.Text.RegularExpressions;

namespace Wide.Core
{
    internal class AllFileHandler : IContentHandler
    {
        private IUnityContainer _container;
        private IOpenFileService _fileService;
        private ILoggerService _loggerService;

        public AllFileHandler(IUnityContainer container, ILoggerService loggerService, IOpenFileService openFileService)
        {
            this._container = container;
            _loggerService = loggerService;
            _fileService = openFileService;
        }

        public bool ValidateContentType(object info)
        {
            string location = info as string;
            if(location != null)
            {
                return File.Exists(location);
            }
            return false;
        }


        public bool ValidateContentFromId(string contentId)
        {
            string[] split = Regex.Split(contentId,":##:");
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
            string location = info as string;
            if (location != null)
            {
                TextViewModel vm = _container.Resolve<TextViewModel>();
                TextModel model = _container.Resolve<TextModel>();
                TextView view = _container.Resolve<TextView>();

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
                    return this.OpenContent(path);
                }
            }
            return null;
        }

        public bool SaveContent(ContentViewModel contentViewModel, bool saveAs = false)
        {
            TextViewModel textViewModel = contentViewModel as TextViewModel;

            if(textViewModel == null)
            {
                _loggerService.Log("ContentViewModel needs to be a TextViewModel to save details", LogCategory.Exception, LogPriority.High);
                throw new ArgumentException("ContentViewModel needs to be a TextViewModel to save details");
            }

            TextModel textModel = textViewModel.Model as TextModel;

            if(textModel == null)
            {
                _loggerService.Log("TextViewModel does not have a TextModel which should have the text", LogCategory.Exception, LogPriority.High);
                throw new ArgumentException("TextViewModel does not have a TextModel which should have the text");
            }

            if(saveAs)
            {
                //TODO: Save as...?
            }
            else
            {
                //Regular save
                string location = textModel.Location as string;
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
    }
}
