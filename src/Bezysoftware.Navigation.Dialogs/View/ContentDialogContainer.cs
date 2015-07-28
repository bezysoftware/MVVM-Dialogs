using Microsoft.Practices.ServiceLocation;
using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Bezysoftware.Navigation.Dialogs.View
{
    public class ContentDialogContainer : IDialogContainer
    {
        private INavigationService NavigationService;
        private Popup popup;

        public void Hide(Type viewType)
        {
            if (this.popup != null)
            {
                Window.Current.SizeChanged -= this.WindowSizeChanged;
                this.popup.KeyUp -= this.PopupKeyUp;

                this.popup.IsOpen = false;
            }
        }

        public async void Show(Type viewType)
        {
            this.Initialize();

            Window.Current.SizeChanged += this.WindowSizeChanged;

            this.popup = new Popup();
            this.popup.IsLightDismissEnabled = false;
            this.popup.Child = (UIElement)Activator.CreateInstance(viewType);
            this.popup.Width = Window.Current.Bounds.Width;
            this.popup.Height = Window.Current.Bounds.Height;
            this.popup.IsOpen = true;
            this.popup.KeyUp += this.PopupKeyUp;
        }
        
        private void PopupKeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Escape)
            {
                this.NavigationService.GoBack(null);
            }
        }

        private void WindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            this.popup.Width = e.Size.Width;
            this.popup.Height = e.Size.Height;
        }

        private void Initialize()
        {
            if (this.NavigationService == null)
            {
                // this would normally happen in xaml by setting DataContext
                this.NavigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            }
        }
    }
}
