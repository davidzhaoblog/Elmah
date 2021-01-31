using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Framework.Xamariner.Effects;

namespace Framework.Xamariner.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FontIcon : ContentView
    {
		#region BindablePropert List

		public static readonly BindableProperty MasterFontIconProperty =
			BindableProperty.Create(nameof(MasterFontIcon), typeof(string), typeof(FontIcon), null, BindingMode.TwoWay);

		public string MasterFontIcon
		{
			get { return (string)GetValue(MasterFontIconProperty); }
			set { SetValue(MasterFontIconProperty, value); }
		}

		public static readonly BindableProperty MasterFontIconFamilyProperty =
			BindableProperty.Create(nameof(MasterFontIconFamily), typeof(string), typeof(FontIcon), null, BindingMode.TwoWay
				, propertyChanged: (sender, oldValue, newValue) => {
					string fontFamilyKey = string.Empty;
					if (newValue != null)
						fontFamilyKey = newValue.ToString();
					else if (oldValue != null)
						fontFamilyKey = oldValue.ToString();

					if (!string.IsNullOrEmpty(fontFamilyKey) && Application.Current.Resources.ContainsKey(fontFamilyKey))
					{
						var control = (FontIcon)sender;
						control.MasterFontIconLabel.FontFamily = (OnPlatform<string>)Application.Current.Resources[fontFamilyKey];
					}
				});

		public string MasterFontIconFamily
		{
			get { return (string)GetValue(MasterFontIconFamilyProperty); }
			set { SetValue(MasterFontIconFamilyProperty, value); }
		}


		public static readonly BindableProperty MasterFontIconSizeProperty =
			BindableProperty.Create(nameof(MasterFontIconSize), typeof(double), typeof(FontIcon), 30.0d, BindingMode.TwoWay);

		public double MasterFontIconSize
		{
			get { return (double)GetValue(MasterFontIconSizeProperty); }
			set { SetValue(MasterFontIconSizeProperty, value); }
		}

		public static readonly BindableProperty SubFontIconProperty =
			BindableProperty.Create(nameof(SubFontIcon), typeof(string), typeof(FontIcon), null, BindingMode.TwoWay);

		public string SubFontIcon
		{
			get { return (string)GetValue(SubFontIconProperty); }
			set { SetValue(SubFontIconProperty, value); }
		}

		public static readonly BindableProperty SubFontIconFamilyProperty =
			BindableProperty.Create(nameof(SubFontIconFamily), typeof(string), typeof(FontIcon), null, BindingMode.TwoWay
				, propertyChanged: (sender, oldValue, newValue) => {
					string fontFamilyKey = string.Empty;
					if (newValue != null)
						fontFamilyKey = newValue.ToString();
					else if (oldValue != null)
						fontFamilyKey = oldValue.ToString();

					if (!string.IsNullOrEmpty(fontFamilyKey) && Application.Current.Resources.ContainsKey(fontFamilyKey))
					{
						var control = (FontIcon)sender;
						control.SubFontIconLabel.FontFamily = (OnPlatform<string>)Application.Current.Resources[fontFamilyKey];
					}
				});

		public string SubFontIconFamily
		{
			get { return (string)GetValue(SubFontIconFamilyProperty); }
			set { SetValue(SubFontIconFamilyProperty, value); }
		}

		public static readonly BindableProperty SubFontIconSizeProperty =
			BindableProperty.Create(nameof(SubFontIconSize), typeof(double), typeof(FontIcon), 12.0d, BindingMode.TwoWay);

		public double SubFontIconSize
		{
			get { return (double)GetValue(SubFontIconSizeProperty); }
			set { SetValue(SubFontIconSizeProperty, value); SubFontIconBackgroundSize = SubFontIconSize + 3; }
		}

		public static readonly BindableProperty SubFontIconBackgroundSizeProperty =
			BindableProperty.Create(nameof(SubFontIconBackgroundSize), typeof(double), typeof(FontIcon), 15.0d, BindingMode.TwoWay);

		public double SubFontIconBackgroundSize
		{
			get { return (double)GetValue(SubFontIconBackgroundSizeProperty); }
			set { SetValue(SubFontIconBackgroundSizeProperty, value); }
		}

		public static readonly BindableProperty InfoFontIconProperty =
			BindableProperty.Create(nameof(InfoFontIcon), typeof(string), typeof(FontIcon), null, BindingMode.TwoWay);

		public string InfoFontIcon
		{
			get { return (string)GetValue(InfoFontIconProperty); }
			set { SetValue(InfoFontIconProperty, value); }
		}

		public static readonly BindableProperty InfoFontIconFamilyProperty =
			BindableProperty.Create(nameof(InfoFontIconFamily), typeof(string), typeof(FontIcon), null, BindingMode.TwoWay
				, propertyChanged: (sender, oldValue, newValue) => {
					string fontFamilyKey = string.Empty;
					if (newValue != null)
						fontFamilyKey = newValue.ToString();
					else if (oldValue != null)
						fontFamilyKey = oldValue.ToString();

					if (!string.IsNullOrEmpty(fontFamilyKey) && Application.Current.Resources.ContainsKey(fontFamilyKey))
					{
						var control = (FontIcon)sender;
						control.InfoFontIconLabel.FontFamily = (OnPlatform<string>)Application.Current.Resources[fontFamilyKey];
					}
				});

		public string InfoFontIconFamily
		{
			get { return (string)GetValue(InfoFontIconFamilyProperty); }
			set { SetValue(InfoFontIconFamilyProperty, value); }
		}

		public static readonly BindableProperty InfoFontIconSizeProperty =
			BindableProperty.Create(nameof(InfoFontIconSize), typeof(double), typeof(FontIcon), 12.0d, BindingMode.TwoWay);

		public double InfoFontIconSize
		{
			get { return (double)GetValue(InfoFontIconSizeProperty); }
			set { SetValue(InfoFontIconSizeProperty, value); InfoFontIconBackgroundSize = InfoFontIconSize + 3; }
		}

		public static readonly BindableProperty InfoFontIconBackgroundSizeProperty =
			BindableProperty.Create(nameof(InfoFontIconBackgroundSize), typeof(double), typeof(FontIcon), 15.0d, BindingMode.TwoWay);

		public double InfoFontIconBackgroundSize
		{
			get { return (double)GetValue(InfoFontIconBackgroundSizeProperty); }
			set { SetValue(InfoFontIconBackgroundSizeProperty, value); }
		}

		public static readonly BindableProperty HasCircleProperty =
			BindableProperty.Create(nameof(HasCircle), typeof(bool), typeof(FontIcon), false, BindingMode.TwoWay);

		public bool HasCircle
		{
			get { return (bool)GetValue(HasCircleProperty); }
			set { SetValue(HasCircleProperty, value); }
		}

		public static readonly BindableProperty CircleColorProperty =
			BindableProperty.Create(nameof(CircleColor), typeof(Color), typeof(FontIcon), null, BindingMode.TwoWay);

		public Color CircleColor
		{
			get { return (Color)GetValue(CircleColorProperty); }
			set { SetValue(CircleColorProperty, value); }
		}

		#endregion BindablePropert List

		public FontIcon()
        {
            InitializeComponent();
        }
    }
}