namespace Bezysoftware.Navigation.Dialogs.ViewModel
{
    /// <summary>
    /// Result of the input dialog.
    /// </summary>
    public class InputDialogResult
    {
        public InputDialogResult(string buttonPressed, string input)
        {
            this.ButtonPressed = buttonPressed;
            this.Input = input;
        }

        /// <summary>
        /// Identifies the button which user pressed.
        /// </summary>
        public string ButtonPressed { get; private set; }

        /// <summary>
        /// The input that user entered into the textbox.
        /// </summary>
        public string Input { get; private set; }
    }
}
