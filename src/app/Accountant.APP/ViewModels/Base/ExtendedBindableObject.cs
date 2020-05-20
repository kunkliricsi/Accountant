using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Accountant.APP.ViewModels.Base
{
    public abstract class ExtendedBindableObject : BindableObject
    {
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(propertyName);
        }
    }
}
