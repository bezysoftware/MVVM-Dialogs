namespace Bezysoftware.Navigation.Dialogs.Interception
{
    using Bezysoftware.Navigation.Dialogs.View;

    public class FlipViewDialogContainerAttribute : DialogContainerAttribute
    {
        public readonly string FlipViewName;

        public FlipViewDialogContainerAttribute(string flipViewName) : base(typeof(FlipViewDialogContainer))
        {
            this.FlipViewName = flipViewName;
        }
    }
}
