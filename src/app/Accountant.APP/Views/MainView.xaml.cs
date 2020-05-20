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

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<MainViewModel, Tabs>(this, MessageKeys.ChangeTab, (sender, arg) =>
            {
                switch (arg)
                {
                    case Tabs.Categories:
                        CurrentPage = CategoriesView;
                        break;
                    case Tabs.Expenses:
                        CurrentPage = ExpensesView;
                        break;
                    case Tabs.Groups:
                        CurrentPage = GroupsView;
                        break;
                    case Tabs.Profile:
                        CurrentPage = ProfileView;
                        break;
                    case Tabs.Reports:
                        CurrentPage = ReportsView;
                        break;
                    case Tabs.ShoppingList:
                        CurrentPage = ShoppingListView;
                        break;
                }
            });

            await ((ReportsViewModel)ReportsView.BindingContext).InitializeAsync(null);
            await ((ShoppingListViewModel)ShoppingListView.BindingContext).InitializeAsync(null);
            await ((CategoriesViewModel)CategoriesView.BindingContext).InitializeAsync(null);
            await ((GroupsViewModel)GroupsView.BindingContext).InitializeAsync(null);
            await ((ExpensesViewModel)ExpensesView.BindingContext).InitializeAsync(null);
            await ((ProfileViewModel)ProfileView.BindingContext).InitializeAsync(null);
        }

        protected override async void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            await (CurrentPage.BindingContext as ViewModelBase).InitializeAsync(null);

            //if (CurrentPage is ReportsView)
            //{
            //    await (ReportsView.BindingContext as ViewModelBase).InitializeAsync(null);
            //}
            //else if (CurrentPage is ShoppingListView)
            //{
            //    await (ShoppingListView.BindingContext as ViewModelBase).InitializeAsync(null);
            //}
            //else if (CurrentPage is CategoriesView)
            //{
            //    await (CategoriesView.BindingContext as ViewModelBase).InitializeAsync(null);
            //}
            //else if (CurrentPage is GroupsView)
            //{
            //    await (GroupsView.BindingContext as ViewModelBase).InitializeAsync(null);
            //}
            //else if (CurrentPage is ExpensesView)
            //{
            //    await (ExpensesView.BindingContext as ViewModelBase).InitializeAsync(null);
            //}
            //else if (CurrentPage is LoginView)
            //{
            //    await (LoginView.BindingContext as ViewModelBase).InitializeAsync(null);
            //}
        }
    }
}
