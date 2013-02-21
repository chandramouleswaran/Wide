using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Practices.Unity;
using Wide.Interfaces;

namespace WideTestApp
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
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/WideTestApp;component/Icon.png"));
                return imageSource;
            }
        }
    }
}
