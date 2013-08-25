using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Wide.Interfaces.Services;

namespace Wide.Interfaces
{
    internal class AbstractStatusbar : IStatusbarService
    {
        public bool Animation(Image image)
        {
            throw new NotImplementedException();
        }

        public bool Clear()
        {
            throw new NotImplementedException();
        }

        public bool FreezeOutput()
        {
            throw new NotImplementedException();
        }

        public bool IsFrozen { get; private set; }
        public string Text { get; set; }
        public Brush Foreground { get; set; }
        public Brush Background { get; set; }
        public bool? InsertMode { get; set; }
        public int? LineNumber { get; set; }
        public int? CharPosition { get; set; }
        public int? ColPosition { get; set; }
        public bool Progress(bool On, uint current, uint total)
        {
            throw new NotImplementedException();
        }

        public bool Analysis(Image image, string text, ICommand command)
        {
            throw new NotImplementedException();
        }
    }
}