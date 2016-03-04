namespace Bezysoftware.Navigation.Dialogs.ViewModel
{
    using Bezysoftware.Navigation.StatePersistence;
    using Bezysoftware.Navigation.Activation;
    using Bezysoftware.Navigation.Dialogs.View;

    /// <summary>
    /// ViewModel which is used by <see cref="InputDialogContainer"/>.
    /// </summary>
    [StatePersistenceBehavior(StatePersistenceBehaviorType.None)]
    public class InputDialogViewModel : IActivating<InputDialogActivationData>
    {
        public InputDialogActivationData ActivationData
        {
            get;
            private set;
        }

        public void Activating(NavigationType navigationType, InputDialogActivationData data)
        {
            this.ActivationData = data;
        }
    }
}
