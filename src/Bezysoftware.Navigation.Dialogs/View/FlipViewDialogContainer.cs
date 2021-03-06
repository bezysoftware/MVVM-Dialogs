﻿namespace Bezysoftware.Navigation.Dialogs.View
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
            if (this.activatedItems.Any() && this.flipView != null)
            {
                var top = this.activatedItems.Pop();
                this.flipView.SelectedItem = top;
            }
        }

        public void Show(Type viewType)
        {
            var attr = viewType.GetTypeInfo().GetCustomAttribute<FlipViewDialogContainerAttribute>();

            this.flipView = Window.Current.Content.FindVisualChildren<FlipView>().FirstOrDefault(cp => cp.Name == attr.FlipViewName) ?? this.flipView;

            if (this.flipView != null)
            {
                var flipViewItem = flipView.Items.Cast<FlipViewItem>().First(item => item.Content.GetType() == viewType);
                this.activatedItems.Push((FlipViewItem)flipView.SelectedItem);

                flipView.SelectedItem = flipViewItem;
            }
        }
    }
}
