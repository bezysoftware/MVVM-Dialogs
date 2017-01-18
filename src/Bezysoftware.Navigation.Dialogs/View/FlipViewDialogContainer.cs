namespace Bezysoftware.Navigation.Dialogs.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Bezysoftware.Navigation.Dialogs.Interception;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public class FlipViewDialogContainer : IDialogContainer
    {
        private Stack<FlipViewItem> activatedItems = new Stack<FlipViewItem>();
        private FlipView flipView;

        public void Hide(Type viewType)
        {
            System.Diagnostics.Debug.WriteLine($"Hide, there are {this.activatedItems.Count} items");
            this.activatedItems.Pop();
            var top = this.activatedItems.Peek();
            this.flipView.SelectedItem = top;
        }

        public void Show(Type viewType)
        {
            var attr = viewType.GetTypeInfo().GetCustomAttribute<FlipViewDialogContainerAttribute>();

            this.flipView = Window.Current.Content.FindVisualChildren<FlipView>().FirstOrDefault(cp => cp.Name == attr.FlipViewName);
            var flipViewItem = flipView.Items.Cast<FlipViewItem>().First(item => item.Content.GetType() == viewType);

            System.Diagnostics.Debug.WriteLine($"Show 1, there are {this.activatedItems.Count} items");

            if (this.activatedItems.Count == 0)
            {
                this.activatedItems.Push((FlipViewItem)flipView.Items[flipView.SelectedIndex]);
            }

            this.activatedItems.Push(flipViewItem);

            flipView.SelectedItem = flipViewItem;

            System.Diagnostics.Debug.WriteLine($"Show 2, there are {this.activatedItems.Count} items");
        }
    }
}
