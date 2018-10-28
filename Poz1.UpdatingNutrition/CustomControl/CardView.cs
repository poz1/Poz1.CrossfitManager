using Xamarin.Forms;

namespace Poz1.UpdatingNutrition.CustomControl
{
    public class CardView : Frame
    {
        public CardView()
        {
            Padding = 0;
            HasShadow = true;
            
            if (Device.OS == TargetPlatform.iOS)
            {
                HasShadow = false;
                OutlineColor = Color.Transparent;
                BackgroundColor = Color.Transparent;
            }
        }
    }
}

