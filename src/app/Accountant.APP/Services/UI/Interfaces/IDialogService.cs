using Acr.UserDialogs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eShopOnContainers.Services
{
    public interface IDialogService
    {
        Task ShowAlertAsync(string message, string title, string buttonLabel);
        Task<bool> ShowConfirmAsync(string message, string title = null, string okText = null, string cancelText = null, CancellationToken? cancelToken = null);
        Task<string> ShowActionSheetAsync(string title, string cancel, string destructive, CancellationToken? cancelToken = null, params string[] buttons);
        Task<DatePromptResult> ShowDatePromptAsync(string title = null, DateTime? selectedDate = null, CancellationToken? cancelToken = null);
        Task<PromptResult> ShowPromptAsync(string message, string title = null, string okText = null, string cancelText = null, string placeholder = "", InputType inputType = InputType.Default, CancellationToken? cancelToken = null);
        void ShowToast(string toast);
    }
}
