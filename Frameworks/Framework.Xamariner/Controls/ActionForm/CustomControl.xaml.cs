using Framework.Xaml;
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
    public partial class CustomControl : ContentView
    {
        public static readonly BindableProperty FontIconSettingsProperty =
            BindableProperty.Create(nameof(FontIconSettings), typeof(FontIconSettings), typeof(CustomControl), null, BindingMode.TwoWay);

        public FontIconSettings FontIconSettings
        {
            get { return (FontIconSettings)GetValue(FontIconSettingsProperty); }
            set { SetValue(FontIconSettingsProperty, value); }
        }

        public static BindableProperty TitleProperty =
            BindableProperty.Create("Title", typeof(string), typeof(NavigationItem), string.Empty
            , propertyChanged: (sender, oldValue, newValue) => {
                //var control = (NavigationItem)sender;
            });

        public string Title
        {
            set { SetValue(TitleProperty, value); }
            get { return (string)GetValue(TitleProperty); }
        }

        public static BindableProperty DescriptionProperty =
            BindableProperty.Create("Description", typeof(string), typeof(NavigationItem), string.Empty
            , propertyChanged: (sender, oldValue, newValue) => {
                //var control = (NavigationItem)sender;
            });

        public string Description
        {
            set { SetValue(DescriptionProperty, value); }
            get { return (string)GetValue(DescriptionProperty); }
        }

        public CustomControl()
        {
            InitializeComponent();
        }

        public void SetChild(ContentView control, bool clear)
        {
            if(clear)
            {
                if (this.Container.Children.Any())
                    this.Container.Children.Clear();
            }
            this.Container.Children.Add(control);
        }
    }
}