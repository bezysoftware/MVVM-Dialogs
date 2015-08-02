using Microsoft.Practices.ServiceLocation;
using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Input;

namespace Bezysoftware.Navigation.Dialogs.View
{
    public class PopupDialogContainer : IDialogContainer
    {
        private INavigationService NavigationService;
        private Popup popup;
        private bool escapeDown;

        public void Hide(Type viewType)
        {
            if (this.popup != null)
            {
                Window.Current.SizeChanged -= this.WindowSizeChanged;
                Window.Current.Content.KeyUp -= this.PopupKeyUp;
                Window.Current.Content.KeyDown -= this.PopupKeyDown;

                this.popup.IsOpen = false;
            }
        }

        public async void Show(Type viewType)
        {
            this.Initialize();

            this.escapeDown = false;

            Window.Current.SizeChanged += this.WindowSizeChanged;
            Window.Current.Content.KeyUp += this.PopupKeyUp;
            Window.Current.Content.KeyDown += this.PopupKeyDown;

            var wrapper = new Grid();
            wrapper.Width = Window.Current.Bounds.Width;
            wrapper.Height = Window.Current.Bounds.Height;
            wrapper.Children.Add((UIElement)Activator.CreateInstance(viewType));            

            this.popup = new Popup();
            this.popup.IsLightDismissEnabled = false;
            this.popup.Child = wrapper;

            this.popup.IsOpen = true;

            this.TryFocusFirstButton(wrapper);
        }

        private void PopupKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Escape)
            {
                this.escapeDown = true;
            }
            else
            {
                this.escapeDown = false;
            }
        }

        private void PopupKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Escape && this.escapeDown)
            {
                this.NavigationService.GoBack();
            }
        }

        private void WindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            var wrapper = this.popup.Child as FrameworkElement;
            wrapper.Width = e.Size.Width;
            wrapper.Height = e.Size.Height;
        }

        private void Initialize()
        {
            if (this.NavigationService == null)
            {
                // this would normally happen in xaml by setting DataContext
                this.NavigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            }
        }
    
        // Attempts to locate first button and give it focus
        private void TryFocusFirstButton(DependencyObject control)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(control); i++)
            {
                var button = VisualTreeHelper.GetChild(control, i);
                if (button is ButtonBase)
                {
                    (button as ButtonBase).Focus(FocusState.Programmatic);
                    return;
                }

                this.TryFocusFirstButton(button);
            }
        }

    }
}
