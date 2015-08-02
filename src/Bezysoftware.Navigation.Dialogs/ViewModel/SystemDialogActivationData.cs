﻿namespace Bezysoftware.Navigation.Dialogs.ViewModel
{
    /// <summary>
    /// Activation data for <see cref="SystemDialogViewModel"/>
    /// </summary>
    public class SystemDialogActivationData
    {
        public SystemDialogActivationData()
        {
            this.Commands = new string[] { "Ok" };
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