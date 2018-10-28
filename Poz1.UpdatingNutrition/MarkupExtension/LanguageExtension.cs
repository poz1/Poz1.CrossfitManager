//using Pagita.Interface;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace Poz1.UpdatingNutrition.MarkupExtension
{
    class LanguageExtension : IMarkupExtension
    {
        readonly CultureInfo ci;
        const string ResourceId = "Poz1.UpdatingNutrition.Languages";

        public LanguageExtension()
        {
            //if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android)
            //{
            //    //ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            //}
        }

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            //if (Text == null)
            //    return "";

            //ResourceManager resmgr = new ResourceManager(ResourceId, typeof(LanguageExtension).GetTypeInfo().Assembly);

            //var translation = resmgr.GetString(Text, ci);

            //if (translation == null)
            //{
                ////throw new ArgumentException(
                //Debug.WriteLine(String.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Text, ResourceId, ci.Name),"Text");

                //translation = Text; // HACK: returns the key, which GETS DISPLAYED TO THE USER
            //}
            return Text;
        }
    }
}