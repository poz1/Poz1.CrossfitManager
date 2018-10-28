using Xamarin.Forms;

namespace Poz1.UpdatingNutrition.CustomControl
{
	public class TappableLabel : Label
	{
		private TapGestureRecognizer tapRecognizer;
		public static readonly BindableProperty TappedCommandProperty = BindableProperty.Create("TappedCommand", typeof(Command), typeof(TappableLabel), null);

		public Command TappedCommand
		{
			get { return (Command)GetValue(TappedCommandProperty); }
			set { SetValue(TappedCommandProperty, value); }
		}

		public TappableLabel()
		{
			tapRecognizer = new TapGestureRecognizer();
			tapRecognizer.Tapped += (s, e) => { TappedCommand?.Execute(null); };
			GestureRecognizers.Add(tapRecognizer);
		}
	}
}
