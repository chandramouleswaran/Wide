using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Practices.Unity;
using Wide.Interfaces;

namespace VS2012TestApp
{
    public class IconWorkspace : AbstractWorkspace
    {
        public IconWorkspace(IUnityContainer container) : base(container)
        {
        }

        public override ImageSource Icon 
        { 
            get
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/VS2012TestApp;component/Icon.png"));
                return imageSource;
            }
        }
    }
}
