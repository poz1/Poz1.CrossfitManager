using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Poz1.UpdatingNutrition.Models;
using Xamarin.Forms;

namespace Poz1.UpdatingNutrition.ViewModels
{
    public class TargetViewModel : BaseViewModel
    {
        WOD upWOD;
        public WOD UpWOD
        {
            get { return upWOD; }
            set { SetProperty(ref upWOD, value); }
        }



        ObservableCollection<WOD> wods;
        public ObservableCollection<WOD> WODS
        {
            get { return wods; }
            set { SetProperty(ref wods, value); }
        }


        ObservableCollection<Exercise> exs;
        public ObservableCollection<Exercise> Exs
        {
            get { return exs; }
            set { SetProperty(ref exs, value); }
        }

        int ca;
        public int Ca
        {
            get { return ca; }
            set { SetProperty(ref ca, value); }
        }

        bool sSP;
        public bool SSP
        {
            get { return sSP; }
            set { SetProperty(ref sSP, value); }
        }

        DateTime dt;
        public DateTime Dt
        {
            get { return dt; }
            set { SetProperty(ref dt, value); }
        }


        public TargetViewModel()
        {
            Title = "Workout Of the Day";
            WODS = new ObservableCollection<WOD>();
            Exs = new ObservableCollection<Exercise>();
            DwnOWD();
            RefreshCommand = new Command(async () => await DwnOWD());


        }

        public ICommand RefreshCommand { get; }


        public async Task DwnOWD(){
            IsBusy = true;
            WODS.Clear();
            Exs.Clear();
            SSP = false;

            try
            {
                var t = (await WODStore.GetItemsAsync(true)).GetEnumerator();
                if (t != null)
                {
                    while (t.MoveNext())
                    {
                        if (t.Current.Date < DateTime.Now)
                            WODS.Add(t.Current);
                        else
                        {
                            upWOD = t.Current;
                            SSP = true;
                        }
                    }
                }

                if (upWOD != null)
                {
                    if (upWOD.Exercises != null)
                        foreach (var ite in upWOD.Exercises)
                            Exs.Add(ite);

                    Ca = upWOD.Calories;
                    Dt = upWOD.Date;
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                IsBusy = false;
            }
        } 
    }
}

