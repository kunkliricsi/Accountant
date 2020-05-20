using Accountant.APP.Models.Web;
using Accountant.APP.Models.Web.Helpers;
using Accountant.APP.Services.Settings.Interfaces;
using Accountant.APP.Services.Web.Interfaces;
using Accountant.APP.ViewModels.Base;
using eShopOnContainers.Services;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Accountant.APP.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;
        private readonly IUserGroupService _userGroupService;
        private readonly IDialogService _dialogService;

        public ProfileViewModel(ISettingsService settingsService,
            INavigationService navigationService,
            IUserService userService,
            IGroupService groupService,
            IUserGroupService userGroupService,
            IDialogService dialogService)
        {
            _settingsService = settingsService;
            _navigationService = navigationService;
            _userService = userService;
            _groupService = groupService;
            _userGroupService = userGroupService;
            _dialogService = dialogService;
        }
        
        private User _user;
        public User User
        {
            get => _user;
            set => Set(ref _user, value);
        }

        private ICollection<Group> _userGroups;
        public ICollection<Group> UserGroups
        {
            get => _userGroups;
            set => Set(ref _userGroups, value);
        }

        private Group _selectedGroup;
        public Group SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                Set(ref _selectedGroup, value);
                _settingsService.GroupId = value?.Id;
            }
        }

        public ICommand LogoutCommand => new Command(async () => await LogoutAsync());
        public ICommand NewGroupCommand => new Command(async () => await NewGroupAsync());
        public ICommand JoinGroupCommand => new Command(async () => await JoinGroupAsync());
        public ICommand DeleteGroupCommand => new Command<Group>(async g => await DeleteGroupAsync(g));
        public ICommand EditGroupCommand => new Command<Group>(async g => await EditGroupAsync(g));

        private async Task LogoutAsync()
        {
            _settingsService.AuthToken = "";
            _settingsService.UserId = null;
            _settingsService.GroupId = null;

            await _navigationService.NavigateToAsync<LoginViewModel>();
            await _navigationService.RemoveBackStackAsync();
        }

        private async Task NewGroupAsync()
        {
            IsBusy = true;

            try
            {
                var result = await _dialogService.ShowPromptAsync("What should be the group's name?", "Add new group", "Add", "Cancel", "Group name");
                if (result.Ok)
                {
                    var created = await _groupService.CreateGroupAsync(new Group()
                    {
                        Name = result.Text,
                    });

                    await _userGroupService.CreateUserGroupAsync(User.Id, created.Id);
                    await RefreshGroups();
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync($"{ex}", "Cannot create group.", "Hmm");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task JoinGroupAsync()
        {
            IsBusy = true;

            try
            {
                // Ugly hack...
                var groups = await _groupService.GetGroupsAsync(Enumerable.Range(1, 100).ToArray());
                var result = await _dialogService.ShowActionSheetAsync("Groups", "", "Cancel", null, groups.Select(g => g.Name).ToArray());
                if (result != "Cancel")
                {
                    var group = groups.SingleOrDefault(g => g.Name == result);
                    if (group != null)
                    {
                        await _userGroupService.CreateUserGroupAsync(_settingsService.UserId.Value, group.Id);
                        await RefreshGroups();
                    }
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync($"{ex}", "Cannot join group.", "Hmm");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task DeleteGroupAsync(Group group)
        {
            IsBusy = true;

            try
            {
                var result = await _dialogService.ShowConfirmAsync($"Do you really want to delete group '{group.Name}'", "Are you sure?", "Yes, confirm", "No");
                if (result)
                {
                    await _groupService.DeleteGroupAsync(group.Id);
                    await RefreshGroups();
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync($"{ex}", "Cannot delete group.", "Hmm");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task EditGroupAsync(Group group)
        {
            IsBusy = true;

            try
            {

                var result = await _dialogService.ShowPromptAsync("What should be the group's name?", $"Edit group '{group.Name}'.", "Add", "Cancel", $"{group.Name}");
                if (result.Ok)
                {
                    group.Name = result.Text;
                    await _groupService.UpdateGroupAsync(group);
                    await RefreshGroups();
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync($"{ex}", "Cannot edit group.", "Hmm");
            }
            finally
            {
                IsBusy = false;
            }

        }
        
        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            try
            {
                await RefreshGroups();
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync($"{ex}", "Getting user failed.", "Return to login page");
                await LogoutAsync();
            }

            IsBusy = false;
        }

        private async Task RefreshGroups()
        {
            User = await _userService.GetUserAsync(_settingsService.UserId.Value);
            UserGroups = User.Groups;

            var settingsGroupId = _settingsService.GroupId;
            if (settingsGroupId != null)
            {
                SelectedGroup = UserGroups.FirstOrDefault(g => g.Id == settingsGroupId);
            }
        }
    }
}
