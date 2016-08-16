namespace Bezysoftware.Navigation.Dialogs.View
{    
    using System;
    using System.Linq;
    using System.Reflection;

    using Bezysoftware.Navigation.Dialogs.Interception;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public class InPlaceDialogContainer : IDialogContainer
    {
        private ContentPresenter contentPresenter;
        private object previousContent;

        public void Hide(Type viewType)
        {
            if (this.contentPresenter != null)
            {
                this.contentPresenter.Content = this.previousContent;
            }
        }

        public void Show(Type viewType)
        {
            var attr = viewType.GetTypeInfo().GetCustomAttribute<InPlaceDialogContainerAttribute>();
            var child = (UIElement)Activator.CreateInstance(viewType);

            this.contentPresenter = Window.Current.Content.FindVisualChildren<ContentPresenter>().FirstOrDefault(cp => cp.Name == attr.ContentPresenterName) ?? this.contentPresenter;

            if (this.contentPresenter != null)
            {
                this.previousContent = this.contentPresenter.Content;
                this.contentPresenter.Content = child;
            }
        }
    }
}
