namespace Bezysoftware.Navigation.Dialogs.Sample.Views
{
    using Windows.UI.Xaml.Controls;
    using Bezysoftware.Navigation.Dialogs.Interception;
    using Bezysoftware.Navigation.Dialogs.Sample.ViewModels;
    using Bezysoftware.Navigation.Dialogs.View;
    using Bezysoftware.Navigation.Lookup;

    [AssociatedViewModel(typeof(DialogWithoutPageViewModel))]
    [DialogContainer(typeof(PopupDialogContainer))]
    public sealed partial class DialogWithoutPage : UserControl
    {
        public DialogWithoutPage()
        {
            this.InitializeComponent();
        }
    }
}
