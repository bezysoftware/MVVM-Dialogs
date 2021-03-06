namespace Bezysoftware.Navigation.Dialogs.Sample.ViewModels
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using Bezysoftware.Navigation.Dialogs.ViewModel;
    using Repository.Settings;

    public class MainViewModel : ViewModelBase
    {
        private string dialogResult;
        private readonly INavigationService navigationService;
        private readonly ISettingsRepository<Settings> settings;
        private RelayCommand<string> showDialogCommand;

        public MainViewModel(INavigationService navigationService, ISettingsRepository<Settings> settings)
        {
            this.navigationService = navigationService;
            this.settings = settings;
        }
        
        public string DialogResult
        {
            get { return this.dialogResult; }
            set { this.Set(() => this.DialogResult, ref this.dialogResult, value); }
        }

        public RelayCommand<string> ShowDialogCommand
        {
            get
            {
                return this.showDialogCommand ?? (this.showDialogCommand = new RelayCommand<string>(s => this.ShowDialog(s)));
            }
        }

        private async void ShowDialog(string s)
        {
            if (s == "popup")
            {
                this.DialogResult += " " + (await this.navigationService.NavigateWithResultAsync<DialogViewModel, bool>()).ToString();
            }
            else if (s == "popupNoPage")
            {
                this.DialogResult += " " + (await this.navigationService.NavigateWithResultAsync<DialogWithoutPageViewModel, bool>()).ToString();
            }
            else if (s == "sliding")
            {
                await this.navigationService.NavigateAsync<SlidingDialogViewModel>();
            }
            else if (s == "system")
            {
                var data = new SystemDialogActivationData
                {
                    Title = "Operation confirmation",
                    Message = "Do you really want to do this? Nothing will happen.",
                    Commands = new[] {
                        "Yes",
                        "No"
                    }
                };
                this.DialogResult += " " + await this.navigationService.NavigateWithResultAsync<SystemDialogViewModel, SystemDialogActivationData, string>(data);
            }
        }
    }
}