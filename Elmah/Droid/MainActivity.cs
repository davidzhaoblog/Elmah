using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;

using System.Threading.Tasks;

namespace Elmah.Droid
{
    [Activity(Label = "NTierOnTime", Icon = "@mipmap/ic_launcher", Theme = "@style/splashscreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            AppCenter.Start("f76af7ff-2612-42c4-a3a5-b53ac91d0c10", typeof(Push));

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            // https://romannurik.github.io/AndroidAssetStudio/nine-patches.html#&sourceDensity=320&name=example
            base.Window.RequestFeature(WindowFeatures.ActionBar);
            // Name of the MainActivity theme you had there before.
            // Or you can use global::Android.Resource.Style.ThemeHoloLight
            base.SetTheme(Resource.Style.MainTheme);

            base.OnCreate(bundle);

            Xamarin.Essentials.Platform.Init(this, bundle);
            Xamarin.Forms.Forms.SetFlags("SwipeView_Experimental");
            Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.Forms.FormsMaterial.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);
            Xamarin.Auth.Presenters.XamarinAndroid.AuthenticationConfiguration.Init(this, bundle);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);

            // TODO: Xam.PlugIn.Media, looks like no initialization required.
            //Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);

            System.Net.ServicePointManager.ServerCertificateValidationCallback += (o, cert, chain, errors) => true;
//#if DEBUG
//            System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) =>
//            {
//                if (certificate.Issuer.Equals("CN=localhost"))
//                    return true;
//                return sslPolicyErrors == System.Net.Security.SslPolicyErrors.None;
//            };
//#endif

            LoadApplication(new Elmah.XamarinForms.App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

