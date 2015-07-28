namespace Bezysoftware.Navigation.Dialogs.ViewModel
{
    public class SystemDialogActivationData
    {
        public SystemDialogActivationData()
        {
            this.Commands = new string[] { "Ok" };
        }

        public string Message { get; set; }

        public string Title { get; set; }

        public string[] Commands { get; set; }
    }
}
