using Accountant.APP.Models.Web.UI;
using Accountant.APP.ViewModels;
using Accountant.APP.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using TabbedPage = Xamarin.Forms.TabbedPage;

namespace Accountant.APP.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainView : TabbedPage
    {
        public MainView()
        {
            InitializeComponent();
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<MainViewModel, Tabs>(this, MessageKeys.ChangeTab, (sender, arg) =>
            {
                switch (arg)
                {
                    case Tabs.Categories:
                        CurrentPage = CategoriesView;
                        break;
                    case Tabs.Profile:
                        CurrentPage = ProfileView;
                        break;
                    case Tabs.Reports:
                        CurrentPage = ReportsView;
                        break;
                    case Tabs.Statistics:
                        CurrentPage = StatisticsView;
                        break;
                    case Tabs.ShoppingList:
                        CurrentPage = ShoppingListView;
                        break;
                }
            });
        }

        protected override async void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            await (CurrentPage.BindingContext as ViewModelBase).InitializeAsync(null);
        }
    }
}
