using Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Framework.Xamariner;

namespace Framework.Xamariner.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThemeSelector : ContentView
    {
        public ThemeSelector()
        {
            InitializeComponent();
        }
    }
}