using System;
using Xamarin.Forms;

namespace Poz1.UpdatingNutrition.CustomControl
{
    public class TintableButton : Button
    {
        public static readonly BindableProperty TintColorProperty = BindableProperty.Create("TintColor", typeof(Color), typeof(Button), (object)Color.Default, BindingMode.OneWay, (BindableProperty.ValidateValueDelegate)null, (BindableProperty.BindingPropertyChangedDelegate)null, (BindableProperty.BindingPropertyChangingDelegate)null, (BindableProperty.CoerceValueDelegate)null, (BindableProperty.CreateDefaultValueDelegate)null);

        public Color TintColor
        {
            get
            {
                return (Color)GetValue(TintColorProperty);
            }
            set
            {
                SetValue(TintColorProperty, value);
            }
        }
    }
}
