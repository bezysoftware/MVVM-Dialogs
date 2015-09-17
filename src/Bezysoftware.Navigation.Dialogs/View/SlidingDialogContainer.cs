namespace Bezysoftware.Navigation.Dialogs.View
{
    using Windows.UI.Xaml;
    using Bezysoftware.Navigation.Dialogs.View.Xaml;

    /// <summary>
    /// Dialog container mimicking behavior of settings pane in the Windows 10 Mail / People app, where is slides from the right.
    /// </summary>
    public class SlidingDialogContainer : PopupDialogContainer
    {
        private SlidingDialogContainerControl container;

        protected override FrameworkElement CreateVisualContainer(UIElement child)
        {
            this.container = new SlidingDialogContainerControl();
            this.container.ContentContainer.Content = child;
            this.container.CloseRequested += (sender, args) => this.NavigationService.GoBack();
            this.container.ShowPane();
            return container;
        }

        protected override async void HideVisualContainer()
        {
            if (this.container != null)
            {
                await this.container.HidePane();
            }

            base.HideVisualContainer();

            this.container = null;
        }
    }
}
