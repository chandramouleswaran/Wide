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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Wide.Interfaces.Services;

namespace Wide.Interfaces
{
    internal class WideStatusbar : ViewModelBase, IStatusbarService
    {
        private int? _lineNumber;
        private bool? _insertMode;
        private int? _colPosition;
        private int? _charPosition;
        private uint _pMax;
        private uint _pVal;
        private Brush _foreground;
        private Brush _background;
        private bool _showProgress;
        private Image _animImage;
        private bool _isFrozen;
        private string _text;

        public WideStatusbar()
        {
            Clear();
        }

        public bool Animation(Image image)
        {
            AnimationImage = image;
            return true;
        }

        public bool Clear()
        {
            Foreground = Brushes.White;
            Background = (SolidColorBrush) new BrushConverter().ConvertFrom("#FF007ACC");
            Text = "Ready";
            IsFrozen = false;
            ShowProgressBar = false;
            InsertMode = null;
            LineNumber = null;
            CharPosition = null;
            ColPosition = null;
            AnimationImage = null;
            return true;
        }

        public bool FreezeOutput()
        {
            return IsFrozen;
        }

        public bool IsFrozen
        {
            get { return _isFrozen; }
            set
            {
                _isFrozen = value;
                RaisePropertyChanged("IsFrozen");
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged("Text");
            }
        }

        public Brush Foreground
        {
            get { return _foreground; }
            set
            {
                _foreground = value;
                RaisePropertyChanged("Foreground");
            }
        }

        public Brush Background
        {
            get { return _background; }
            set
            {
                _background = value;
                RaisePropertyChanged("Background");
            }
        }

        public bool? InsertMode
        {
            get { return _insertMode; }
            set
            {
                _insertMode = value;
                RaisePropertyChanged("InsertMode");
            }
        }

        public int? LineNumber
        {
            get { return _lineNumber; }
            set
            {
                _lineNumber = value;
                RaisePropertyChanged("LineNumber");
            }
        }

        public int? CharPosition
        {
            get { return _charPosition; }
            set
            {
                _charPosition = value;
                RaisePropertyChanged("CharPosition");
            }
        }

        public int? ColPosition
        {
            get { return _colPosition; }
            set
            {
                _colPosition = value;
                RaisePropertyChanged("ColPosition");
            }
        }

        public bool Progress(bool On, uint current, uint total)
        {
            ShowProgressBar = On;
            ProgressMaximum = total;
            ProgressValue = current;
            return true;
        }

        public uint ProgressMaximum
        {
            get { return _pMax; }
            set
            {
                _pMax = value;
                RaisePropertyChanged("ProgressMaximum");
            }
        }

        public uint ProgressValue
        {
            get { return _pVal; }
            set
            {
                _pVal = value;
                RaisePropertyChanged("ProgressValue");
            }
        }


        public bool ShowProgressBar
        {
            get { return _showProgress; }
            set
            {
                _showProgress = value;
                RaisePropertyChanged("ShowProgressBar");
            }
        }

        public Image AnimationImage
        {
            get { return _animImage; }
            set
            {
                _animImage = value;
                RaisePropertyChanged("AnimationImage");
            }
        }
    }
}