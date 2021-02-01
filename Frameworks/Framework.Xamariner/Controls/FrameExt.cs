using Xamarin.Forms;

namespace Framework.Xamariner.Controls
{
    public class FrameExt : Frame, Framework.Xamariner.Controls.IIsSelected
    {
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(FrameExt), false, BindingMode.TwoWay);
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
    }
}

