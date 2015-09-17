namespace Bezysoftware.Navigation.Dialogs.View.Xaml
{
    using System;
    using System.Threading.Tasks;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Controls;

    public sealed partial class SlidingDialogContainerControl : UserControl
    {
        public SlidingDialogContainerControl()
        {
            this.InitializeComponent();
        }


        public event EventHandler<EventArgs> CloseRequested;

        public ContentPresenter ContentContainer => this.ContentPresenter;

        public void ShowPane()
        {
            this.ShowPaneAnimation.Begin();
        }

        public Task HidePane()
        {
            var tcs = new TaskCompletionSource<object>();
            this.HidePaneAnimation.Completed += (sender, o) => tcs.SetResult(new object());
            this.HidePaneAnimation.Begin();

            return tcs.Task;
        }

        private void OverlayTapped(object sender, TappedRoutedEventArgs e)
        {
            this.CloseRequested?.Invoke(this, new EventArgs());
        }

        private void ContentPresenterTapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;
        }
    }
}
