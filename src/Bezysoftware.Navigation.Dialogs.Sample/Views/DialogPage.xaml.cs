namespace Bezysoftware.Navigation.Dialogs.Sample.Views
{
    using Windows.UI.Xaml.Controls;
    using Bezysoftware.Navigation.Dialogs.Sample.ViewModels;
    using Bezysoftware.Navigation.Lookup;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [AssociatedViewModel(typeof(DialogViewModel))]
    public sealed partial class DialogPage : Page
    {
        public DialogPage()
        {
            this.InitializeComponent();
        }
    }
}
