using System.Collections.Generic;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using Microsoft.Practices.Unity;

namespace Wide.Core.Services
{
    internal sealed class ContentHandlerRegistry : IContentHandlerRegistry
    {
        private List<IContentHandler> _contentHandlers;
        private IUnityContainer _container;

        public ContentHandlerRegistry(IUnityContainer container)
        {
            _contentHandlers = new List<IContentHandler>();
            _container = container;
        }

        public bool Register(IContentHandler handler)
        {
            _contentHandlers.Add(handler);
            return true;
        }

        public bool Unregister(IContentHandler handler)
        {
            return _contentHandlers.Remove(handler);
        }

        #region Get the view model
        public ContentViewModel GetViewModel(object info)
        {
            foreach (var opener in _contentHandlers)
            {
                if(opener.ValidateContentType(info))
                {
                    ContentViewModel vm = opener.OpenContent(info);
                    vm.Handler = opener;
                    return vm;
                }
            }
            return null;
        }

        public ContentViewModel GetViewModelFromContentId(string contentId)
        {
            foreach (var opener in _contentHandlers)
            {
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
    }
}
