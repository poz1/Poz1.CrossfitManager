using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using Poz1.UpdatingNutrition.ViewModels;
using Poz1.UpdatingNutrition.Interface;

namespace Poz1.UpdatingNutrition.Mvvm
{
    public static class ViewFactory
    {
		public static bool CacheEnabled { get; set;}

		private static readonly Dictionary<Type, Type> TypeDictionary = new Dictionary<Type, Type>();
        private static readonly Dictionary<Type, object> ViewModelDictionary = new Dictionary<Type, object>();
        private static readonly Dictionary<Type, object> ViewDictionary = new Dictionary<Type, object>();

        public static void Register<TPage, TViewModel>() where TPage : Page where TViewModel : BaseViewModel
        {
            TypeDictionary[typeof(TViewModel)] = typeof(TPage);
        }

        public static object GetPage<TViewModel>(object parameter = null) where TViewModel : BaseViewModel
        {
            Type viewModelType = typeof(TViewModel);
            Type viewType;

            Page page;
            IViewModel viewModel;

            if (!TypeDictionary.ContainsKey(viewModelType))
                throw new InvalidOperationException("Unknown View for ViewModel");

            viewType = TypeDictionary[viewModelType];

			if (!ViewDictionary.ContainsKey(viewType) || !CacheEnabled)
            {
                page = Activator.CreateInstance(viewType) as Page;
                ViewDictionary[viewType] = page;
            }
            else
                page = ViewDictionary[viewType] as Page;

            if (!ViewModelDictionary.ContainsKey(viewModelType) || !CacheEnabled)
            {
                if(parameter != null)
                    viewModel = Activator.CreateInstance(viewModelType, parameter) as IViewModel;
                else
                    viewModel = Activator.CreateInstance(viewModelType) as IViewModel;

                ViewModelDictionary[viewModelType] = viewModel;
            }
            else
                viewModel = ViewModelDictionary[viewModelType] as IViewModel;

            page.BindingContext = viewModel;
            viewModel.Navigation = new ViewModelNavigation(page.Navigation);

            return page;
        }
    }
}