using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Poz1.UpdatingNutrition.Models;
using Xamarin.Forms;

namespace Poz1.UpdatingNutrition.ViewModels
{
	public class NewWODViewModel:BaseViewModel
    {
        public NewWODViewModel()
        {
            Exs = new ObservableCollection<Exercise>();
            SaveCommand = new Command(async () => await Save());
            DT = DateTime.Now;

        }
        public Command SaveCommand { get; set; }

        ObservableCollection<Exercise> exs;
        public ObservableCollection<Exercise> Exs
        {
            get { return exs; }
            set { SetProperty(ref exs, value); }
        }

        DateTime dt;
        public DateTime DT
        {
            get { return dt; }
            set { SetProperty(ref dt, value); }
        }
        public async Task Save()
        {
            IsBusy = true;
            await WODStore.AddItemAsync(new WOD() { Date = DT, Exercises = Exs.ToList(), Calories = 3200 });
            IsBusy = false;

        }
    }
}

