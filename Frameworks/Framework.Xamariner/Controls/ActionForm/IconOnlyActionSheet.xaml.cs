using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Framework.Xamariner.Controls.ActionForm
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IconOnlyActionSheet : ContentView
    {
        public static readonly BindableProperty OrientationProperty =
            BindableProperty.Create(nameof(Orientation), typeof(StackOrientation), typeof(IconOnlyActionSheet), StackOrientation.Horizontal, BindingMode.TwoWay);

        public StackOrientation Orientation
        {
            get { return (StackOrientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public IconOnlyActionSheet()
        {
            InitializeComponent();
        }
    }
}