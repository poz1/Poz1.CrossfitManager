using System;
using System.Collections.Generic;
using Poz1.UpdatingNutrition.Mvvm;
using Poz1.UpdatingNutrition.ViewModels;
using Xamarin.Forms;

namespace Poz1.UpdatingNutrition.Views
{
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();

            if(App.IsCoach)
                this.Children.Add(new NavigationPage(ViewFactory.GetPage<DietViewModel>() as DietPage){Title = "Coaching"});

            CurrentPage = Children[1];
        }
    }
}
