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
    public partial class VerticalSwipeBar : ContentView
    {
		#region BindablePropert List

		public static readonly BindableProperty DirectionProperty =
			BindableProperty.Create(nameof(Direction), typeof(SwipeDirection), typeof(NavigationItem), null, BindingMode.TwoWay);

		public SwipeDirection Direction
		{
			get { return (SwipeDirection)GetValue(DirectionProperty); }
			set { SetValue(DirectionProperty, value); }
		}

		public static readonly BindableProperty CommandProperty =
			BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(NavigationItem), null, BindingMode.TwoWay);

		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		public static readonly BindableProperty CommandParameterProperty =
			BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(NavigationItem), null, BindingMode.TwoWay);

		public object CommandParameter
		{
			get { return (object)GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		public static readonly BindableProperty GlyphProperty =
			BindableProperty.Create(nameof(Glyph), typeof(string), typeof(NavigationItem), null, BindingMode.TwoWay);

		public string Glyph
		{
			get { return (string)GetValue(GlyphProperty); }
			set { SetValue(GlyphProperty, value); }
		}

		public static readonly BindableProperty FontFamilyProperty =
			BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(NavigationItem), null, BindingMode.TwoWay);

		public string FontFamily
		{
			get { return (string)GetValue(FontFamilyProperty); }
			set { SetValue(FontFamilyProperty, value); }
		}

		public static readonly BindableProperty ColorProperty =
			BindableProperty.Create(nameof(Color), typeof(Color), typeof(NavigationItem), null, BindingMode.TwoWay);

		public Color Color
		{
			get { return (Color)GetValue(ColorProperty); }
			set { SetValue(ColorProperty, value); }
		}

		#endregion BindablePropert List

		public VerticalSwipeBar()
        {
            InitializeComponent();
        }
    }
}