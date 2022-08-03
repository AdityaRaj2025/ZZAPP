using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LNT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
            Navigation.PopAsync(true);
        }

        void OnWebViewClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage(), true);
        }
    }

}