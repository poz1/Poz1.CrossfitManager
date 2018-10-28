using System;
using System.Collections.Generic;
using Poz1.UpdatingNutrition.ViewModels;
using Xamarin.Forms;

namespace Poz1.UpdatingNutrition.Views
{
    public partial class TargetPage : ContentPage
    {
        public TargetPage()
        {
            InitializeComponent();

            this.listview.ItemSelected+= (sender, e) => listview.SelectedItem = null;

            var tapGestureRecognizer = new TapGestureRecognizer((obj) =>
            {
                Navigation.PushModalAsync(new NavigationPage(new ModelPage()));
            });

            GridGym.GestureRecognizers.Add(tapGestureRecognizer);
        }
    }
}
