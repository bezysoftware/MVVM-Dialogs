namespace Bezysoftware.Navigation.Dialogs.ViewModel
{
    using Bezysoftware.Navigation.StatePersistence;
    using Bezysoftware.Navigation.Activation;
    using Bezysoftware.Navigation.Dialogs.View;

    /// <summary>
    /// ViewModel which is used by <see cref="SystemDialogContainer"/>.
    /// </summary>
    [StatePersistenceBehavior(StatePersistenceBehaviorType.None)]
    public class SystemDialogViewModel : IActivate<SystemDialogActivationData>
    {
        public SystemDialogActivationData ActivationData
        {
            get;
            private set;
        }

        public void Activate(NavigationType navigationType, SystemDialogActivationData data)
        {
            this.ActivationData = data;
        }
    }
}
