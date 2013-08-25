using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Wide.Interfaces.Services
{
    public interface IStatusbarService
    {
        bool Animation(Image image);
        bool Clear();
        bool FreezeOutput();
        bool IsFrozen { get; }
        string Text { get; set; }
        Brush Foreground { get; set; }
        Brush Background { get; set; }
        bool? InsertMode { get; set; }
        int? LineNumber { get; set; }
        int? CharPosition { get; set; }
        int? ColPosition { get; set; }
        bool Progress(bool On, uint current, uint total);
    }
}