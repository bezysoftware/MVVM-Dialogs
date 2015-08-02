namespace Bezysoftware.Navigation.Dialogs.Sample.Views
{
    using Bezysoftware.Navigation.Dialogs.Interception;
    using Bezysoftware.Navigation.Dialogs.Sample.ViewModels;
    using Bezysoftware.Navigation.Dialogs.View;
    using Bezysoftware.Navigation.Lookup;
    using Microsoft.Practices.ServiceLocation;
    using Windows.UI.Xaml.Controls;

    [AssociatedViewModel(typeof(DialogViewModel))]
    [DialogContainer(typeof(PopupDialogContainer))]
    public sealed partial class Dialog : UserControl
    {
        public Dialog()
        {
            this.InitializeComponent();
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
