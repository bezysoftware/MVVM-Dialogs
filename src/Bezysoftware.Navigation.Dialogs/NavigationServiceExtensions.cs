namespace Bezysoftware.Navigation.Dialogs
{    
    using System;
    using System.Threading.Tasks;

    using Bezysoftware.Navigation.Dialogs.ViewModel;

    public static class NavigationServiceExtensions
    {
        public static async Task<TResult> NavigateWithResult<TViewModel, TResult>(this INavigationService service) 
        {
            //await service.NavigateAsync<TViewModel>();

            //var tcs = new TaskCompletionSource<TResult>();

            //IDialogViewModel<TResult> item = null;

            //EventHandler<EventArgs<TResult>> closedAction = (s, r) => tcs.SetResult(r.Payload);

            //item.Closed += closedAction;

            //var result = await tcs.Task;

            //item.Closed -= closedAction;

            //return result;
            return default(TResult);
        }
    }
}
