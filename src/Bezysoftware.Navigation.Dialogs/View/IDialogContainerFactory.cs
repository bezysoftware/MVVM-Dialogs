namespace Bezysoftware.Navigation.Dialogs.View
{
    using System;

    public interface IDialogContainerFactory
    {
        IDialogContainer CreateInstance(Type dialogType);
    }
}
