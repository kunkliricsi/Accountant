using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accountant.APP.Services.Web.Interfaces
{
    public interface IGroupHubService
    {
        event Action HubMessageRecieved;

        Task ConnectAsync();
        Task DisconnectAsync();
    }
}
