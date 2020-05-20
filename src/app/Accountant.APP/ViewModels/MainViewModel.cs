using Accountant.APP.Models.Web.UI;
using Accountant.APP.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Accountant.APP.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public override Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            if (navigationData is Tabs tab)
            {
                // Change selected application tab
                MessagingCenter.Send(this, MessageKeys.ChangeTab, tab);
            }

            return base.InitializeAsync(navigationData);
        }
    }
}
