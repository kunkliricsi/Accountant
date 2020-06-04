using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Accountant.API.Hubs
{
    [Authorize]
    public class GroupHub : Hub
    {
        private readonly ILogger<GroupHub> _logger;

        public GroupHub(ILogger<GroupHub> logger)
        {
            _logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            _logger.LogInformation("Client connected.");

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _logger.LogInformation("Client disconnected.");

            return base.OnDisconnectedAsync(exception);
        }

        public Task UserJoinedAsync(string userName, string groupName)
        {
            return Clients.All.SendAsync("JoinedGroup", userName, groupName);
        }
    }
}
