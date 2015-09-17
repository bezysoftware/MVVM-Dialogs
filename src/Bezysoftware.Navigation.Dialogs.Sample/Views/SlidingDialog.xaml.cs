namespace Bezysoftware.Navigation.Dialogs.Sample.Views
{
    using Windows.UI.Xaml.Controls;
    using Bezysoftware.Navigation.Dialogs.Interception;
    using Bezysoftware.Navigation.Dialogs.Sample.ViewModels;
    using Bezysoftware.Navigation.Dialogs.View;
    using Bezysoftware.Navigation.Lookup;

    [AssociatedViewModel(typeof(SlidingDialogViewModel))]
    [DialogContainer(typeof(SlidingDialogContainer))]
    public sealed partial class SlidingDialog : UserControl
    {
        public SlidingDialog()
        {
            this.InitializeComponent();
        }
    }
}
