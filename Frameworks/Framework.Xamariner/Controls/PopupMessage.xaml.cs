using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Framework.Xamariner.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopupMessage : ContentView
	{
        public static readonly BindableProperty MessageProperty = BindableProperty.Create(nameof(Message), typeof(string), typeof(PopupMessage), null, BindingMode.OneWay);
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly BindableProperty ShowCommandButtonsProperty = BindableProperty.Create(nameof(ShowCommandButtons), typeof(bool), typeof(PopupMessage), true, BindingMode.OneWay);
        public bool ShowCommandButtons
        {
            get { return (bool)GetValue(ShowCommandButtonsProperty); }
            set { SetValue(ShowCommandButtonsProperty, value); }
        }

        public static readonly BindableProperty ShowActionSheetProperty =
            BindableProperty.Create(nameof(ShowActionSheet), typeof(bool), typeof(PopupMessage), null, BindingMode.TwoWay);

        public bool ShowActionSheet
        {
            get { return (bool)GetValue(ShowActionSheetProperty); }
            set { SetValue(ShowActionSheetProperty, value); }
        }

        public static readonly BindableProperty CloseCommandProperty = BindableProperty.Create(nameof(CloseCommand), typeof(ICommand), typeof(PopupMessage), null, BindingMode.OneWay);
        public ICommand CloseCommand
        {
            get { return (ICommand)GetValue(CloseCommandProperty); }
            set { SetValue(CloseCommandProperty, value); }
        }

        public PopupMessage ()
		{
			InitializeComponent ();

            //btnClose.Clicked += BtnClose_Clicked;
		}

        private void BtnClose_Clicked(object sender, System.EventArgs e)
        {
            //if(CloseCommand.CanExecute(null))
            //{
            //    CloseCommand.Execute(null);
            //}
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if(propertyName == MessageProperty.PropertyName)
            {
                lblMessage.Text = Message;
            }
            //else if(propertyName == ShowCommandButtonsProperty.PropertyName)
            //{
            //    gridCommandButtons.IsVisible = ShowCommandButtons;
            //}
        }
    }
}