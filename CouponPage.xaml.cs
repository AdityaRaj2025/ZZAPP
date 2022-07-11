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
        }

        protected override bool OnBackButtonPressed()
        {
            _Model.CloseAlert();
            return true;
        }
        
        async private void MainListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var Selected = e.Item as CouponInfo;

            await Application.Current.MainPage.Navigation.PushAsync(new CouponDetailPage(Selected.CouponName, Selected.ImageUrl, Selected.ExpirationDate.ToString(), Selected.Note, Selected.FreeUse, Selected.Barcode), false);

            ((ListView)sender).SelectedItem = null;

        }
    }
}
