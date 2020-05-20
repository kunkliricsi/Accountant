using Accountant.APP.Services.Settings.Interfaces;
using Accountant.APP.ViewModels.Base;
using System.Net.Http.Headers;

namespace Accountant.APP.Services.Web.Providers
{
    public interface IServiceClientFactory<T> where T : class, IServiceClient
    {
        T CreateClient();
    }

    public class ServiceClientFactory<T> : IServiceClientFactory<T> where T : class, IServiceClient
    {
        private readonly ISettingsService _settings;

        public ServiceClientFactory(ISettingsService settings)
        {
            _settings = settings;
        }

        public T CreateClient()
        {
            var serviceClient = ViewModelLocator.Resolve<T>();
            serviceClient.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _settings.AuthToken);

            return serviceClient;
        }
    }
}
