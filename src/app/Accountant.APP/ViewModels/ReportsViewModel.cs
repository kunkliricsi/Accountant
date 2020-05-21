using Accountant.APP.Models.Web;
using Accountant.APP.Services.Settings.Interfaces;
using Accountant.APP.Services.Web.Interfaces;
using Accountant.APP.ViewModels.Base;
using eShopOnContainers.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace Accountant.APP.ViewModels
{
    public class ReportsViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly INavigationService _navigationService;
        private readonly IReportService _reportService;
        private readonly IExpenseService _expenseService;
        private readonly IDialogService _dialogService;

        public ReportsViewModel(ISettingsService settingsService,
            INavigationService navigationService,
            IReportService reportService,
            IExpenseService expenseService,
            IDialogService dialogService)
        {
            _settingsService = settingsService;
            _navigationService = navigationService;
            _reportService = reportService;
            _expenseService = expenseService;
            _dialogService = dialogService;
        }

        private ICollection<Report> _reports;
        public ICollection<Report> Reports
        {
            get => _reports;
            set => Set(ref _reports, value);
        }

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;
            try
            {
                await RefreshReports();
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync($"{ex}", "Could not initialize reports.", "Hmm");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task RefreshReports()
        {
            Reports = await _reportService.GetReportsAsync(_settingsService.GroupId.Value);
        }
    }
}
