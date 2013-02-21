using AvalonDock.Layout.Serialization;
using MahApps.Metro.Controls;
using Microsoft.Practices.Unity;
using Wide.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AvalonDock;
using AvalonDock.Layout;
using System.Collections.ObjectModel;
using Wide.Interfaces.Services;

namespace Wide.Shell
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class ShellView : Window, IShell
    {
        private IUnityContainer _container;

        public ShellView(IUnityContainer container)
        {
            InitializeComponent();
            this._container = container;
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IWorkspace workspace = this.DataContext as IWorkspace;
            if(!workspace.Closing())
            {
                e.Cancel = true;
                return;
            }
            SaveLayout();
        }


        public void LoadLayout()
        {
            var layoutSerializer = new XmlLayoutSerializer(dockManager);
            layoutSerializer.LayoutSerializationCallback += (s, e) =>
            {
                LayoutAnchorable anchorable = e.Model as LayoutAnchorable;
                LayoutDocument document = e.Model as LayoutDocument;
                IWorkspace workspace = _container.Resolve<AbstractWorkspace>();

                if (anchorable != null)
                {
                    ToolViewModel model = workspace.Tools.First(f => f.ContentId == e.Model.ContentId);
                    if (model != null)
                    {
                        e.Content = model;
                        model.IsVisible = anchorable.IsVisible;
                        model.IsActive = anchorable.IsActive;
                        model.IsSelected = anchorable.IsSelected;
                    }
                }
                if (document != null)
                {
                    IContentHandlerRegistry registry = _container.Resolve<IContentHandlerRegistry>();
                    ContentViewModel model = registry.GetViewModelFromContentId(e.Model.ContentId);
                    workspace.Documents.Add(model);
                    if (model != null)
                    {
                        e.Content = model;
                        model.IsActive = document.IsActive;
                        model.IsSelected = document.IsSelected;
                    }
                }
            };
            try
            {
                layoutSerializer.Deserialize(@".\AvalonDock.Layout.config");
            }
            catch (Exception)
            {
                
            }
        }

        public void SaveLayout()
        {
            var layoutSerializer = new XmlLayoutSerializer(dockManager);
            layoutSerializer.Serialize(@".\AvalonDock.Layout.config");
        }
    }
}
