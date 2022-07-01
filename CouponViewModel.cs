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

        public CouponViewModel(INavigation nav)
        {
            _Nav = nav;

            PageTitle = AppResources.CouponTitle;
            GetCoupon();
        }

        private async void GetCoupon()
        {
            try
            {

                // Retrieve the coupon from the app
                IsBusy = true;
                var coupon = await CouponService.GetMemberCoupon(GlobalVar.MemberCode);

                IsBusy = false;

                if (coupon != null)
                {
                    ErrorMessage = null;
                    StackLayout CouponStackLayout;
                    Label CouponNameLabel;
                    BoxView Line;
                    Image CouponImage;
                    Label CouponLimitLabel;
       
                    if (coupon.Length >= 1)
                    {
                        for (int i = 0; i < coupon.Length; i++)
                        {
                           // Increase array
                            if (i != 0)
                            {
                                Array.Resize(ref CouponFrame, CouponFrame.Length + 1);
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

                            // Screen Display Creation
                            CouponStackLayout = new StackLayout
                            {
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                Margin = new Thickness(5, 5, 5, 0)
                            };
                            CouponFrame[i] = new Frame
                            {
                                Content = CouponStackLayout,
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                Margin = new Thickness(20, 10),
                                Padding = new Thickness(20, 10),
                                BorderColor = MainColor,
                                BackgroundColor = Color.White,
                                CornerRadius = 10,
                                HasShadow = true
                            };
                            detail.Children.Add(CouponFrame[i]);

                            CouponNameLabel = new Label
                            {
                                Text = CouponName[i],
                                TextColor = MainColor,
                                FontSize = FontSizeX3Large,
                                FontAttributes = FontAttributes.Bold,
                                HorizontalOptions = LayoutOptions.Center
                            };
                            CouponStackLayout.Children.Add(CouponNameLabel);

                            Line = new BoxView
                            {
                                Color = MainColor,
                                HeightRequest = 1,
                                HorizontalOptions = LayoutOptions.FillAndExpand
                            };
                            CouponStackLayout.Children.Add(Line);

                            CouponImageAddress[i] = coupon[i].ImageUrl ?? "";
                            if (coupon[i].ImageUrl != "")
                            {
                                CouponImageAddress[i] = GlobalVar.WebServerShortAddress + CouponImageAddress[i];
                                CouponImage = new Image
                                {
                                    Source = ImageSource.FromUri(new Uri(CouponImageAddress[i])),
                                    HorizontalOptions = LayoutOptions.FillAndExpand,
                                    VerticalOptions = LayoutOptions.StartAndExpand
                                };
                                CouponStackLayout.Children.Add(CouponImage);
                            }

                            CouponLimitLabel = new Label
                            {
                                Text = CouponLimit[i],
                                TextColor = Color.Black,
                                Margin = new Thickness(0, 10, 0, 0),
                                FontSize = FontSizeXLarge,
                                HorizontalOptions = LayoutOptions.Center
                            };
                            CouponStackLayout.Children.Add(CouponLimitLabel);

                            // Operation when pressed
                            var tgr = new TapGestureRecognizer();
                            tgr.Tapped += (sender, e) => Handle_OnClickedCoupon(sender, e);
                            CouponFrame[i].GestureRecognizers.Add(tgr);
                        }
                    }
                    else
                    {
                        // No coupon
                        CouponNameLabel = new Label
                        {
                            Text = AppResources.CouponNotExists,
                            TextColor = Color.Black,
                            FontSize = FontSizeXLarge,
                            Margin = new Thickness(20, 5, 20, 0)
                        };
                        detail.Children.Add(CouponNameLabel);
                    }
                }
                else
                {
                    // communication error
                    //Label ErrorLabel = new Label
                    //{
                    //    Text = "Unable to retrieve coupon",
                    //    FontSize = FontSizeXLarge,
                    //    TextColor = Color.Red,
                    //    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    //    Margin = new Thickness(0, 5, 0, 0)
                    //};
                    //detail.Children.Add(ErrorLabel);

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
