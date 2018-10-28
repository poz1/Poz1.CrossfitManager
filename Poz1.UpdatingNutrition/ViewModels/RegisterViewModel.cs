using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Poz1.UpdatingNutrition.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {

        bool isCoach;
        public bool IsCoach
        {
            get { return isCoach; }
            set { SetProperty(ref isCoach, value); }
        }

        List<string> sexList;
        public List<string> SexList
        {
            get { return sexList; }
            set { SetProperty(ref sexList, value); }
        }

        List<string> ageList;
        public List<string> AgeList
        {
            get { return ageList; }
            set { SetProperty(ref ageList, value); }
        }

        List<string> weightList;
        public List<string> WeightList
        {
            get { return weightList; }
            set { SetProperty(ref weightList, value); }
        }

        List<string> heightList;
        public List<string> HeightList
        {
            get { return heightList; }
            set { SetProperty(ref heightList, value); }
        }

        string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        string surname;
        public string Surname
        {
            get { return surname; }
            set { SetProperty(ref surname, value); }
        }
        string email;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        string sex;
        public string Sex
        {
            get { return sex; }
            set { SetProperty(ref sex, value); }
        }

        string age = "12";
        public string Age
        {
            get { return age; }
            set { SetProperty(ref age, value); }
        }

        string weight = "30";
        public string Weight
        {
            get { return weight; }
            set { SetProperty(ref weight, value); }
        }
        string height = "120";

        public string Height
        {
            get { return height; }
            set { SetProperty(ref height, value); }
        }

        public RegisterViewModel()
        {
            RegisterCommand = new Command(async () => await NavigateToRegisterView());
            this.Title = "New User";
            this.SexList = new List<string>() { "Male", "Female" };
            this.ageList = new List<string>();
            this.weightList = new List<string>();
            this.heightList = new List<string>();

            for (int year = 12; year < 99; year++)
                this.ageList.Add(year.ToString());

            for (int weight = 30; weight < 250; weight++)
                this.weightList.Add(weight.ToString());

            for (int height = 120; height < 250; height++)
                this.heightList.Add(height.ToString());
        }

        public ICommand RegisterCommand { get; }


        private async Task NavigateToRegisterView()
        {
            IsBusy = true;
            var user = new Models.User()
            {
                Email = Email,
                Name = Name,
                Surname = Surname,
                Password = Password,

                Sex = Sex,
                Age = int.Parse(Age),
                Weight = int.Parse(Weight),
                Height = int.Parse(Height),
                IsCoach = IsCoach


            };
            await UserStore.AddItemAsync(user);
            App.mainUser = user;
            App.IsCoach = IsCoach;
            await Navigation.PushModalAsync<HomeViewModel>();
        }
    }
}
