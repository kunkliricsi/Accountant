using System.Net.Http;

namespace Accountant.APP.Services.Web.Providers
{
    public interface IServiceClient
    {
        HttpClient Client { get; }
    }

    public partial class CategoriesClient
    {
        public HttpClient Client => _httpClient;
    }

    public partial class ExpensesClient
    {
        public HttpClient Client => _httpClient;
    }

    public partial class GroupsClient
    {
        public HttpClient Client => _httpClient;
    }

    public partial class ReportsClient
    {
        public HttpClient Client => _httpClient;
    }

    public partial class ShoppingListsClient
    {
        public HttpClient Client => _httpClient;
    }

    public partial class UserGroupClient
    {
        public HttpClient Client => _httpClient;
    }

    public partial class UsersClient
    {
        public HttpClient Client => _httpClient;
    }
}
