namespace Bezysoftware.Navigation.Dialogs.Sample.Views
{
    using Bezysoftware.Navigation.Dialogs.Sample.ViewModels;
    using Bezysoftware.Navigation.Lookup;
    using Microsoft.Practices.ServiceLocation;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [AssociatedViewModel(typeof(MainViewModel))]
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ShowDialogClick(object sender, RoutedEventArgs e)
        {
            ServiceLocator.Current.GetInstance<INavigationService>().Navigate<DialogViewModel>();
        }
    }
}
