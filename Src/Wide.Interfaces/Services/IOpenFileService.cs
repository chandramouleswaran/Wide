namespace Wide.Interfaces.Services
{
    public interface IOpenFileService
    {
        ContentViewModel Open(object location = null);
        ContentViewModel OpenFromID(string contentID);
    }
}
