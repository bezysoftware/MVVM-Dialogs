namespace Bezysoftware.Navigation.Dialogs.Interception
{
    using System;

    public class DialogAttribute : Attribute
    {
        public readonly Type ContainerType;

        public DialogAttribute(Type containerType)
        {
            this.ContainerType = containerType;
    }
    }
}
