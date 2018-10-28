using System;
using Android.Graphics;
using Poz1.UpdatingNutrition.CustomControl;
using Poz1.UpdatingNutrition.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(TintableButton), typeof(TintableButtonRenderer))]
namespace Poz1.UpdatingNutrition.Droid

{
    public class TintableButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            var control = e.NewElement as TintableButton;
            if (control != null)
            {
                if (control.TintColor != Xamarin.Forms.Color.Default)
                {
                    var androidColor = control.TintColor.ToAndroid();
                    Control.Background.SetColorFilter(androidColor, PorterDuff.Mode.Src);
                }
            }
        }
    }
}

