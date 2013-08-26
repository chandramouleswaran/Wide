#region License
// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System.ComponentModel;
using System.Configuration;
using System.Windows.Media;
using Wide.Interfaces.Settings;

namespace WideMD.Core.Settings
{
    internal class EditorOptions : AbstractSettings
    {
        private static EditorOptions settings = new EditorOptions();

        private EditorOptions()
        {
        }

        [Browsable(false)]
        public static EditorOptions Default
        {
            get 
            { return settings; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("White")]
        [Category("Colors")]
        [DisplayName("Background Color")]
        [Description("The background color of the text editor")]
        public SolidColorBrush BackgroundColor
        {
            get { return (SolidColorBrush)this["BackgroundColor"]; }
            set { this["BackgroundColor"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Black")]
        [Category("Colors")]
        [DisplayName("Foreground Color")]
        [Description("The foreground color of the text editor")]
        public SolidColorBrush ForegroundColor
        {
            get { return (SolidColorBrush)this["ForegroundColor"]; }
            set { this["ForegroundColor"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        [Category("Format")]
        [DisplayName("Display line numbers?")]
        [Description("Set to Yes to show line numbers on the text editor")]
        public bool ShowLineNumbers
        {
            get { return (bool)this["ShowLineNumbers"]; }
            set { this["ShowLineNumbers"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        [Category("Format")]
        [DisplayName("Word wrap?")]
        [Description("Set to Yes to wrap words in a line on the text editor")]
        public bool WordWrap
        {
            get { return (bool)this["WordWrap"]; }
            set { this["WordWrap"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Consolas")]
        [Category("Format")]
        [DisplayName("Font")]
        [Description("Select the font to use in the text editor")]
        public FontFamily FontFamily
        {
            get { return (FontFamily)this["FontFamily"]; }
            set { this["FontFamily"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("14")]
        [Category("Format")]
        [DisplayName("Size")]
        [Description("Select the size to use for the font in the text editor")]
        public int FontSize
        {
            get { return (int)this["FontSize"]; }
            set { this["FontSize"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        [Browsable(false)]
        public bool LivePreview
        {
            get { return (bool)this["LivePreview"]; }
            set { this["LivePreview"] = value; }
        }
    }
}
