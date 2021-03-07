using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace Elmah.Droid
{
    /// <summary>
    /// Original Url: https://github.com/xamarin/xamarin-forms-samples/blob/master/WebServices/OAuthNativeFlow/Droid/CustomUrlSchemeInterceptorActivity.cs
    /// Issue: Browser doesn't close due to WebAuthenticatorNativeBrowserActivity has leaked ServiceConnection
    /// https://github.com/xamarin/Xamarin.Auth/issues/275
    /// Fix: devoirtechsandip commented on Sep 16, 2019, (Suggested by EdHubbel)
    /// </summary>
    [Activity(Label = " CustomUrlSchemeInterceptorActivity", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(
        new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataSchemes = new[] { "com.googleusercontent.apps.59029375461-3nsrf1nqihs00fba0b9jauj3r4udhshg" },
        DataPath = "/oauth2redirect")]
    public class CustomUrlSchemeInterceptorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            global::Android.Net.Uri uri_android = Intent.Data;

            Uri uri_netfx = new Uri(uri_android.ToString());

            // load redirect_url Page
            //Elmah.XamarinForms.Authentication.AuthenticationState.Authenticator.OnPageLoading(uri_netfx);

            var intent = new Intent(this, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            StartActivity(intent);

            this.Finish();

            return;
        }
    }
}

