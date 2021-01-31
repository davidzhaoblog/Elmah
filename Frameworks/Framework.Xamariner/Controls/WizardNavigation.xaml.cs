using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Framework.Xamariner.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WizardNavigation : ContentView
    {
        public WizardNavigation()
        {
            InitializeComponent();
        }
    }
}