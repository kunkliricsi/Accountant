using Accountant.APP.Models.Web;
using Acr.UserDialogs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eShopOnContainers.Services
{
    public class DialogService : IDialogService
    {
        public Task ShowAlertAsync(string message, string title, string buttonLabel)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }

        public Task<bool> ShowConfirmAsync(string message, string title = null, string okText = null, string cancelText = null, CancellationToken? cancelToken = null)
        {
            return UserDialogs.Instance.ConfirmAsync(message, title, okText, cancelText, cancelToken);
        }

        public Task<string> ShowActionSheetAsync(string title, string cancel, string destructive, CancellationToken? cancelToken = null, params string[] buttons)
        {
            return UserDialogs.Instance.ActionSheetAsync(title, cancel, destructive, cancelToken, buttons);
        }

        public Task<DatePromptResult> ShowDatePromptAsync(string title = null, DateTime? selectedDate = null, CancellationToken? cancelToken = null)
        {
            return UserDialogs.Instance.DatePromptAsync(title, selectedDate, cancelToken);
        }

        public Task<PromptResult> ShowPromptAsync(string message, string title = null, string okText = null, string cancelText = null, string placeholder = "", InputType inputType = InputType.Default, CancellationToken? cancelToken = null)
        {
            return UserDialogs.Instance.PromptAsync(message, title, okText, cancelText, placeholder, inputType, cancelToken);
        }
    }
}
