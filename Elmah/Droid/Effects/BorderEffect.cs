using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Android.Widget;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("UsingValidationSample")]
[assembly: ExportEffect(typeof(Elmah.Droid.Effects.BorderEffect), "BorderEffect")]
namespace Elmah.Droid.Effects
{
    /// <summary>
    /// This file/Code is copied from:
    /// https://devblogs.microsoft.com/premier-developer/validate-input-in-xamarin-forms-using-inotifydataerrorinfo-custom-behaviors-effects-and-prism/
    /// https://github.com/davidezordan/UsingValidation
    /// </summary>
    public class BorderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                if (Element is Entry)
                {
                    GradientDrawable gd = new GradientDrawable();
                    gd.SetColor(((Entry)Element).BackgroundColor.ToAndroid());
                    //gd.SetCornerRadius(((EntryExt)Element).CornerRadius);
                    //gd.SetStroke(5, Xamarin.Forms.Color.Red.ToAndroid());
                    Control.SetBackground(gd);
                }
                else if (Element is Framework.Xamariner.Controls.EntryExt)
                {
                    GradientDrawable gd = new GradientDrawable();
                    gd.SetColor(((Framework.Xamariner.Controls.EntryExt)Element).BackgroundColor.ToAndroid());
                    //gd.SetCornerRadius(((Framework.Xamariner.Controls.EntryExt)Element).CornerRadius);
                    //gd.SetStroke(5, Xamarin.Forms.Color.Red.ToAndroid());
                    Control.SetBackground(gd);
                }
                else if (Element is Picker)
                {
                    GradientDrawable gd = new GradientDrawable();
                    gd.SetColor(((Picker)Element).BackgroundColor.ToAndroid());
                    //gd.SetCornerRadius(((EntryExt)Element).CornerRadius);
                    //gd.SetStroke(5, Xamarin.Forms.Color.Red.ToAndroid());
                    Control.SetBackground(gd);
                }
            }
            catch (Exception)
            {
            }
        }

        protected override void OnDetached()
        {
            try
            {
                if(Element is Entry)
                {
                    GradientDrawable gd = new GradientDrawable();
                    gd.SetColor(((Entry)Element).BackgroundColor.ToAndroid());
                    Control.SetBackground(gd);
                }
                else if (Element is Framework.Xamariner.Controls.EntryExt)
                {
                    GradientDrawable gd = new GradientDrawable();
                    gd.SetColor(((Framework.Xamariner.Controls.EntryExt)Element).BackgroundColor.ToAndroid());
                    gd.SetCornerRadius(((Framework.Xamariner.Controls.EntryExt)Element).CornerRadius);
                    gd.SetStroke(((Framework.Xamariner.Controls.EntryExt)Element).BorderWidth, ((Framework.Xamariner.Controls.EntryExt)Element).BorderColor.ToAndroid());
                    Control.SetBackground(gd);
                }
                else if (Element is Picker)
                {
                    GradientDrawable gd = new GradientDrawable();
                    gd.SetColor(((Picker)Element).BackgroundColor.ToAndroid());
                    gd.SetCornerRadius(10);
                    gd.SetStroke(2, Xamarin.Forms.Color.Black.ToAndroid());
                    Control.SetBackground(gd);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}

