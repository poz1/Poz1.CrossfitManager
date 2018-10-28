using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Poz1.UpdatingNutrition.Models;
using Xamarin.Forms;

namespace Poz1.UpdatingNutrition.ViewModels
{
    public class DietViewModel : BaseViewModel
    {
        public DietViewModel()
        {
            this.Title = "Coaching";
            Exs = new ObservableCollection<Comment>();

            Exs.Add(new Comment() { Date = new DateTime(2018, 10, 27), Content = "A Beatiful training, I really enjoyed it" });
            Exs.Add(new Comment() { Date = new DateTime(2018, 10, 27), Content = "Very tiring, wasn't expecting so much effort this time" });


        }



        ObservableCollection<Comment> exs;
        public ObservableCollection<Comment> Exs
        {
            get { return exs; }
            set { SetProperty(ref exs, value); }
        }


    }
}
