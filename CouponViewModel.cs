using Xamarin.Forms;
using ZZAPP.Views;
using ZZAPP.Models;
using ZZAPP.Properties;

namespace ZZAPP.ViewModels
{
    class CouponViewModel : BaseViewModel
    { 
        private CouponInfo[] _CouponInfo;
        
        protected IMemberService CouponService => DependencyService.Get<IMemberService>();
        
        public CouponViewModel(INavigation nav)
        {
            PageTitle = AppResources.CouponTitle;
            GetCoupon();           
        }
        
        public CouponInfo[] coupon
        {
            get
            {
                return _CouponInfo;
            }
            set
            {
                SetProperty(ref _CouponInfo, value);
            }
        }
   
        private async void GetCoupon()
        {
            try
            {
                IsBusy = true;
                // Retrieve the coupon details from the server
                var coupon = await CouponService.GetMemberCoupon(GlobalVar.MemberCode);

                IsBusy = false;
                    foreach(CouponInfo item in coupon)
                    {
                        item.ImageUrl = GlobalVar.WebServerShortAddress + item.ImageUrl;
                    }
                Coupons = coupon;
            }
            catch(Exception)
            {
                coupon = null;
                ErrorMessage = null;
            }
        }

        public bool ErrorMessageVisible { get { return ErrorMessage != null; } }
        public string _ErrorMessage;
        public string ErrorMessage
        {
            get
            {
                return _ErrorMessage;
            }
            set
            {
                SetProperty(ref _ErrorMessage, value);
                OnPropertyChanged(nameof(ErrorMessageVisible));
            }
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
