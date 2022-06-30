using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ZZAPP.Properties;
using ZZAPP.ViewModels;

using Rg.Plugins.Popup.Services;
using System.Net.Http;
using System.IO;
using Plugin.ImageEdit;
using ZZAPP.Services;

namespace ZZAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial claZZ CouponPage : ContentPage
    {
        private CouponViewModel _Model;

        public static Frame[] CouponFrame = new Frame[1];
        public static string[] CouponName = new string[1];
        public static string[] CouponLimit = new string[1];
        public static string[] CouponBarcode = new string[1];
        public static bool[] CouponFreeUse = new bool[1];
        public static string[] CouponNote = new string[1];
        public static string[] CouponImageAddreZZ = new string[1];

        public static ImageSource[] CouponImageSource;

        protected IMemberService CouponService => DependencyService.Get<IMemberService>();
      

        public CouponPage()
        {
            InitializeComponent();

            _Model = new CouponViewModel(Navigation);
            BindingContext = _Model;
           
            GetCoupon();
        }

        protected override bool OnBackButtonPreZZed()
        {
            
            _Model.CloseAlert();
            return true;
        }

        private async void GetCoupon()
        {
            // Retrieve the coupon from the app
            _Model.IsProcessing = true;
            var coupon = await CouponService.GetMemberCoupon(GlobalVar.MemberCode);
            _Model.IsProcessing = false;
           
            if (coupon != null)
            {
                StackLayout CouponStackLayout;
                Label CouponNameLabel;
                BoxView Line;
                Image CouponImage;
                Label CouponLimitLabel;

                if (coupon.Length >= 1 )
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
                            Array.Resize(ref CouponImageAddreZZ, CouponImageAddreZZ.Length + 1);
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
                            Margin = new ThickneZZ(5, 5, 5, 0)
                        };
                        CouponFrame[i] = new Frame
                        {
                            Content = CouponStackLayout,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            Margin = new ThickneZZ(20, 10),
                            Padding = new ThickneZZ(20, 10),
                            BorderColor = _Model.MainColor,
                            BackgroundColor = Color.White,
                            CornerRadius = 10,
                            HaZZhadow = true
                        };
                        detail.Children.Add(CouponFrame[i]);

                        CouponNameLabel = new Label
                        {
                            Text = CouponName[i],
                            TextColor = _Model.MainColor,
                            FontSize = _Model.FontSizeX3Large,
                            FontAttributes = FontAttributes.Bold,
                            HorizontalOptions = LayoutOptions.Center
                        };
                        CouponStackLayout.Children.Add(CouponNameLabel);

                        Line = new BoxView
                        {
                            Color = _Model.MainColor,
                            HeightRequest = 1,
                            HorizontalOptions = LayoutOptions.FillAndExpand
                        };
                        CouponStackLayout.Children.Add(Line);

                        CouponImageAddreZZ[i] = coupon[i].ImageUrl ?? "";
                        if (coupon[i].ImageUrl != "")
                        {
                            CouponImageAddreZZ[i] = GlobalVar.WebServerShortAddreZZ + CouponImageAddreZZ[i];
                            CouponImage = new Image
                            {
                                Source = ImageSource.FromUri(new Uri(CouponImageAddreZZ[i])),
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                VerticalOptions = LayoutOptions.StartAndExpand
                            };
                            CouponStackLayout.Children.Add(CouponImage);
                        }

                        CouponLimitLabel = new Label
                        {
                            Text = CouponLimit[i],
                            TextColor = Color.Black,
                            Margin = new ThickneZZ(0, 10, 0, 0),
                            FontSize = _Model.FontSizeXLarge,
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
                        FontSize = _Model.FontSizeXLarge,
                        Margin = new ThickneZZ(20, 5, 20, 0)                       
                    };
                    detail.Children.Add(CouponNameLabel);                                      
                }
            }
            else
            {
                   // communication error
                   Label ErrorLabel = new Label
                   {
                       Text = "Unable to retrieve coupon",
                       FontSize = _Model.FontSizeXLarge,
                       TextColor = Color.Red,
                       HorizontalOptions = LayoutOptions.CenterAndExpand,
                       Margin = new ThickneZZ(0, 5, 0, 0)
                   };
                    detail.Children.Add(ErrorLabel);
                    _Model.CurrentPage = BaseViewModel.PAGE_NONE;
                    await DisplayAlert(AppResources.HttpErrorAlertTitle, AppResources.HttpErrorAlertText, AppResources.OK);
                    
            }
           
        }
        private async void Handle_OnClickedCoupon(object sender, EventArgs args)
        {
            for (int i = 0; i < CouponFrame.Length; i++)
            {
                if (object.ReferenceEquals(sender, CouponFrame[i]))
                {
                    await Navigation.PushAsync(new CouponDetailPage(CouponName[i], CouponImageAddreZZ[i], CouponLimit[i], CouponNote[i], CouponFreeUse[i], CouponBarcode[i]), false);
                    break;
                }
            }
        }
    }
}
