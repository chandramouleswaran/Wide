namespace Wide.Interfaces.Services
{
    public interface IContentHandlerRegistry
    {
        bool Register(IContentHandler handler);
        bool Unregister(IContentHandler handler);
        ContentViewModel GetViewModel(object info);
        ContentViewModel GetViewModelFromContentId(string contentId);
    }
}