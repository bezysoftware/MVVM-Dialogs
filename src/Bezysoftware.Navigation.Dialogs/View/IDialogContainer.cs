namespace Bezysoftware.Navigation.Dialogs.View
{
    using System;

    public interface IDialogContainer
    {
        void Show(Type viewType);

        void Hide(Type viewType);
    }
}
