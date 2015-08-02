namespace Bezysoftware.Navigation.Dialogs.View
{
    using System;

    /// <summary>
    /// Container holding the dialog control.
    /// </summary>
    public interface IDialogContainer
    {
        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <param name="viewType"> Type of dialog control. </param>
        void Show(Type viewType);

        /// <summary>
        /// Closes the dialog.
        /// </summary>
        /// <param name="viewType"> Type of dialog control. </param>
        void Hide(Type viewType);
    }
}
