using Accountant.APP.ViewModels.Base;
using Accountant.APP.Views;
using eShopOnContainers.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Accountant.APP
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override async void OnStart()
        {
            base.OnStart();

            await InitializeNavigation();

            base.OnResume();
        }

        private Task InitializeNavigation()
        {
            return ViewModelLocator.Resolve<INavigationService>().InitializeAsync();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
