namespace Bezysoftware.Navigation.Dialogs.Sample.Views
{
    using Windows.UI.Xaml.Controls;
    using Bezysoftware.Navigation.Dialogs.Lookup;
    using Bezysoftware.Navigation.Dialogs.Sample.ViewModels;
    using Bezysoftware.Navigation.Platform;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [AssociatedDialogViewModel(typeof(DialogViewModel), typeof(Dialog))]
    [AdaptiveNavigationByWidth(MinWidth = 600)]
    public sealed partial class DialogPage : Page
    {
        public DialogPage()
        {
            this.InitializeComponent();
        }
    }
}
