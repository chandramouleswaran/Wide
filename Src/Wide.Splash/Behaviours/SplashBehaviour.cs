using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Wide.Splash.Behaviours
{
    public class SplashBehaviour
    {
        #region Dependency Properties
        public static readonly DependencyProperty EnabledProperty = DependencyProperty.RegisterAttached(
          "Enabled", typeof(bool), typeof(SplashBehaviour), new PropertyMetadata(OnEnabledChanged));

        public static bool GetEnabled(DependencyObject d_)
        {
            return (bool)d_.GetValue(EnabledProperty);
        }

        public static void SetEnabled(DependencyObject d_, bool value_)
        {
            d_.SetValue(EnabledProperty, value_);
        }
        #endregion

        #region Event Handlers
        private static void OnEnabledChanged(DependencyObject d_, DependencyPropertyChangedEventArgs arge_)
        {
            var splash = d_ as Window;
            if (splash != null && arge_.NewValue is bool && (bool)arge_.NewValue)
            {
                splash.Closed += (s_, e_) =>
                {
                    splash.DataContext = null;
                    splash.Dispatcher.InvokeShutdown();
                };
                splash.MouseDoubleClick += (s_, e_) => splash.Close();
                splash.MouseLeftButtonDown += (s_, e_) => splash.DragMove();
            }
        }
        #endregion
    }
}
