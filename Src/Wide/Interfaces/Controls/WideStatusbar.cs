#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System.Windows.Controls;
using System.Windows.Media;
using Wide.Interfaces.Services;

namespace Wide.Interfaces
{
    /// <summary>
    /// Class Wide Status bar
    /// </summary>
    internal class WideStatusbar : ViewModelBase, IStatusbarService
    {
        #region Fields
        /// <summary>
        /// The line number
        /// </summary>
        private int? _lineNumber;
        /// <summary>
        /// The insert mode
        /// </summary>
        private bool? _insertMode;
        /// <summary>
        /// The col position
        /// </summary>
        private int? _colPosition;
        /// <summary>
        /// The char position
        /// </summary>
        private int? _charPosition;
        /// <summary>
        /// The p max
        /// </summary>
        private uint _pMax;
        /// <summary>
        /// The _p val
        /// </summary>
        private uint _pVal;
        /// <summary>
        /// The _foreground
        /// </summary>
        private Brush _foreground;
        /// <summary>
        /// The _background
        /// </summary>
        private Brush _background;
        /// <summary>
        /// The _show progress
        /// </summary>
        private bool _showProgress;
        /// <summary>
        /// The _anim image
        /// </summary>
        private Image _animImage;
        /// <summary>
        /// The _is frozen
        /// </summary>
        private bool _isFrozen;
        /// <summary>
        /// The _text
        /// </summary>
        private string _text;
        #endregion

        #region CTOR
        /// <summary>
        /// Initializes a new instance of the <see cref="WideStatusbar"/> class.
        /// </summary>
        public WideStatusbar()
        {
            Clear();
        }
        #endregion

        #region IStatusbarService members
        /// <summary>
        /// Animations the specified image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool Animation(Image image)
        {
            AnimationImage = image;
            return true;
        }

        /// <summary>
        /// Clears this status bar.
        /// </summary>
        /// <returns><c>true</c> if successfully, <c>false</c> otherwise</returns>
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

        /// <summary>
        /// Freezes the output.
        /// </summary>
        /// <returns><c>true</c> if frozen, <c>false</c> otherwise</returns>
        public bool FreezeOutput()
        {
            return IsFrozen;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is frozen.
        /// </summary>
        /// <value><c>true</c> if this instance is frozen; otherwise, <c>false</c>.</value>
        public bool IsFrozen
        {
            get { return _isFrozen; }
            set
            {
                _isFrozen = value;
                RaisePropertyChanged("IsFrozen");
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged("Text");
            }
        }

        /// <summary>
        /// Gets or sets the foreground.
        /// </summary>
        /// <value>The foreground.</value>
        public Brush Foreground
        {
            get { return _foreground; }
            set
            {
                _foreground = value;
                RaisePropertyChanged("Foreground");
            }
        }

        /// <summary>
        /// Gets or sets the background.
        /// </summary>
        /// <value>The background.</value>
        public Brush Background
        {
            get { return _background; }
            set
            {
                _background = value;
                RaisePropertyChanged("Background");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [insert mode].
        /// </summary>
        /// <value><c>null</c> if [insert mode] contains no value, <c>true</c> if [insert mode]; otherwise, <c>false</c>.</value>
        public bool? InsertMode
        {
            get { return _insertMode; }
            set
            {
                _insertMode = value;
                RaisePropertyChanged("InsertMode");
            }
        }

        /// <summary>
        /// Gets or sets the line number.
        /// </summary>
        /// <value>The line number.</value>
        public int? LineNumber
        {
            get { return _lineNumber; }
            set
            {
                _lineNumber = value;
                RaisePropertyChanged("LineNumber");
            }
        }

        /// <summary>
        /// Gets or sets the char position.
        /// </summary>
        /// <value>The char position.</value>
        public int? CharPosition
        {
            get { return _charPosition; }
            set
            {
                _charPosition = value;
                RaisePropertyChanged("CharPosition");
            }
        }

        /// <summary>
        /// Gets or sets the col position.
        /// </summary>
        /// <value>The col position.</value>
        public int? ColPosition
        {
            get { return _colPosition; }
            set
            {
                _colPosition = value;
                RaisePropertyChanged("ColPosition");
            }
        }

        /// <summary>
        /// Progresses the specified on.
        /// </summary>
        /// <param name="On">if set to <c>true</c> [on].</param>
        /// <param name="current">The current.</param>
        /// <param name="total">The total.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool Progress(bool On, uint current, uint total)
        {
            ShowProgressBar = On;
            ProgressMaximum = total;
            ProgressValue = current;
            return true;
        }

        /// <summary>
        /// Gets or sets the progress maximum.
        /// </summary>
        /// <value>The progress maximum.</value>
        public uint ProgressMaximum
        {
            get { return _pMax; }
            set
            {
                _pMax = value;
                RaisePropertyChanged("ProgressMaximum");
            }
        }

        /// <summary>
        /// Gets or sets the progress value.
        /// </summary>
        /// <value>The progress value.</value>
        public uint ProgressValue
        {
            get { return _pVal; }
            set
            {
                _pVal = value;
                RaisePropertyChanged("ProgressValue");
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether [show progress bar].
        /// </summary>
        /// <value><c>true</c> if [show progress bar]; otherwise, <c>false</c>.</value>
        public bool ShowProgressBar
        {
            get { return _showProgress; }
            set
            {
                _showProgress = value;
                RaisePropertyChanged("ShowProgressBar");
            }
        }

        /// <summary>
        /// Gets or sets the animation image.
        /// </summary>
        /// <value>The animation image.</value>
        public Image AnimationImage
        {
            get { return _animImage; }
            set
            {
                _animImage = value;
                RaisePropertyChanged("AnimationImage");
            }
        }
        #endregion
    }
}