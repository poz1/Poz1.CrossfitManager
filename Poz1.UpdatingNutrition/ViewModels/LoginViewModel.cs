using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Acr.UserDialogs;

namespace Poz1.UpdatingNutrition.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        public LoginViewModel()
        {
            Title = "Crossfit Manager";
            LoginCommand = new Command(async () => await Login());
            RegisterCommand = new Command(async () => await NavigateToRegisterView());
            RegisterEnabled = true;

        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        private string email;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }


        bool registerEnabled;
        public bool RegisterEnabled
        {
            get { return registerEnabled; }
            set { SetProperty(ref registerEnabled, value); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        private async Task Login()
        {
            RegisterEnabled = false;
            //App.IsRegisteringWithFacebook = false;

            try
            {
                IsBusy = true;
                var res = await UserStore.Login(new Models.pwd() { Email = Email, Password = Password });
                //await App.ApiService.Login(Email, Password);

                //App.CredentialService.SaveCredentials(Email, Password);

                //CrossSettings.Current.AddOrUpdateValue(nameof(Email), Email);
                if (res)
                {
                    var user = (await UserStore.GetItemsAsync()).GetEnumerator();
                    while (user.Current.Email != Email)
                        user.MoveNext();

                    App.mainUser = user.Current;
                    await NavigateToMainView();
                }
                else
                    throw new Exception("Wrong Username or Password!");
            }
            catch (Exception e)
            {
                //if(e.Message.Contains("You must have a confirmed email"))
                //  await UserDialogs.Instance.AlertAsync(PagitaResources.RegistrationMailSent, PagitaResources.Error, PagitaResources.Ok);
                //else
                await UserDialogs.Instance.AlertAsync(e.Message, "Error", "Ok");
            }
            finally
            {
                IsBusy = false;
                RegisterEnabled = true;
            }
        }

        private async Task NavigateToMainView()
        {
            await Navigation.PopModalAsync();
        }

        private async Task NavigateToRegisterView()
        {
            await Navigation.PushAsync<RegisterViewModel>();
        }
    }
}
