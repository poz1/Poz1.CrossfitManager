using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Poz1.UpdatingNutrition.Services;
using Poz1.UpdatingNutrition.Views;
using Poz1.UpdatingNutrition.Mvvm;
using Poz1.UpdatingNutrition.ViewModels;
using Poz1.UpdatingNutrition.Models;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Poz1.UpdatingNutrition
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        public static string AzureBackendUrl = "https://updatingnutrition.azurewebsites.net/";
        public static bool UseMockDataStore = false;
        public static User mainUser { get; set; }
        public static bool IsCoach { get; set; }

        public App()
        {
            RegisterViewModels();
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
            {
                DependencyService.Register<AzureDataStore>();
                DependencyService.Register<UserDataStore>();
            }

            MainPage = new NavigationPage(ViewFactory.GetPage<LoginViewModel>() as LoginPage);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void RegisterViewModels()
        {
            ViewFactory.Register<LoginPage, LoginViewModel>();
            ViewFactory.Register<RegisterPage, RegisterViewModel>();
            ViewFactory.Register<HomePage, HomeViewModel>();
            ViewFactory.Register<MainPage, MainViewModel>();
            ViewFactory.Register<TargetPage, TargetViewModel>();
            ViewFactory.Register<DietPage, DietViewModel>();
            ViewFactory.Register<ModelPage, ModelViewModel>();
            ViewFactory.Register<NewWODPage, NewWODViewModel>();
            //ViewFactory.Register<PromotionsPage, PromotionsViewModel>();
            //ViewFactory.Register<PromotionDetailPage, PromotionDetailViewModel>();
            //ViewFactory.Register<PrivacyPage, PrivacyViewModel>();
            //ViewFactory.Register<ContactsPage, ContactsViewModel>();
        }
    }
}
