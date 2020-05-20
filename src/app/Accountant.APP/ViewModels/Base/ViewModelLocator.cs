using Accountant.APP.Services.Settings;
using Accountant.APP.Services.Settings.Interfaces;
using Accountant.APP.Services.Web;
using Accountant.APP.Services.Web.Interfaces;
using Accountant.APP.Services.Web.Providers;
using eShopOnContainers.Services;
using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using TinyIoC;
using Xamarin.Forms;

namespace Accountant.APP.ViewModels.Base
{
    public static class ViewModelLocator
    {
        private static readonly TinyIoCContainer _container;

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
        }

        static ViewModelLocator()
        {
            _container = new TinyIoCContainer();

            // View models - by default, TinyIoC will register concrete classes as multi-instance.

            //_container.Register<BasketViewModel>();
            //_container.Register<CatalogViewModel>();
            //_container.Register<CheckoutViewModel>();
            //_container.Register<LoginViewModel>();
            //_container.Register<MainViewModel>();
            //_container.Register<OrderDetailViewModel>();
            //_container.Register<ProfileViewModel>();
            //_container.Register<SettingsViewModel>();
            //_container.Register<CampaignViewModel>();
            //_container.Register<CampaignDetailsViewModel>();

            // Services - by default, TinyIoC will register interface registrations as singletons.

            //_container.Register<INavigationService, NavigationService>();
            //_container.Register<IDialogService, DialogService>();
            //_container.Register<IOpenUrlService, OpenUrlService>();
            //_container.Register<IIdentityService, IdentityService>();
            //_container.Register<IRequestProvider, RequestProvider>();
            //_container.Register<IDependencyService, Services.Dependency.DependencyService>();
            //_container.Register<ISettingsService, SettingsService>();
            //_container.Register<IFixUriService, FixUriService>();
            //_container.Register<ILocationService, LocationService>();
            //_container.Register<ICatalogService, CatalogMockService>();
            //_container.Register<IBasketService, BasketMockService>();
            //_container.Register<IOrderService, OrderMockService>();
            //_container.Register<IUserService, UserMockService>();

            // Clients
            _container.Register<HttpClient>();
            _container.Register<ICategoriesClient, CategoriesClient>();
            _container.Register<IExpensesClient, ExpensesClient>();
            _container.Register<IGroupsClient, GroupsClient>();
            _container.Register<IReportsClient, ReportsClient>();
            _container.Register<IShoppingListsClient, ShoppingListsClient>();
            _container.Register<IUserGroupClient, UserGroupClient>();
            _container.Register<IUsersClient, UsersClient>();

            // Client Factory
            _container.Register(typeof(IServiceClientFactory<>), typeof(ServiceClientFactory<>)).AsMultiInstance();

            // Web Services
            _container.Register<ICategoryService, CategoryService>();
            _container.Register<IExpenseService, ExpenseService>();
            _container.Register<IGroupService, GroupService>();
            _container.Register<IReportService, ReportService>();
            _container.Register<IShoppingListService, ShoppingListService>();
            _container.Register<IUserGroupService, UserGroupService>();
            _container.Register<IUserService, UserService>();

            // UI Services
            _container.Register<INavigationService, NavigationService>();
            _container.Register<IDialogService, DialogService>();

            // Settings Service
            _container.Register<ISettingsService, SettingsService>();
        }

        public static void RegisterSingleton<TInterface, T>() where TInterface : class where T : class, TInterface
        {
            _container.Register<TInterface, T>().AsSingleton();
        }

        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is Element view))
                return;

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }
            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}