 protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
           
            base.Window.RequestFeature(WindowFeatures.ActionBar);

            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            NotificationCenter.CreateNotificationChannel(new NotificationChannelRequest());
   
            LoadApplication(new App());

            NotificationCenter.NotifyNotificationTapped(Intent);
        }
