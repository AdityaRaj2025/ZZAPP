using Xamarin.Forms;
using ZZAPP.Views;
using ZZAPP.Models;
using ZZAPP.Properties;

namespace ZZAPP.ViewModels
{
    class CouponViewModel : BaseViewModel
    {
        private INavigation _Nav;

        public CouponViewModel(INavigation nav)
        {
            _Nav = nav;

            PageTitle = AppResources.CouponTitle;
        }

        private bool _Processing;
        public bool IsProcessing
        {
            get
            {
                return _Processing;
            }
            set
            {
                SetProperty(ref _Processing, value);
            }
        }
    }
}
