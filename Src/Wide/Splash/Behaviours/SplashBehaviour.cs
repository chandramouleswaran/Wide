#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System.Windows;

namespace Wide.Splash.Behaviours
{
    /// <summary>
    /// Class SplashBehaviour - Add as an attached property on your splash view 
    /// </summary>
    public sealed class SplashBehaviour
    {
        #region Dependency Properties

        public static readonly DependencyProperty EnabledProperty = DependencyProperty.RegisterAttached(
            "Enabled", typeof (bool), typeof (SplashBehaviour), new PropertyMetadata(OnEnabledChanged));

        public static bool GetEnabled(DependencyObject d)
        {
            return (bool) d.GetValue(EnabledProperty);
        }

        public static void SetEnabled(DependencyObject d, bool value)
        {
            d.SetValue(EnabledProperty, value);
        }

        #endregion

        #region Event Handlers

        private static void OnEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var splash = d as Window;
            if (splash != null && args.NewValue is bool && (bool) args.NewValue)
            {
                splash.Closed += (s, e) =>
                                     {
                                         splash.DataContext = null;
                                         splash.Dispatcher.InvokeShutdown();
                                     };
                splash.MouseDoubleClick += (s, e) => splash.Close();
                splash.MouseLeftButtonDown += (s, e) => splash.DragMove();
            }
        }

        #endregion
    }
}