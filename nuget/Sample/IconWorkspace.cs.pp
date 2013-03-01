using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Practices.Unity;
using Wide.Interfaces;

namespace $rootnamespace$
{
    public class IconWorkspace : AbstractWorkspace
    {
        public IconWorkspace(IUnityContainer container)
            : base(container)
        {
        }

        public override ImageSource Icon
        {
            get
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/$rootnamespace$;component/Icon.png"));
                return imageSource;
            }
        }
    }
}