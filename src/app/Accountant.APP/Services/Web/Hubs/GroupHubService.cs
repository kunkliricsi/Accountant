using Accountant.APP.Services.Settings.Interfaces;
using Accountant.APP.Services.Web.Interfaces;
using eShopOnContainers.Services;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Accountant.APP.Services.Web.Hubs
{
    public class GroupHubService : IGroupHubService
    {
        private HubConnection _connection;
        private readonly IDialogService _dialogService;
        private readonly ISettingsService _settingsService;
        private const string backendUrl = "http://192.168.1.144:5000/groupHub";

        public event Action HubMessageRecieved;

        public GroupHubService(IDialogService dialogService, ISettingsService settingsService)
        {
            _dialogService = dialogService;
            _settingsService = settingsService;

            _connection = new HubConnectionBuilder()
                .WithUrl(backendUrl, opt =>
                {
                    opt.SkipNegotiation = true;
                    opt.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
                    opt.AccessTokenProvider = () => Task.FromResult(_settingsService.AuthToken);
                })
                .WithAutomaticReconnect()
                .Build();

        }

        public async Task ConnectAsync()
        {
            if (_connection.State == HubConnectionState.Connected) return;

            _connection.On<string, string>("UserJoinedAsync", (userName, groupName) =>
            {
                _dialogService.ShowToast($"{userName} joined group {groupName}.");
                HubMessageRecieved?.Invoke();
            });
            
            await _connection.StartAsync();
        }

        public async Task DisconnectAsync()
        {
            HubMessageRecieved = null;

            await _connection.DisposeAsync();

            _connection = new HubConnectionBuilder()
                .WithUrl(backendUrl)
                .Build();
        }
    }
}
