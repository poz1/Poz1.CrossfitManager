using System;
using Poz1.UpdatingNutrition.CustomControl;
using Poz1.UpdatingNutrition.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TintableButton), typeof(TintableButtonCustomRenderer))]
namespace Poz1.UpdatingNutrition.iOS
{
    public class TintableButtonCustomRenderer: ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            var control = e.NewElement as TintableButton;

            if (control != null)
            {
                if (control.TintColor != Xamarin.Forms.Color.Default)
                {
                    var androidColor = control.TintColor.ToUIColor();
                    Control.BackgroundColor = androidColor;
                }
            }
        }
    }
}


