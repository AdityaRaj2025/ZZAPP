using Xamarin.Forms;
using ZZAPP.Views;
using ZZAPP.Models;
using ZZAPP.Properties;

namespace ZZAPP.ViewModels
{
    class CouponViewModel : BaseViewModel
    {
        private INavigation _Nav;
        
        private CouponInfo[] _CouponInfo;
        public static Frame[] CouponFrame = new Frame[1];
        public static string[] CouponName = new string[1];
        public static string[] CouponLimit = new string[1];
        public static string[] CouponBarcode = new string[1];
        public static bool[] CouponFreeUse = new bool[1];
        public static string[] CouponNote = new string[1];
        public static string[] CouponImageAddress = new string[1];
        public static ImageSource[] CouponImageSource;

        protected IMemberService CouponService => DependencyService.Get<IMemberService>();
        public Command LoadElements { get; set; }
        
        public CouponViewModel(INavigation nav)
        {
            _Nav = nav;

            PageTitle = AppResources.CouponTitle;
            GetCoupon();
            LoadElements = new Command(execute: async () => await ExecuteElements());
        }
        
        async Task ExecuteElements()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new NotificationNotify());
              //await Application.Current.MainPage.Navigation.PushAsync(new CouponDetailPage(CouponName, CouponImageAddress, CouponLimit, CouponNote, CouponFreeUse, CouponBarcode));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
            }
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

                if (coupon != null)
                {
                   
                    if (coupon.Length >= 1)
                    {
                        for (int i = 0; i < coupon.Length; i++)
                        {
                           // Increase array
                            if (i != 0)
                            {
                                Array.Resize(ref CouponName, CouponName.Length + 1);
                                Array.Resize(ref CouponLimit, CouponLimit.Length + 1);
                                Array.Resize(ref CouponBarcode, CouponBarcode.Length + 1);
                                Array.Resize(ref CouponFreeUse, CouponFreeUse.Length + 1);
                                Array.Resize(ref CouponNote, CouponNote.Length + 1);
                                Array.Resize(ref CouponImageAddress, CouponImageAddress.Length + 1);
                            }
                         
                            if (coupon[i].ExpirationDate != DateTime.MaxValue)
                            {
                               
                                CouponLimit[i] = AppResources.DueDate + "：" + coupon[i].ExpirationDate.ToString("yyyy/MM/dd") + AppResources.By;
                            }
                            else
                            {
                                CouponLimit[i] = "　";
                            }
                            CouponName[i] = coupon[i].CouponName;
                            CouponBarcode[i] = coupon[i].Barcode;                         
                            CouponFreeUse[i] = coupon[i].FreeUse;
                            CouponNote[i] = coupon[i].Note;
                            CouponImageAddress[i] = coupon[i].ImageUrl = GlobalVar.WebServerShortAddress + coupon[i].ImageUrl;                           
                        }
                    }
                    else
                    {
                        // No coupon
                        await Application.Current.MainPage.DisplayAlert(AppResources.HttpErrorAlertTitle, AppResources.CouponNotExists, AppResources.OK);
                    }
                }
                else
                {                    
                    ErrorMessage = "Unable to retrieve coupon";
                    CurrentPage = BaseViewModel.PAGE_NONE;
                    await Application.Current.MainPage.DisplayAlert(AppResources.HttpErrorAlertTitle, AppResources.HttpErrorAlertText, AppResources.OK);
                }
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
