using Accountant.APP.Models.Web;
using Accountant.APP.Services.Settings.Interfaces;
using Accountant.APP.Services.Web.Interfaces;
using Accountant.APP.ViewModels.Base;
using eShopOnContainers.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accountant.APP.ViewModels
{
    public class ExpensesViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly INavigationService _navigationService;
        private readonly IReportService _reportService;
        private readonly IExpenseService _expenseService;
        private readonly IDialogService _dialogService;

        public ExpensesViewModel(ISettingsService settingsService,
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

        private ICollection<Expense> _expenses;
        public ICollection<Expense> Expenses
        {
            get => _expenses;
            set => Set(ref _expenses, value);
        }

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            try
            {
                if (navigationData is int reportId)
                    Expenses = await _expenseService.GetExpensesAsync(reportId);
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync($"{ex}", "Could not get expenses", "Hmm");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
