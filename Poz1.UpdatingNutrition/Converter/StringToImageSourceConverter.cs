using System;
using Xamarin.Forms;

namespace Poz1.UpdatingNutrition.Converter
{
	public class StringToImageSourceConverter: IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
			{
				return null;
			}

			if (Uri.IsWellFormedUriString(value as string, UriKind.Absolute))
				return ImageSource.FromUri(new Uri(value as string));
			else
				return ImageSource.FromResource(value as string);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;
		}
	}
}
