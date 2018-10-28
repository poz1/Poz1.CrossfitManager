using System;
using System.Collections.Generic;
using Poz1.UpdatingNutrition.ViewModels;
using Xamarin.Forms;

namespace Poz1.UpdatingNutrition.Views
{
    public partial class NewWODPage : ContentPage
    {
        public NewWODPage()
        {
            InitializeComponent();
            ToolbarItems.Add(new ToolbarItem("Add Ex", "Plus.png", async () =>
            { var vm = this.BindingContext as NewWODViewModel;
                vm.Exs.Add(new Models.Exercise() { Name = "Jumping Jack" });
            }));

        }
    }
}
