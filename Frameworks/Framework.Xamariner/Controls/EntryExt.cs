using Xamarin.Forms;

namespace Framework.Xamariner.Controls
{
    /// <summary>
    /// https://stackoverflow.com/questions/37822668/how-to-change-border-color-of-entry-in-xamarin-forms
    /// </summary>
    public class EntryExt : Entry
    {
        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(int), typeof(EntryExt), 1, BindingMode.TwoWay);
        public int BorderWidth
        {
            get { return (int)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(EntryExt), Color.Transparent, BindingMode.TwoWay);
        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(EntryExt), (float)10.0f, BindingMode.TwoWay);
        public float CornerRadius
        {
            get { return (float)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
    }
}

