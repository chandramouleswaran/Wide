using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wide.Splash;

namespace VS2010TestApp
{
    /// <summary>
    /// Interaction logic for AppSplash.xaml
    /// </summary>
    public partial class AppSplash : Window, ISplashView
    {
        public AppSplash(SplashViewModel model)
        {
            InitializeComponent();
            this.DataContext = model;
        }
    }
}
