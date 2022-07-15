public App()
{
            InitializeComponent();
               
            if (Application.Current.Properties.ContainsKey("reservDT")) GlobalVar.ReservDate = (DateTime)Application.Current.Properties["reservDT"];
            
            GlobalVar.ClientID = GetPropValue<string>("ClientID");    
                GlobalVar.WebServerAddress = obj.WebServerAddress;
                GlobalVar.WebServerShortAddress = obj.WebServerShortAddress;
                GlobalVar.Timeout = obj.TimeOut;

                GlobalVar.BackgroundColor = Xamarin.Forms.Color.FromHex(obj.BackgroundColor);
                GlobalVar.MainColor = Xamarin.Forms.Color.FromHex(obj.MainColor);
                GlobalVar.AccentColor = Xamarin.Forms.Color.FromHex(obj.AccentColor);
         
             NotificationCenter.Current.NotificationTapped += OnLocalNotificationTapped;
            
            MainPage = new NavigationPage(new MainPage());
            MainPage.BackgroundColor = GlobalVar.BackgroundColor;
            
            FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
}
private void OnLocalNotificationTapped(NotificationEventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new NotificationNotify());
        }
