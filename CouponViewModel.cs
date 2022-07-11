using Xamarin.Forms;
using ZZAPP.Views;
using ZZAPP.Models;
using ZZAPP.Properties;

namespace ZZAPP.ViewModels
{
    class CouponViewModel : BaseViewModel
    { 
        public CouponViewModel(INavigation nav)
        {
            PageTitle = AppResources.CouponTitle;
           
            GetCoupon();
           
        }

        private CouponInfo[] _CouponInfo;
        public CouponInfo[] Coupons
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

        protected IMemberService CouponService => DependencyService.Get<IMemberService>();
   
        private async void GetCoupon()
        {
            try
            {
                IsBusy = true;
                // Retrieve the coupon details from the server
                var coupon = await CouponService.GetMemberCoupon(GlobalVar.MemberCode);

                IsBusy = false;
                 if(coupon != null)
                {
                    if (coupon.Length >= 1)
                    {
                        foreach (CouponInfo item in coupon)
                        {
                            item.ImageUrl = GlobalVar.WebServerShortAddress + item.ImageUrl;
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(AppResources.HttpErrorAlertTitle, AppResources.CouponNotExists, AppResources.OK);
                        ErrorMessage = AppResources.CouponNotExists;
                    }
                }
                else
                {
                    ErrorMessage = "Unable to get coupon";
                    CurrentPage = BaseViewModel.PAGE_NONE;
                    await Application.Current.MainPage.DisplayAlert(AppResources.HttpErrorAlertTitle, AppResources.HttpErrorAlertText, AppResources.OK); 
                }  
                Coupons = coupon;
            }
            catch(Exception)
            {
                coupon = null;
                ErrorMessage = null;
            }
        }

        public bool _ErrorMessageVisible;
        public bool ErrorMessageVisible 
        {
            get
            {
                return ErrorMessage != null;
            }
            set
            {
                SetProperty(ref _ErrorMessageVisible, value);
                OnPropertyChanged(nameof(ErrorMessageVisible));
            }
        }
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
