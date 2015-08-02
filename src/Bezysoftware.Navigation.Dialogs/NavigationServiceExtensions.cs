namespace Bezysoftware.Navigation.Dialogs
{
    using Bezysoftware.Navigation.Activation;
    using System;
    using System.Threading.Tasks;

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
            var tcs = new TaskCompletionSource<TResult>();

            if (await service.NavigateAsync<TViewModel>())
            {
                return await AwaitResultAsync(service, tcs);
            }

            return default(TResult);
        }

        /// <summary>
        /// Awaitable method which navigates to given ViewModel along with activation data and returns a result.
        /// </summary>
        /// <typeparam name="TViewModel"> Type of ViewModel to navigate to. </typeparam>
        /// <typeparam name="TResult"> Type of result you are expecting. If a different type is returned, exception is thrown. </typeparam>
        /// <typeparam name="TData"> Type of activation data you are passing to the target ViewModel </typeparam>
        /// <param name="service"> The navigation service. </param>
        /// <returns> Result from the <typeparamref name="TViewModel"/> or default(TResult) if navigation was prevented. </returns>
        public static async Task<TResult> NavigateWithResultAsync<TViewModel, TData, TResult>(this INavigationService service, TData data) where TViewModel : IActivate<TData>
        {
            var tcs = new TaskCompletionSource<TResult>();

            if (await service.NavigateAsync<TViewModel, TData>(data))
            {
                return await AwaitResultAsync(service, tcs);
            }

            return default(TResult);
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

        private static async Task<TResult> AwaitResultAsync<TResult>(INavigationService service, TaskCompletionSource<TResult> tcs)
        {
            EventHandler<NavigationEventArgs> NavigatedAction = (s, r) =>
            {
                try
                {
                    tcs.SetResult((TResult)r.ActivationData);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            };

            service.Navigated += NavigatedAction;
            var result = await tcs.Task;
            service.Navigated -= NavigatedAction;

            return result;
        }
    }
}
