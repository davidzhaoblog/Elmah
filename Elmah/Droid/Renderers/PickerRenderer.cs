using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Picker), typeof(Elmah.Droid.Renderers.PickerRenderer))]
namespace Elmah.Droid.Renderers
{
    /// <summary>
    /// https://stackoverflow.com/questions/37822668/how-to-change-border-color-of-entry-in-xamarin-forms
    /// </summary>
    public class PickerRenderer : Xamarin.Forms.Platform.Android.PickerRenderer
    {
        public PickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(Element.BackgroundColor.ToAndroid());
                gd.SetCornerRadius(10);
                gd.SetStroke(2, Xamarin.Forms.Color.Black.ToAndroid());
                Control.SetBackground(gd);
            }
        }
    }
}

