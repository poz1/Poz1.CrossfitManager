using System;
using Urho;
using Urho.Forms;
using Xamarin.Forms;

namespace Poz1.UpdatingNutrition.Views
{
    public class ModelPage : ContentPage
    {

        UrhoSurface urhoSurface;
        Gym urhoApp;
        Slider selectedBarSlider;
        ActivityIndicator loading;
        public ModelPage()
        {
            var restartBtn = new Button { Text = "Run!" };
            restartBtn.Clicked += (sender, e) => urhoApp?.Jump();
            var resetBtn = new Button { Text = "Reset!" };
            resetBtn.Clicked += (sender, e) => urhoApp?.Reset();
            urhoSurface = new UrhoSurface();
            urhoSurface.VerticalOptions = LayoutOptions.FillAndExpand;
            urhoSurface.BackgroundColor = Xamarin.Forms.Color.Transparent;

            //urhoSurface.IsVisible = false;
            //loading = new ActivityIndicator(){IsEnabled = true, IsRunning = true, IsVisible = true};
            //var grid = new Grid();
            //grid.Children.Add(urhoSurface);
            //grid.Children.Add(loading);

                Title = "Jumping Jacks";
            Content = new StackLayout
            {
                Padding = new Thickness(12, 12, 12, 12),
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    urhoSurface,
                    restartBtn,
                    resetBtn,
                    new Label() {Text = "Physical jumping exercise performed by jumping to a position with the legs spread wide and the hands touching overhead, sometimes in a clap, and then returning to a position with the feet together and the arms at the sides." }
                }
            };
        }

        protected override void OnDisappearing()
        {
            UrhoSurface.OnDestroy();
            base.OnDisappearing();
        }

        void OnValuesSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            //if (urhoApp?.SelectedBar != null)
                //urhoApp.SelectedBar.Value = (float)e.NewValue;
        }

        private void OnBarSelection(Bar bar)
        {
            //reset value
            selectedBarSlider.ValueChanged -= OnValuesSliderValueChanged;
            selectedBarSlider.Value = bar.Value;
            selectedBarSlider.ValueChanged += OnValuesSliderValueChanged;
        }

        protected override async void OnAppearing()
        {
            StartUrhoApp();
        }

        async void StartUrhoApp()
        {
            urhoApp = await urhoSurface.Show<Gym>(new ApplicationOptions(assetsFolder: "Ninja") { Orientation = ApplicationOptions.OrientationType.Portrait });
            //urhoApp.Ready += (sender, e) => { urhoSurface.IsVisible = true; loading.IsVisible = false; };

        }
    }
}

