namespace Elmah.XamarinForms.Authentication
{
    public static class GoogleAuthenticationConstants
    {
        public static string AppName = "OAuthNativeFlow";

        // OAuth
        // For Google login, configure at https://console.developers.google.com/
        //public static string iOSClientId = "<insert IOS client ID here>";
        //public static string AndroidClientId = "<insert Android client ID here>";
        public static string iOSClientId = "59029375461-mj9gimmfgl3ta8hd0t4988la0fj9im7j.apps.googleusercontent.com";
        public static string AndroidClientId = "59029375461-3nsrf1nqihs00fba0b9jauj3r4udhshg.apps.googleusercontent.com";

        // These values do not need changing
        public static string Scope = "https://www.googleapis.com/auth/userinfo.email";
        public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        // Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
        public static string iOSRedirectUrl = "59029375461-mj9gimmfgl3ta8hd0t4988la0fj9im7j.apps.googleusercontent.com:/oauth2redirect";
        public static string AndroidRedirectUrl = "com.googleusercontent.apps.59029375461-3nsrf1nqihs00fba0b9jauj3r4udhshg:/oauth2redirect";
    }
}

