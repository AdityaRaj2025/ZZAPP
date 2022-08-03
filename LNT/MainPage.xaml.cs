using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LNT
{
    public partial class MainPage : ContentPage
    {
        DateTime ReservDate = new DateTime(2022, 07, 22, 12, 52, 00);
        string url = "https://en.wikipedia.org/wiki/Wikipedia";
        public MainPage()
        {
            InitializeComponent();
            var browser = new MyWebView
            {
                Source = url,
            };


            //browser.Source = url;

            webview.Children.Add(browser);

        }
        void OnScheduleClick(object sender, EventArgs e)
        {
            var notification = new NotificationRequest
            {
                NotificationId = 100,
                Title = "Happiness +1",
                Description = "Good morning aditya",
                ReturningData = "Dummy data", 
                Schedule =
                {
                    NotifyTime = ReservDate.AddHours(-1)
                }
            };
            NotificationCenter.Current.Show(notification);            
        }

        //void OnWebViewClick(object sender, EventArgs e)
        //{
        //    var browser = new MyWebView
        //    {
        //        Source = url,
        //    };


        //    //browser.Source = url;

        //    webview.Children.Add(browser);
        //}
    }
}
