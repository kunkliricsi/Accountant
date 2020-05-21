using Accountant.APP.Models.Web;
using Accountant.APP.Services.Settings.Interfaces;
using Accountant.APP.Services.Web.Interfaces;
using Accountant.APP.ViewModels.Base;
using eShopOnContainers.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

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

        public ICommand OpenReportCommand => new Command<Report>(async r => await OpenReportAsync(r));
        public ICommand EditReportCommand => new Command<Report>(async r => await EditReportAsync(r));
        public ICommand DeleteReportCommand => new Command<Report>(async r => await DeleteReportAsync(r));
        public ICommand AddNewReportCommand => new Command(async () => await AddNewReportAsync());

        private async Task OpenReportAsync(Report report)
        {
            await _navigationService.NavigateToAsync<ExpensesViewModel>(report.Id);
        }

        private async Task EditReportAsync(Report report)
        {
            IsBusy = true;
            try
            {
                var result = await _dialogService.ShowDatePromptAsync("Start date of report", report.StartDate.DateTime);
                if (result.Ok)
                {
                    await _reportService.UpdateReportAsync(new Models.Web.Helpers.UpdateReportModel { Id = report.Id, StartDate = result.SelectedDate, EndDate = result.SelectedDate.AddMonths(1) });
                    await RefreshReports();
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync($"{ex}", "Could not edit report.", "Hmm");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task DeleteReportAsync(Report report)
        {
            IsBusy = true;
            try
            {

                var result = await _dialogService.ShowConfirmAsync($"Do you really want to delete report '{report.Id}:[{report.StartDate}]'", "Are you sure?", "Yes, confirm", "No");
                if (result)
                {
                    await _reportService.DeleteReportAsync(report.Id);
                    await RefreshReports();
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync($"{ex}", "Could not delete report.", "Hmm");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AddNewReportAsync()
        {
            IsBusy = true;
            try
            {

                var result = await _dialogService.ShowDatePromptAsync("Start date of report", DateTime.Now);
                if (result.Ok)
                {
                    await _reportService.CreateReportAsync(new Models.Web.Helpers.AddReportModel { StartDate = result.SelectedDate, EndDate = result.SelectedDate.AddMonths(1), GroupId = _settingsService.GroupId.Value });
                    await RefreshReports();
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync($"{ex}", "Could not delete report.", "Hmm");
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
            var groupId = _settingsService.GroupId;

            if (groupId.HasValue)
                Reports = await _reportService.GetReportsAsync(groupId.Value);
        }
    }
}
