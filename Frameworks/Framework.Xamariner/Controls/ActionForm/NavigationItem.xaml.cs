using Framework.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Framework.Xamariner.Controls.ActionForm
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationItem : ContentView
    {
        public static readonly BindableProperty FontIconSettingsProperty =
            BindableProperty.Create(nameof(FontIconSettings), typeof(FontIconSettings), typeof(NavigationItem), null, BindingMode.TwoWay);

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
        
        public static readonly BindableProperty ShowNavigationIconProperty =
            BindableProperty.Create(nameof(ShowNavigationIcon), typeof(bool), typeof(NavigationItem), true, BindingMode.TwoWay);

        public bool ShowNavigationIcon
        {
            get { return (bool)GetValue(ShowNavigationIconProperty); }
            set { SetValue(ShowNavigationIconProperty, value); }
        }

        public static readonly BindableProperty NavigationIconProperty =
            BindableProperty.Create(nameof(NavigationIcon), typeof(string), typeof(NavigationItem), Framework.Xaml.FontAwesomeIcons.ChevronRight, BindingMode.TwoWay);

        public string NavigationIcon
        {
            get { return (string)GetValue(NavigationIconProperty); }
            set { SetValue(NavigationIconProperty, value); }
        }

        public static readonly BindableProperty NavigationIconFamilyProperty =
            BindableProperty.Create(nameof(NavigationIconFamily), typeof(string), typeof(NavigationItem), null, BindingMode.TwoWay);

        public string NavigationIconFamily
        {
            get { return (string)GetValue(NavigationIconFamilyProperty); }
            set { SetValue(NavigationIconFamilyProperty, value); }
        }

        public static readonly BindableProperty NavigationIconSizeProperty =
            BindableProperty.Create(nameof(NavigationIconSize), typeof(double), typeof(NavigationItem), 24.0d, BindingMode.TwoWay);

        public double NavigationIconSize
        {
            get { return (double)GetValue(NavigationIconSizeProperty); }
            set { SetValue(NavigationIconSizeProperty, value); }
        }

        public static BindableProperty NavigationCommandProperty =
            BindableProperty.Create("NavigationCommand", typeof(ICommand), typeof(NavigationItem), null);

        public ICommand NavigationCommand
        {
            set { SetValue(NavigationCommandProperty, value); }
            get { return (ICommand)GetValue(NavigationCommandProperty); }
        }

        public static BindableProperty NavigationCommandParamProperty =
            BindableProperty.Create("NavigationCommandParam", typeof(object), typeof(NavigationItem), null);

        public object NavigationCommandParam
        {
            set { SetValue(NavigationCommandParamProperty, value); }
            get { return (object)GetValue(NavigationCommandParamProperty); }
        }

        public NavigationItem()
        {
            InitializeComponent();
            AddTapGestureRecognizer(this);
        }

        private static void AddTapGestureRecognizer(NavigationItem tt)
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandProperty, "NavigationCommand");
            tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandParameterProperty, "NavigationCommandParam");

            tt.Container.GestureRecognizers.Add(tapGestureRecognizer);
            if (tt.ShowNavigationIcon)
            {
                if (tt.NavigationLabel.GestureRecognizers.Any(t => t is TapGestureRecognizer))
                {
                    var original = tt.NavigationLabel.GestureRecognizers.FirstOrDefault(t => t is TapGestureRecognizer);
                    tt.NavigationLabel.GestureRecognizers.Remove(original);
                }
                tt.NavigationLabel.GestureRecognizers.Add(tapGestureRecognizer);
            }
            else
            {
                if (tt.Container.GestureRecognizers.Any(t => t is TapGestureRecognizer))
                {
                    var original = tt.Container.GestureRecognizers.FirstOrDefault(t => t is TapGestureRecognizer);
                    tt.Container.GestureRecognizers.Remove(original);
                }
                tt.Container.GestureRecognizers.Add(tapGestureRecognizer);
            }
        }
    }
}