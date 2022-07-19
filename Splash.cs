namespace SSAPP.Droid
{
    [Activity(Label = "ZZAPP", Icon = "@mipmap/icon", Theme = "@style/splashscreen", MainLauncher = true, LaunchMode = Android.Content.PM.LaunchMode.SingleTop, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
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
