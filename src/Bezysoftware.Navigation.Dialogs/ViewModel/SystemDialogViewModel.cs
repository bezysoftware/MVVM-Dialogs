namespace Bezysoftware.Navigation.Dialogs.ViewModel
{
    using System;
    using System.Threading.Tasks;

    using Bezysoftware.Navigation.StatePersistence;
    using Bezysoftware.Navigation.Activation;

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
