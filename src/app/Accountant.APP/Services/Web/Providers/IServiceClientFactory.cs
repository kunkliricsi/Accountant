namespace Accountant.APP.Services.Web.Providers
{
    public interface IServiceClientFactory<T> where T : IServiceClient
    {
        T CreateClient();
    }
}
