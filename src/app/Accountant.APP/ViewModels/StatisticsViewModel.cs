using Accountant.APP.Models.Web;
using Accountant.APP.Services.Settings.Interfaces;
using Accountant.APP.Services.Web.Interfaces;
using Accountant.APP.ViewModels.Base;
using eShopOnContainers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accountant.APP.ViewModels
{
    public class UserStatistics
    {
        public string Name { get; set; }
        public int SumAmount { get; set; }
    }

    public class StatisticsViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly INavigationService _navigationService;
        private readonly IReportService _reportService;
        private readonly IUserService _userService;
        private readonly IExpenseService _expenseService;
        private readonly IDialogService _dialogService;

        public StatisticsViewModel(ISettingsService settingsService,
            INavigationService navigationService,
            IReportService reportService,
            IUserService userService,
            IExpenseService expenseService,
            IDialogService dialogService)
        {
            _settingsService = settingsService;
            _navigationService = navigationService;
            _reportService = reportService;
            _userService = userService;
            _expenseService = expenseService;
            _dialogService = dialogService;
        }

        private ICollection<UserStatistics> _statistics;
        public ICollection<UserStatistics> Statistics
        {
            get => _statistics;
            set => Set(ref _statistics, value);
        }

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            try
            {
                var groupId = _settingsService.GroupId;
                if (groupId.HasValue)
                {
                    var dict = new Dictionary<string, UserStatistics>();

                    var users = await _userService.GetUsersAsync(groupId.Value);
                    foreach (var u in users)
                    {
                        dict.Add(u.Name, new UserStatistics { Name = u.Name, SumAmount = 0 });
                    }

                    var reports = await _reportService.GetReportsAsync(groupId.Value);
                    var expenses = await _expenseService.GetExpensesAsync(reports.Select(r => r.Id).ToArray());

                    foreach (var e in expenses)
                    {
                        dict[e.User.Name].SumAmount += e.Amount;
                    }

                    Statistics = dict.Select(p => p.Value).ToList();
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync($"{ex}", "Could not get statistics.", "Hmm");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
