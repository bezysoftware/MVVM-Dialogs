﻿namespace Bezysoftware.Navigation.Dialogs.Sample.Views
{
    using Windows.UI.Xaml;
    using Bezysoftware.Navigation.Dialogs.Interception;
    using Bezysoftware.Navigation.Dialogs.Sample.ViewModels;
    using Bezysoftware.Navigation.Dialogs.View;
    using Bezysoftware.Navigation.Lookup;
    using Microsoft.Practices.ServiceLocation;
    using Windows.UI.Xaml.Controls;

    [DialogContainer(typeof(PopupDialogContainer))]
    public sealed partial class Dialog : UserControl
    {
        public Dialog()
        {
            this.InitializeComponent();
            this.Loaded += DialogLoaded;
        }

        private void DialogLoaded(object sender, RoutedEventArgs e)
        {
            this.OkButton.Focus(FocusState.Pointer);
        }

        private void YesClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ServiceLocator.Current.GetInstance<INavigationService>().GoBack(true);
        }

        private void NoClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ServiceLocator.Current.GetInstance<INavigationService>().GoBack(false);
        }
    }
}
