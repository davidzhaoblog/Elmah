using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Xaml
{
    public struct SignInData
    {
        public string UserName;
        public string Password;
        public bool RememberMe;
        public bool AutoSignIn;
        public string Token;
        public long EntityID;
        public string ShortGuid;
        public bool GoToWelcomeWizard;
    }

    public class SignInData1
    {
        public string Provider;
        public string UserName;
        public string Password;
        public bool RememberMe;
        public bool AutoSignIn;
        public string Token;
        public long EntityID;
        public string ShortGuid;
        public bool GoToWelcomeWizard;
    }
}

