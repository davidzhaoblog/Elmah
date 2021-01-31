using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Windows.Input;

namespace Framework.Xamariner.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GenericListItemWithFontIcon : Framework.Xamariner.Controls.ListItemViewBase
    {
		public static readonly BindableProperty MasterContentProperty =
			BindableProperty.Create(nameof(MasterContent), typeof(View), typeof(GenericListItemWithFontIcon), null, BindingMode.TwoWay
				, propertyChanged: (sender, oldValue, newValue) => {
					try
					{
						(sender as GenericListItemWithFontIcon).MasterContentView.Content = (View)newValue;
					}
					catch (Exception)
					{
					}
				});

		public View MasterContent
		{
			get { return (View)GetValue(MasterContentProperty); }
			set { SetValue(MasterContentProperty, value); }
		}

		public static readonly BindableProperty ModeProperty =
            BindableProperty.Create(nameof(Mode), typeof(Framework.Xaml.ListItemViewModes), typeof(GenericListItemWithFontIcon), Framework.Xaml.ListItemViewModes.NavigationWhenClickItem, BindingMode.TwoWay
				, propertyChanged: (sender, oldValue, newValue) => {
					var typedSender = (GenericListItemWithFontIcon)sender;
					var typedNewMode = (Framework.Xaml.ListItemViewModes)newValue;

					AssignNavigateCommand(typedSender, typedNewMode, typedSender.NavigateCommmand);
				});

        public Framework.Xaml.ListItemViewModes Mode
		{
			get { return (Framework.Xaml.ListItemViewModes)GetValue(ModeProperty); }
			set { SetValue(ModeProperty, value); }
		}

		public static readonly BindableProperty NavigateCommmandProperty =
			BindableProperty.Create(nameof(NavigateCommmand), typeof(ICommand), typeof(GenericListItemWithFontIcon), null, BindingMode.TwoWay
				, propertyChanged: (sender, oldValue, newValue) => {
					var typedSender = (GenericListItemWithFontIcon)sender;
					var command = (ICommand)newValue;
					AssignNavigateCommand(typedSender, typedSender.Mode, command);
				});

		public ICommand NavigateCommmand
		{
			get { return (ICommand)GetValue(NavigateCommmandProperty); }
			set { SetValue(NavigateCommmandProperty, value); }
		}

		private static void AssignNavigateCommand(GenericListItemWithFontIcon typedSender, Framework.Xaml.ListItemViewModes typedNewMode, ICommand command)
		{
			if (command != null)
			{
				var tapGestureRecognizer = new TapGestureRecognizer();
				tapGestureRecognizer.Command = command;
				tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandParameterProperty, ".");

				if (typedNewMode == Framework.Xaml.ListItemViewModes.SingleSelection || typedNewMode == Framework.Xaml.ListItemViewModes.NavigationWhenRightArrow)
				{
					typedSender.RightArrow.IsVisible = true;
					typedSender.RightArrow.GestureRecognizers.Add(tapGestureRecognizer);
				}
				else
				{
					typedSender.RightArrow.IsVisible = false;
					typedSender.GestureRecognizers.Add(tapGestureRecognizer);
				}
			}
		}

		public static readonly BindableProperty MasterFontIconProperty =
			BindableProperty.Create(nameof(MasterFontIcon), typeof(string), typeof(GenericListItemWithFontIcon), null, BindingMode.TwoWay);

		public string MasterFontIcon
		{
			get { return (string)GetValue(MasterFontIconProperty); }
			set { SetValue(MasterFontIconProperty, value); }
		}

		public static readonly BindableProperty MasterFontIconFamilyProperty =
			BindableProperty.Create(nameof(MasterFontIconFamily), typeof(string), typeof(GenericListItemWithFontIcon), null, BindingMode.TwoWay);

		public string MasterFontIconFamily
		{
			get { return (string)GetValue(MasterFontIconFamilyProperty); }
			set { SetValue(MasterFontIconFamilyProperty, value); }
		}

		public GenericListItemWithFontIcon()
        {
            InitializeComponent();
        }
    }
}