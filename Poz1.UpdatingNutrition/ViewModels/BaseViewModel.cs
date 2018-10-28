using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using Poz1.UpdatingNutrition.Models;
using Poz1.UpdatingNutrition.Services;
using Poz1.UpdatingNutrition.Mvvm;
using Poz1.UpdatingNutrition.Interface;

namespace Poz1.UpdatingNutrition.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged, IViewModel
    {
        public ViewModelNavigation Navigation { get; set; }

        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>() ?? new MockDataStore();
        public IUserStore<User> UserStore => DependencyService.Get<IUserStore<User>>() ?? new UserDataStore();
        public IDataStore<WOD> WODStore => DependencyService.Get<IDataStore<WOD>>() ?? new WODStore();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
