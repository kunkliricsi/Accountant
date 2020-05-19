using Accountant.APP.Services.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Accountant.APP.Services.Web.Providers
{
    public class ServiceClientFactory<T> : IServiceClientFactory<T> where T : IServiceClient
    {
        private static readonly Dictionary<Type, Type> _container;

        static ServiceClientFactory()
        {
            _container = new Dictionary<Type, Type>()
            {
                [typeof(ICategoriesClient)] = typeof(CategoriesClient),
                [typeof(IExpensesClient)] = typeof(ExpensesClient),
                [typeof(IGroupsClient)] = typeof(GroupsClient),
                [typeof(IReportsClient)] = typeof(ReportsClient),
                [typeof(IShoppingListsClient)] = typeof(ShoppingListsClient),
                [typeof(IUserGroupClient)] = typeof(UserGroupClient),
                [typeof(IUsersClient)] = typeof(UsersClient),
            };
        }

        private readonly ISettingsService _settings;

        public ServiceClientFactory(ISettingsService settings)
        {
            _settings = settings;
        }

        public T CreateClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(_settings.AuthToken);

            return (T)Activator.CreateInstance(_container[typeof(T)], client);
        }
    }
}
