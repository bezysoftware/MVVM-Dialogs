namespace Bezysoftware.Navigation.Dialogs.Interception
{
    using System;

    using Bezysoftware.Navigation.Dialogs.View;

    public class InPlaceDialogContainerAttribute : DialogContainerAttribute
    {
        public readonly string ContentPresenterName;

        public InPlaceDialogContainerAttribute(string contentPresenterName) : base(typeof(InPlaceDialogContainer))
        {
            this.ContentPresenterName = contentPresenterName;
        }
    }
}
