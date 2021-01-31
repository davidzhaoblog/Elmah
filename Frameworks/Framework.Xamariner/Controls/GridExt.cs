using Xamarin.Forms;

namespace Framework.Xamariner.Controls
{
    public class GridExt : Grid, Framework.Xamariner.Controls.IIsSelected
    {
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(GridExt), false, BindingMode.TwoWay);
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
    }
}

