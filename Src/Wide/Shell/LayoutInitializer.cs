#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using Xceed.Wpf.AvalonDock.Layout;

namespace Wide.Shell
{
    internal class LayoutInitializer : ILayoutUpdateStrategy
    {
        #region ILayoutUpdateStrategy Members

        public bool BeforeInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableToShow, ILayoutContainer destinationContainer)
        {
            var tool = anchorableToShow.Content as ITool;
            if (tool != null)
            {
                var preferredLocation = tool.PreferredLocation;
                string paneName = GetPaneName(preferredLocation);
                var toolsPane = layout.Descendents().OfType<LayoutAnchorablePane>().FirstOrDefault(d => d.Name == paneName);
                if (toolsPane == null)
                {
                    switch (preferredLocation)
                    {
                        case PaneLocation.Left:
                            toolsPane = CreateAnchorablePane(layout, Orientation.Horizontal, paneName, InsertPosition.Start);
                            break;
                        case PaneLocation.Right:
                            toolsPane = CreateAnchorablePane(layout, Orientation.Horizontal, paneName, InsertPosition.End);
                            break;
                        case PaneLocation.Bottom:
                            toolsPane = CreateAnchorablePane(layout, Orientation.Vertical, paneName, InsertPosition.End);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                toolsPane.Children.Add(anchorableToShow);
                return true;
            }

            return false;
        }

        private static string GetPaneName(PaneLocation location)
        {
            switch (location)
            {
                case PaneLocation.Left:
                    return "LeftPane";
                case PaneLocation.Right:
                    return "RightPane";
                case PaneLocation.Bottom:
                    return "BottomPane";
                default:
                    throw new ArgumentOutOfRangeException("location");
            }
        }

        private static LayoutAnchorablePane CreateAnchorablePane(LayoutRoot layout, Orientation orientation,
            string paneName, InsertPosition position)
        {
            var parent = layout.Descendents().OfType<LayoutPanel>().First(d => d.Orientation == orientation);
            var toolsPane = new LayoutAnchorablePane { Name = paneName };
            if (position == InsertPosition.Start)
                parent.InsertChildAt(0, toolsPane);
            else
                parent.Children.Add(toolsPane);
            return toolsPane;
        }

        private enum InsertPosition
        {
            Start,
            End
        }

        public void AfterInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableShown)
        {
            // If this is the first anchorable added to this pane, then use the preferred size.
            var tool = anchorableShown.Content as ITool;
            if (tool != null)
            {
                var anchorablePane = anchorableShown.Parent as LayoutAnchorablePane;
                if (anchorablePane != null && anchorablePane.ChildrenCount == 1)
                {
                    switch (tool.PreferredLocation)
                    {
                        case PaneLocation.Left:
                        case PaneLocation.Right:
                            anchorablePane.DockWidth = new GridLength(tool.PreferredWidth, GridUnitType.Pixel);
                            break;
                        case PaneLocation.Bottom:
                            anchorablePane.DockHeight = new GridLength(tool.PreferredHeight, GridUnitType.Pixel);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        public bool BeforeInsertDocument(LayoutRoot layout, LayoutDocument anchorableToShow, ILayoutContainer destinationContainer)
        {
            return false;
        }

        public void AfterInsertDocument(LayoutRoot layout, LayoutDocument anchorableShown)
        {

        }

        #endregion
    }
}