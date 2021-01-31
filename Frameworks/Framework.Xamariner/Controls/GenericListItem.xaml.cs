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
    public partial class GenericListItem : Framework.Xamariner.Controls.ListItemViewBase
    {
		public static readonly BindableProperty MasterContentProperty =
			BindableProperty.Create(nameof(MasterContent), typeof(View), typeof(GenericListItem), null, BindingMode.TwoWay
				, propertyChanged: (sender, oldValue, newValue) => {
					try
					{
						(sender as GenericListItem).MasterContentView.Content = (View)newValue;
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


		public static readonly BindableProperty IconContentProperty =
			BindableProperty.Create(nameof(IconContent), typeof(View), typeof(GenericListItem), null, BindingMode.TwoWay
				, propertyChanged: (sender, oldValue, newValue) => {
					try
					{
						(sender as GenericListItem).IconContentView.Content = (View)newValue;
					}
					catch (Exception)
					{
					}
				});


		public View IconContent
		{
			get { return (View)GetValue(IconContentProperty); }
			set { SetValue(IconContentProperty, value); }
		}

		public static readonly BindableProperty ModeProperty =
            BindableProperty.Create(nameof(Mode), typeof(Framework.Xaml.ListItemViewModes), typeof(GenericListItem), Framework.Xaml.ListItemViewModes.NavigationWhenClickItem, BindingMode.TwoWay
				, propertyChanged: (sender, oldValue, newValue) => {
					var typedSender = (GenericListItem)sender;
					var typedNewMode = (Framework.Xaml.ListItemViewModes)newValue;

					AssignNavigateCommand(typedSender, typedNewMode, typedSender.NavigateCommmand);
				});

        public Framework.Xaml.ListItemViewModes Mode
		{
			get { return (Framework.Xaml.ListItemViewModes)GetValue(ModeProperty); }
			set { SetValue(ModeProperty, value); }
		}

		public static readonly BindableProperty NavigateCommmandProperty =
			BindableProperty.Create(nameof(NavigateCommmand), typeof(ICommand), typeof(GenericListItem), null, BindingMode.TwoWay
				, propertyChanged: (sender, oldValue, newValue) => {
					var typedSender = (GenericListItem)sender;
					var command = (ICommand)newValue;
					AssignNavigateCommand(typedSender, typedSender.Mode, command);
				});

		public ICommand NavigateCommmand
		{
			get { return (ICommand)GetValue(NavigateCommmandProperty); }
			set { SetValue(NavigateCommmandProperty, value); }
		}

		private static void AssignNavigateCommand(GenericListItem typedSender, Framework.Xaml.ListItemViewModes typedNewMode, ICommand command)
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

		public GenericListItem()
        {
            InitializeComponent();
        }
    }
}