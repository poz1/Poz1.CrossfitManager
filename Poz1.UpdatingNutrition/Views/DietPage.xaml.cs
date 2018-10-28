using System;
using System.Collections.Generic;
using System.Diagnostics;
using Poz1.UpdatingNutrition.Mvvm;
using Poz1.UpdatingNutrition.ViewModels;
using Xamarin.Forms;

namespace Poz1.UpdatingNutrition.Views
{
    public partial class DietPage : ContentPage
    {
        public DietPage()
        {
            InitializeComponent();
            this.listview.ItemSelected += (sender, e) => listview.SelectedItem = null;

            ToolbarItems.Add(new ToolbarItem("New WOD", "Plus.png", async () => 
            {
                Navigation.PushAsync(ViewFactory.GetPage<NewWODViewModel>() as NewWODPage); 
                }));

        }
    }
}
