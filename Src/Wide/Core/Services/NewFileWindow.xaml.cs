#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System.Windows.Input;
using MahApps.Metro.Controls;
using Wide.Core.Attributes;
using System.Windows.Controls;
using System.Windows;

namespace Wide.Core.Services
{
    /// <summary>
    /// Interaction logic for NewFileWindow.xaml
    /// </summary>
    internal partial class NewFileWindow : Window
    {
        public NewFileWindow()
        {
            InitializeComponent();
        }

        private void listBoxItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.NewContent = (sender as ListBoxItem).DataContext as NewContentAttribute;
            this.DialogResult = true;
        }

        public NewContentAttribute NewContent { get; private set; }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.NewContent = this.listView.SelectedItem as NewContentAttribute;
            this.DialogResult = true;
        }
    }
}
