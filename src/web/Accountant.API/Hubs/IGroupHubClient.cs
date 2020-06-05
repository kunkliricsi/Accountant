using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accountant.API.Hubs
{
    public interface IGroupHubClient
    {
        Task UserJoinedAsync(string userName, string groupName);
    }
}
