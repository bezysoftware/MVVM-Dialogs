namespace Bezysoftware.Navigation.Dialogs.Interception
{
    using System;

    /// <summary>
    /// Attribute identifying dialog controls.
    /// </summary>
    public class DialogContainerAttribute : Attribute
    {
        /// <summary>
        /// Type of container which should be used to display this dialog. It must implement <see cref="IDialogContainer"/> interface.
        /// </summary>
        public readonly Type ContainerType;

        /// <summary>
        /// Creates a new instance of <see cref="DialogContainerAttribute"/>.
        /// </summary>
        /// <param name="containerType"> Type of container which should be used to display this dialog. It must implement <see cref="IDialogContainer"/> interface. </param>
        public DialogContainerAttribute(Type containerType)
        {
            this.ContainerType = containerType;
    }
    }
}
