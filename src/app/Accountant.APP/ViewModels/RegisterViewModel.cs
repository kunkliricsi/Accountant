using Accountant.APP.Services.Settings.Interfaces;
using Accountant.APP.Services.Web.Interfaces;
using Accountant.APP.ViewModels.Base;
using eShopOnContainers.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Accountant.APP.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;
        private readonly IDialogService _dialogService;

        public RegisterViewModel(ISettingsService settingsService,
            INavigationService navigationService,
            IUserService userService,
            IDialogService dialogService)
        {
            _settingsService = settingsService;
            _navigationService = navigationService;
            _userService = userService;
            _dialogService = dialogService;
        }

        private string _username;
        public string Username
        {
            get => _username;
            set => Set(ref _username, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => Set(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }

        public ICommand RegisterCommand => new Command(async () => await RegisterAsync());

        private async Task RegisterAsync()
        {
            IsBusy = true;

            try
            {
                await _userService.CreateUserAsync(new Models.Web.Helpers.UpdateModel { Name = Username, Email = Email, Password = Password });
                await _navigationService.NavigateToAsync<LoginViewModel>(Username);
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync($"{ex}", "Register failed.", "OK");
            }
            finally
            {
                Password = "";
                IsBusy = false;
            }
        }

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is string username)
                Username = username;

            return base.InitializeAsync(navigationData);
        }
    }
}
