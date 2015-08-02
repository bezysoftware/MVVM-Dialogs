namespace Bezysoftware.Navigation.Dialogs.Sample.ViewModels
{
    using System.Threading.Tasks;
    using Bezysoftware.Navigation.Activation;
    using Bezysoftware.Navigation.StatePersistence;

    [StatePersistenceBehavior(StatePersistenceBehaviorType.None)]
    public class DialogViewModel : IDeactivate
    {
        public async Task DeactivateAsync(NavigationType navigationType, DeactivationParameters parameters)
        {
            parameters.OverrideDefaultResult(navigationType, false);
        }
    }
}
