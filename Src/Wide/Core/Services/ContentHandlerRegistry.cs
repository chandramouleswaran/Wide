// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Wide.Interfaces;
using Wide.Interfaces.Services;

namespace Wide.Core.Services
{
    internal sealed class ContentHandlerRegistry : IContentHandlerRegistry
    {
        private readonly List<IContentHandler> _contentHandlers;

        public ContentHandlerRegistry(IUnityContainer container)
        {
            _contentHandlers = new List<IContentHandler>();
        }

        #region IContentHandlerRegistry Members

        public bool Register(IContentHandler handler)
        {
            _contentHandlers.Add(handler);
            return true;
        }

        public bool Unregister(IContentHandler handler)
        {
            return _contentHandlers.Remove(handler);
        }

        #endregion

        #region Get the view model

        public ContentViewModel GetViewModel(object info)
        {
            foreach (IContentHandler opener in _contentHandlers)
            {
                if (opener.ValidateContentType(info))
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
            foreach (IContentHandler opener in _contentHandlers)
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