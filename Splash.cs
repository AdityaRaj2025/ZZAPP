namespace ZZAPP.Droid
{
    [Activity(Label = "ZZAPP", Icon = "@mipmap/icon", Theme = "@style/splashscreen", MainLauncher = true, LaunchMode = Android.Content.PM.LaunchMode.SingleTask, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashActivity : Activity
    {
    
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var data = Intent.GetStringExtra(NotificationCenter.ReturnRequest);
            var mainIntent = new Intent(Application.Context, typeof(MainActivity));

            mainIntent.SetFlags(ActivityFlags.SingleTop);
            if (!string.IsNullOrWhiteSpace(data))
            {
                mainIntent.PutExtra(NotificationCenter.ReturnRequest, data);
            }

            StartActivity(mainIntent);
        }
        
        protected override void OnResume()
        {
            base.OnResume();

            Task.Run(() =>
            {
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            });
        }
    }
}
