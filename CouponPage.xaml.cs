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
    public partial class CouponPage : ContentPage
    {
        private CouponViewModel _Model;

        
      

        public CouponPage()
        {
            InitializeComponent();

            _Model = new CouponViewModel(Navigation);
            BindingContext = _Model;
           
            GetCoupon();
        }

        protected override bool OnBackButtonPressed()
        {
            
            _Model.CloseAlert();
            return true;
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
