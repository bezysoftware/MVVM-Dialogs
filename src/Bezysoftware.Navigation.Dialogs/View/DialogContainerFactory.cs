namespace Bezysoftware.Navigation.Dialogs.View
{
    using System;

    public class DialogContainerFactory : IDialogContainerFactory
    {
        public IDialogContainer CreateInstance(Type dialogType)
        {
            return Activator.CreateInstance(dialogType) as IDialogContainer;
        }
    }
}
