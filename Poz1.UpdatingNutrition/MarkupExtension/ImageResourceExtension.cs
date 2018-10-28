using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Poz1.UpdatingNutrition.MarkupExtension
{
	class ImageResourceExtension : IMarkupExtension
    {
		public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
			if (Source == null)
            {
                return null;
            }

			return ImageSource.FromResource(Source);
        }
    }
}