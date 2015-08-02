namespace Bezysoftware.Navigation.Dialogs.View
{
    using System;
    using System.Threading.Tasks;

    using Bezysoftware.Navigation.Dialogs.Interception;
    using Bezysoftware.Navigation.Dialogs.ViewModel;
    using Bezysoftware.Navigation.Lookup;
    using Microsoft.Practices.ServiceLocation;
    using Windows.UI.Popups;
    using Windows.Foundation;

    /// <summary>
    /// Container for the built-in <see cref="MessageDialog"/>. See <seealso cref="SystemDialogViewModel"/> and <seealso cref="SystemDialogActivationData"/> for more details on activation. 
    /// </summary>
    [DialogContainer(typeof(SystemDialogContainer))]
    [AssociatedViewModel(typeof(SystemDialogViewModel))]
    public class SystemDialogContainer : IDialogContainer
    {
        private INavigationService NavigationService;
        private SystemDialogViewModel ViewModel;
        private IAsyncOperation<IUICommand> dialogTask;

        /// <summary>
        /// Shows the dialog. It takes <see cref="SystemDialogViewModel"/> to populate the system dialog. It also sets the first command to be 
        /// the default and last one to be the cancel command.
        /// </summary>
        /// <param name="viewType"> Type of dialog control. </param>
        public async void Show(Type viewType)
        {
            this.Initialize(); 

            var dialog = new MessageDialog(ViewModel.ActivationData.Message, ViewModel.ActivationData.Title);
            foreach (var command in ViewModel.ActivationData.Commands)
            {
                dialog.Commands.Add(new UICommand(command));
            }

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = (uint)ViewModel.ActivationData.Commands.Length - 1;

            try
            {
                this.dialogTask = dialog.ShowAsync();
                var result = await this.dialogTask;

                this.dialogTask = null;
                this.NavigationService.GoBack(result.Label);
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
                this.ViewModel = ServiceLocator.Current.GetInstance<SystemDialogViewModel>();
                this.NavigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            }
        }
    }
}
