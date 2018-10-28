using System;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Poz1.UpdatingNutrition.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var entries = new[]
            {
                new Microcharts.Entry(200)
                {
                    Label = "Push Jerk",
                    ValueLabel = "200",
                    Color = SKColor.Parse("#5D4037")
                },
                new Microcharts.Entry(400)
                {
                Label = "Air Squat",
                ValueLabel = "400",
                Color = SKColor.Parse("#68B9C0")
                },
                new Microcharts.Entry(250)
                {
                Label = "Push Press",
                ValueLabel = "250",
                Color = SKColor.Parse("#90D585")
                },
                new Microcharts.Entry(500)
                {
                Label = "Jumping Jack",
                ValueLabel = "500",
                Color = SKColor.Parse("#90D585")
                }
            };
            this.chart.Chart = new RadialGaugeChart() { Entries = entries, LabelTextSize = 16 };

            var entries2 = new[]
            {
                new Microcharts.Entry(320)
                {
                    Label = "Bayron",
                    ValueLabel = "320",
                    Color = SKColor.Parse("#4CAF50")
                },
                new Microcharts.Entry(400)
                {
                Label = "Thomas",
                ValueLabel = "400",
                Color = SKColor.Parse("#0288D1")
                },
                new Microcharts.Entry(350)
                {
                Label = "Alessandro",
                ValueLabel = "350",
                Color = SKColor.Parse("#90D585")
                }
            };
            this.chart2.Chart = new LineChart() { Entries = entries2, LabelTextSize = 16 };
        }
    }
}