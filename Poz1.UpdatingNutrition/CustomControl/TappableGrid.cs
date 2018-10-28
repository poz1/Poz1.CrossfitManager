using Xamarin.Forms;

namespace Poz1.UpdatingNutrition.CustomControl
{
    class TappableGrid : Grid
    {
        private TapGestureRecognizer tapRecognizer;
        public static readonly BindableProperty TappedCommandProperty = BindableProperty.Create("TappedCommand", typeof(Command), typeof(TappableGrid), null);

        public Command TappedCommand
        {
            get { return (Command)GetValue(TappedCommandProperty); }
            set { SetValue(TappedCommandProperty, value); }
        }

        public TappableGrid()
        {
            tapRecognizer = new TapGestureRecognizer();
            tapRecognizer.Tapped += (s, e) => { TappedCommand?.Execute(null);};
            GestureRecognizers.Add(tapRecognizer);
        }
    }
}
