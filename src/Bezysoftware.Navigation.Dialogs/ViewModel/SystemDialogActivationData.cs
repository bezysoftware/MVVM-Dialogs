namespace Bezysoftware.Navigation.Dialogs.ViewModel
{
    using System;

    /// <summary>
    /// Activation data for <see cref="SystemDialogViewModel"/>
    /// </summary>
    public class SystemDialogActivationData
    {
        public SystemDialogActivationData()
        {
            this.Commands = new string[] { "OK" };
            this.Message = string.Empty;
            this.Title = string.Empty;
        }

        /// <summary>
        /// The message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The title. 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The list of commands. These texts will be displayed on dialog buttons.
        /// </summary>
        public string[] Commands { get; set; }
    }
}
