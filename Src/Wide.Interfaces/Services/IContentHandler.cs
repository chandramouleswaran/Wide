namespace Wide.Interfaces.Services
{
    public interface IContentHandler
    {
        bool ValidateContentType(object info);
        ContentViewModel OpenContent(object info);
        ContentViewModel OpenContentFromId(string contentId);
        bool SaveContent(ContentViewModel contentViewModel, bool saveAs = false);
        bool ValidateContentFromId(string contentId);
    }
}