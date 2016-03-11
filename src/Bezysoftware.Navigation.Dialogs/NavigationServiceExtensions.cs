namespace Bezysoftware.Navigation.Dialogs
{
    using Bezysoftware.Navigation.Activation;
    using System;
    using System.Threading.Tasks;
    using Bezysoftware.Navigation.Dialogs.ViewModel;

    /// <summary>
    /// Dialog related extensions for the <see cref="INavigationService"/>
    /// </summary>
    public static class NavigationServiceExtensions
    {
        /// <summary>
        /// Awaitable method which navigates to given ViewModel and returns a result.
        /// </summary>
        /// <typeparam name="TViewModel"> Type of ViewModel to navigate to. </typeparam>
        /// <typeparam name="TResult"> Type of result you are expecting. If a different type is returned, exception is thrown. </typeparam>
        /// <param name="service"> The navigation service. </param>
        /// <returns> Result from the <typeparamref name="TViewModel"/> or default(TResult) if navigation was prevented. </returns>
        public static async Task<TResult> NavigateWithResultAsync<TViewModel, TResult>(this INavigationService service) 
        {
            return await AwaitResultAsync<TResult>(service.NavigateAsync<TViewModel>, service, service.ActiveViewModelType);
        }

        /// <summary>
        /// Awaitable method which navigates to given ViewModel along with activation data and returns a result.
        /// </summary>
        /// <typeparam name="TViewModel"> Type of ViewModel to navigate to. </typeparam>
        /// <typeparam name="TResult"> Type of result you are expecting. If a different type is returned, exception is thrown. </typeparam>
        /// <typeparam name="TData"> Type of activation data you are passing to the target ViewModel </typeparam>
        /// <param name="service"> The navigation service. </param>
        /// <returns> Result from the <typeparamref name="TViewModel"/> or default(TResult) if navigation was prevented. </returns>
        public static async Task<TResult> NavigateWithResultAsync<TViewModel, TData, TResult>(this INavigationService service, TData data) 
        {
            return await AwaitResultAsync<TResult>(() => service.NavigateAsync<TViewModel, TData>(data), service, service.ActiveViewModelType);
        }

        /// <summary>
        /// Awaitable method which navigates to given ViewModel and waits until it navigates back again.
        /// </summary>
        /// <typeparam name="TViewModel"> Type of ViewModel to navigate to. </typeparam>
        /// <param name="service"> The navigation service. </param>
        /// <returns> Result from the <typeparamref name="TViewModel"/> or default(TResult) if navigation was prevented. </returns>
        public static async Task NavigateAndWaitAsync<TViewModel>(this INavigationService service)
        {
            await AwaitResultAsync<object>(service.NavigateAsync<TViewModel>, service, service.ActiveViewModelType);
        }

        /// <summary>
        /// Awaitable method which navigates to given ViewModel along with activation data and waits until it navigates back again.
        /// </summary>
        /// <typeparam name="TViewModel"> Type of ViewModel to navigate to. </typeparam>
        /// <typeparam name="TData"> Type of activation data you are passing to the target ViewModel </typeparam>
        /// <param name="service"> The navigation service. </param>
        /// <returns> Result from the <typeparamref name="TViewModel"/> or default(TResult) if navigation was prevented. </returns>
        public static async Task NavigateAndWaitAsync<TViewModel, TData>(this INavigationService service, TData data)
        {
            await AwaitResultAsync<object>(() => service.NavigateAsync<TViewModel, TData>(data), service, service.ActiveViewModelType);
        }

        /// <summary>
        /// Navigates to <see cref="SystemDialogViewModel"/>.
        /// </summary>
        public static async Task ShowSystemDialogAsync(this INavigationService service, string message, string title = "" )
        {
            await service.NavigateAndWaitAsync<SystemDialogViewModel, SystemDialogActivationData>(new SystemDialogActivationData {Message = message, Title = title});
        }

        /// <summary>
        /// Navigates to <see cref="SystemDialogViewModel"/>.
        /// </summary>
        public static async Task<string> ShowConfirmationDialogAsync(this INavigationService service, string message, string title = "", string yesLabel = "", string noLabel = "")
        {
            var data = new SystemDialogActivationData
            {
                Message = message,
                Title = title,
                Commands = new[] { yesLabel, noLabel }
            };

            return await AwaitResultAsync<string>(() => service.NavigateAsync<SystemDialogViewModel, SystemDialogActivationData>(data), service, service.ActiveViewModelType);
        }

        /// <summary>
        /// Navigates to <see cref="InputDialogViewModel"/>.
        /// </summary>
        public static async Task<InputDialogResult> ShowInputDialogAsync(this INavigationService service, string message, string title = "", string originalText = "", string[] commands = null)
        {
            var data = new InputDialogActivationData
            {
                Message = message,
                Title = title,
                Text = originalText,
                Commands = commands ?? new[] { "Ok" }
            };

            return await AwaitResultAsync<InputDialogResult>(() => service.NavigateAsync<InputDialogViewModel, InputDialogActivationData>(data), service, service.ActiveViewModelType);
        }

        /// <summary>
        /// Sets a default value to deactivation data when going back and deactivation data is null.
        /// </summary>
        /// <param name="parameters"> The deactivation parameters. </param>
        /// <param name="navigationType"> The navigation type. </param>
        /// <param name="defaultValue"> The default value. </param>
        public static void OverrideDefaultResult(this DeactivationParameters parameters, NavigationType navigationType, object defaultValue)
        {
            if (navigationType == NavigationType.Backward && parameters.DeactivationData == null)
            {
                parameters.DeactivationData = defaultValue;
            }
        }

        private static async Task<TResult> AwaitResultAsync<TResult>(Func<Task<bool>> navigationFunc, INavigationService service, Type currentViewModelType)
        {
            var tcs = new TaskCompletionSource<TResult>();

            EventHandler<NavigationEventArgs> navigatedAction = (s, r) =>
            {
                try
                {
                    // if returning to where I started
                    if (r.ViewModelType == currentViewModelType)
                    {
                        tcs.SetResult((TResult)r.ActivationData);
                    }
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            };

            service.Navigated += navigatedAction;

            if (!(await navigationFunc()))
            {
                // if navigation forward does not occur (e.g. current VM could not be deactivated) set the result to default
                tcs.SetResult(default(TResult));
            }

            var result = await tcs.Task;
            service.Navigated -= navigatedAction;

            return result;
        }
    }
}
