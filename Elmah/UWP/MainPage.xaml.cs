using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Elmah.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            Xamarin.FormsMaps.Init("ypqEczOfCGJzoOrFLlxB~tjaElp1CSeWNLLbdeR5P4g~AlOX3fjMI1_0tS90jPCPnV9gAxplr7WaQC58kNmmXul5ZzostIIubUtfhQNBRAXi");
            LoadApplication(new Elmah.XamarinForms.App());
        }
    }
}

