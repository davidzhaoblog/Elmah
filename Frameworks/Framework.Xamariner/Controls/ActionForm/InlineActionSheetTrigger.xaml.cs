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
    public partial class InlineActionSheetTrigger : ContentView
    {
        public static readonly BindableProperty OnTappedProperty =
            BindableProperty.Create(nameof(OnTapped), typeof(EventHandler), typeof(InlineActionSheetTrigger), null, BindingMode.TwoWay
                , propertyChanged: (sender, oldValue, newValue) => {
                    var ctrl = (InlineActionSheetTrigger)sender;
                    ctrl.OnTapped = (EventHandler)newValue;
                });

        public event EventHandler OnTapped;

        public InlineActionSheetTrigger()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            OnTapped.Invoke(sender, e);
        }
    }
}