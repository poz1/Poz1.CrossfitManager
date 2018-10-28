using Poz1.UpdatingNutrition.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Poz1.UpdatingNutrition.Mvvm
{
    public class ViewModelNavigation
    {
        readonly INavigation navigation;

		public IReadOnlyList<Page> NavigationStack 
		{
			get { return navigation.NavigationStack; }
		}

        public ViewModelNavigation(INavigation pageNavigation)
        {
            navigation = pageNavigation;
        }

        public Task PushAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel
        {
            return navigation.PushAsync(ViewFactory.GetPage<TViewModel>(parameter) as Page, true);
        }

        public Task PopAsync()
        {
            return navigation.PopAsync(true);
        }

		public Task PushModalAsync<TViewModel>(bool inNavigationPage = false, object parameter = null,  double[] navigationColor = null) where TViewModel : BaseViewModel
        {
			if (inNavigationPage)
			{
				var nav = new NavigationPage(ViewFactory.GetPage<TViewModel>(parameter) as Page);

				if (navigationColor != null)
				{
					nav.BarBackgroundColor = Color.FromRgb(navigationColor[0], navigationColor[1], navigationColor[2]);
					nav.BarTextColor = Color.White;
				}

				return navigation.PushModalAsync(nav, true);
			}
            else
                return navigation.PushModalAsync(ViewFactory.GetPage<TViewModel>(parameter) as Page, true);
        }
    
        public Task PopModalAsync()
        {
            return navigation.PopModalAsync(true);
        }

        public Task PopToRootAsync()
        {
            return navigation.PopToRootAsync(true);
        }
       
        public async Task RemoveAsync<TViewModel>(TViewModel viewModel)  where TViewModel : BaseViewModel
        {
            foreach (var page in navigation.NavigationStack)
            {
                if (page.BindingContext == viewModel)
                {
                    // If the page is on top of the stack it must be popped first
                    if (navigation.NavigationStack[this.navigation.NavigationStack.Count - 1] == page)
                    {
                        await PopAsync();
                    }

                    // Clear the view model/bindings
                    page.BindingContext = null;

                    // Remove the page from the stack
                    navigation.RemovePage(page);
                    return;
                }
            }
        }

		public void ClearStack() 
		{ 
			foreach (var page in navigation.NavigationStack)
			{
				if(navigation.NavigationStack.Count != 1)
					navigation.RemovePage(page);
			}
		}
    }
}