using Accountant.APP.Models.Web;
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
    public class LoginViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;
        private readonly IDialogService _dialogService;

        public LoginViewModel(ISettingsService settingsService,
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

        private string _password;
        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }

        public ICommand LoginCommand => new Command(async () => await LoginAsync());
        public ICommand RegisterCommand => new Command(async () => await RegisterAsync());

        private async Task LoginAsync()
        {
            IsBusy = true;

            try
            {
                var user = await _userService.AuthenticateUserAsync(Username, Password);
                _settingsService.AuthToken = user.Token;
                _settingsService.UserId = user.Id;
                await _navigationService.NavigateToAsync<MainViewModel>();
                await _navigationService.RemoveBackStackAsync();
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync($"{ex}", "Login failed", "OK");
            }
            finally
            {
                Password = "";
                IsBusy = false;
            }
        }

        private Task RegisterAsync()
        {
            return _navigationService.NavigateToAsync<RegisterViewModel>(Username);
        }

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is string username)
                Username = username;

            return base.InitializeAsync(navigationData);
        }
    }
}
