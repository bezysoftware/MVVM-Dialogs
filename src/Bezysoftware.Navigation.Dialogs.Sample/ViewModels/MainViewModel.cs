namespace Bezysoftware.Navigation.Dialogs.Sample.ViewModels
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using System.Threading.Tasks;
    using System;
    using Bezysoftware.Navigation.Dialogs.ViewModel;
    using Bezysoftware.Navigation.Activation;

    public class MainViewModel : ViewModelBase, IActivate<string>, IActivate<bool>
    {
        private string dialogResult;
        private readonly INavigationService navigationService;
        private RelayCommand showDialogCommand;

        public MainViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
        
        public string DialogResult
        {
            get { return this.dialogResult; }
            set { this.Set(() => this.DialogResult, ref this.dialogResult, value); }
        }

        public RelayCommand ShowDialogCommand
        {
            get
            {
                return this.showDialogCommand ?? (this.showDialogCommand = new RelayCommand(() => this.ShowDialog()));
            }
        }

        public void Activate(NavigationType navigationType, bool data)
        {
            this.DialogResult = data.ToString();
        }

        public void Activate(NavigationType navigationType, string data)
        {
            this.DialogResult = data;
        }

        private async void ShowDialog()
        {
            // this.navigationService.Navigate<DialogViewModel>();
            var data = new SystemDialogActivationData {
                Title = "Operation confirmation",
                Message = "Do you really want to do this? This action cannot be undone.",
                Commands = new[] {
                    "Yes",
                    "No"
                } 
            };

            //this.navigationService.Navigate<SystemDialogViewModel, SystemDialogActivationData>(data);
            this.navigationService.Navigate<DialogViewModel>();
        }
    }
}