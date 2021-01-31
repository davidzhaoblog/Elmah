using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Framework.Xamariner.Controls.EntryExt), typeof(Elmah.Droid.Renderers.EntryExtRenderer))]
namespace Elmah.Droid.Renderers
{
    /// <summary>
    /// https://stackoverflow.com/questions/37822668/how-to-change-border-color-of-entry-in-xamarin-forms
    /// </summary>
    public class EntryExtRenderer : EntryRenderer
    {
        public EntryExtRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(Element.BackgroundColor.ToAndroid());
                gd.SetCornerRadius(((Framework.Xamariner.Controls.EntryExt)Element).CornerRadius);
                gd.SetStroke(((Framework.Xamariner.Controls.EntryExt)Element).BorderWidth, ((Framework.Xamariner.Controls.EntryExt)Element).BorderColor.ToAndroid());
                Control.SetBackground(gd);
            }
        }
    }
}

