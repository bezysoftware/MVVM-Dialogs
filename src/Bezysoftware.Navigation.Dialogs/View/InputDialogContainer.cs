namespace Bezysoftware.Navigation.Dialogs.View
{
    using Bezysoftware.Navigation.Dialogs.Interception;
    using Bezysoftware.Navigation.Dialogs.ViewModel;
    using Bezysoftware.Navigation.Lookup;
    using Microsoft.Practices.ServiceLocation;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Windows.Foundation;
    using Windows.System;
    using Windows.UI.Popups;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Container for the built-in <see cref="ContentDialog"/>. See <seealso cref="InputDialogViewModel"/> and <seealso cref="InputDialogActivationData"/> for more details on activation. 
    /// </summary>
    [DialogContainer(typeof(InputDialogContainer))]
    [AssociatedViewModel(typeof(InputDialogViewModel))]
    public class InputDialogContainer : IDialogContainer
    {
        private INavigationService NavigationService;
        private InputDialogViewModel ViewModel;
        private IAsyncOperation<ContentDialogResult> dialogTask;

        /// <summary>
        /// Shows the dialog. It takes <see cref="InputDialogViewModel"/> to populate the system dialog. It also sets the first command to be 
        /// the default and last one to be the cancel command.
        /// </summary>
        /// <param name="viewType"> Type of dialog control. </param>
        public async void Show(Type viewType)
        {
            this.Initialize();

            var dialog = new ContentDialog
            {
                Title = this.ViewModel.ActivationData.Title,
                MaxWidth = Window.Current.CoreWindow.Bounds.Width
            };

            var panel = new StackPanel();
            panel.Children.Add(new TextBlock
            {
                Text = this.ViewModel.ActivationData.Message,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 6, 0, 12)
            });

            var tb = new TextBox
            {
                Text = this.ViewModel.ActivationData.Text
            };

            panel.Children.Add(tb);
            
            tb.KeyUp += (sender, args) =>
            {
                if (args.Key == VirtualKey.Enter)
                {
                    dialog.Hide();
                }
            };

            dialog.Content = panel;
            dialog.PrimaryButtonText = this.ViewModel.ActivationData.Commands.FirstOrDefault() ?? "Ok";
            dialog.SecondaryButtonText = this.ViewModel.ActivationData.Commands.Skip(1).FirstOrDefault() ?? string.Empty;

            tb.SelectionStart = tb.Text.Length;
            tb.Focus(FocusState.Programmatic);

            try
            {
                this.dialogTask = dialog.ShowAsync();
                var result = await this.dialogTask;

                this.dialogTask = null;
                await this.NavigationService.GoBackAsync(new InputDialogResult(result == ContentDialogResult.Primary ? dialog.PrimaryButtonText : dialog.SecondaryButtonText, tb.Text));
            }
            catch (TaskCanceledException ex)
            {
                // Happens when you call nanavigationSerivce.GoBack(...) while the dialog is still open.
            }
        }

        /// <summary>
        /// Closes the dialog.
        /// </summary>
        /// <param name="viewType"> Type of dialog control. </param>
        public void Hide(Type viewType)
        {
            if (this.dialogTask != null)
            {
                var task = this.dialogTask;
                this.dialogTask = null;
                task.Cancel();
            }
        }

        private void Initialize()
        {
            if (this.ViewModel == null)
            {
                // this would normally happen in xaml by setting DataContext
                this.ViewModel = ServiceLocator.Current.GetInstance<InputDialogViewModel>();
                this.NavigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            }
        }
    }
}
