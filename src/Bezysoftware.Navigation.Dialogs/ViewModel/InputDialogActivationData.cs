namespace Bezysoftware.Navigation.Dialogs.ViewModel
{
    /// <summary>
    /// Activation data for <see cref="InputDialogViewModel"/>
    /// </summary>
    public class InputDialogActivationData : SystemDialogActivationData
    {
        public InputDialogActivationData()
        {
            this.Text = string.Empty;
        }

        /// <summary>
        /// The text.
        /// </summary>
        public string Text { get; set; }
    }
}
