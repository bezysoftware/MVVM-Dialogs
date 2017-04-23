namespace Bezysoftware.Navigation.Dialogs.View
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public class ContentDialogContainer : IDialogContainer
    {
        private ContentDialog dialog;
        
        public async void Show(Type viewType)
        {
            var child = (UIElement)Activator.CreateInstance(viewType);
            this.dialog = new ContentDialog() { Content = child };

            await dialog.ShowAsync();
        }

        public void Hide(Type viewType)
        {
            this.dialog?.Hide();
            this.dialog = null;
        }
    }
}
