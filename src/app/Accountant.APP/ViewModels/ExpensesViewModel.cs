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
    public class ExpensesViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly INavigationService _navigationService;
        private readonly IReportService _reportService;
        private readonly IExpenseService _expenseService;
        private readonly IDialogService _dialogService;
        private int _reportId;

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

        public ICommand EditExpenseCommand => new Command<Expense>(async e => await EditExpenseAsync(e));
        public ICommand DeleteExpenseCommand => new Command<Expense>(async e => await DeleteExpenseAsync(e));
        public ICommand AddNewExpenseCommand => new Command(async () => await AddNewExpense());


        private async Task DeleteExpenseAsync(Expense expense)
        {
            IsBusy = true;

            try
            {
                var result = await _dialogService.ShowConfirmAsync($"Do you really want to delete expense '{expense.Id}:[{expense.Amount}]'", "Are you sure?", "Yes, confirm", "No");
                if (result)
                {
                    await _expenseService.DeleteExpenseAsync(expense.Id);
                    await RefreshExpenses();
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync($"{ex}", "Cannot delete expense.", "Hmm");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task EditExpenseAsync(Expense expense)
        {
            IsBusy = true;

            try
            {
                var result = await _dialogService.ShowPromptAsync("Expense amount:", $"Edit expense '{expense.Id}'.", "Add", "Cancel", $"{expense.Amount}", Acr.UserDialogs.InputType.Number);
                if (result.Ok)
                {
                    expense.Amount = int.Parse(result.Text);
                    await _expenseService.UpdateExpenseAsync(new Models.Web.Helpers.UpdateExpenseModel { Id = expense.Id, Amount = expense.Amount});
                    await RefreshExpenses();
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync($"{ex}", "Cannot edit expense.", "Hmm");
            }
            finally
            {
                IsBusy = false;
            }

        }

        private async Task AddNewExpense()
        {
            IsBusy = true;

            try
            {

                var result = await _dialogService.ShowPromptAsync("Expense amount:", "Add Expense.", "Add", "Cancel", "1500", Acr.UserDialogs.InputType.Number);
                if (result.Ok)
                {
                    var created = await _expenseService.CreateExpenseAsync(new Models.Web.Helpers.AddExpenseModel()
                    {
                        Amount = int.Parse(result.Text),
                        CategoryId = 2,
                        PurchaseDate = DateTime.Now,
                        ReportId = _reportId,
                        UserId = _settingsService.UserId.Value,
                    });

                    await RefreshExpenses();
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync($"{ex}", "Cannot add expense.", "Hmm");
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
                if (navigationData is int reportId)
                {
                    _reportId = reportId;
                    await RefreshExpenses();
                }

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

        private async Task RefreshExpenses()
        {
            Expenses = await _expenseService.GetExpensesAsync(_reportId);
        }
    }
}
