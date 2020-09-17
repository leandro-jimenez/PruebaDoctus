using PruebaDoctus.ViewModel;
using PruebaDoctus.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PruebaDoctus.Services
{
	class NavigationService : INavigationService
	{
		public BaseViewModel PreviousViewModel {
			get
			{
				var mainPage = Application.Current.MainPage as CustomNavigationView;
				var viewModel = mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2].BindingContext;
				return viewModel as BaseViewModel;
			}
		}

		public Task InitializeAsync()
		{
			return NavigateToAsync<TipListViewModel>();
		}

		public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
		{
			throw new NotImplementedException();
		}

		public async Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
		{
			Page page = CreatePage(typeof(TViewModel), parameter);
			var navigationPage = Application.Current.MainPage as CustomNavigationView;
			if (navigationPage != null)
			{
				await navigationPage.PushAsync(page);
			}
			else
			{
				Application.Current.MainPage = new CustomNavigationView(page);
			}

			await(page.BindingContext as BaseViewModel).InitializeAsync(parameter);
		}

		public Task RemoveBackStackAsync()
		{
			var mainPage = Application.Current.MainPage as CustomNavigationView;

			if (mainPage != null)
			{
				for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
				{
					var page = mainPage.Navigation.NavigationStack[i];
					mainPage.Navigation.RemovePage(page);
				}
			}

			return Task.FromResult(true);
		}

		public Task RemoveLastFromBackStackAsync()
		{
			var mainPage = Application.Current.MainPage as CustomNavigationView;

			if (mainPage != null)
			{
				mainPage.Navigation.RemovePage(
					mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
			}

			return Task.FromResult(true);
		}

		private Page CreatePage(Type viewModelType, object parameter)
		{
			Type pageType = GetPageTypeForViewModel(viewModelType);
			if (pageType == null)
			{
				throw new Exception($"Cannot locate page type for {viewModelType}");
			}

			Page page = Activator.CreateInstance(pageType) as Page;
			return page;
		}

		private Type GetPageTypeForViewModel(Type viewModelType)
		{
			var viewName = viewModelType.FullName.Replace("Model", string.Empty);
			var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
			var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
			var viewType = Type.GetType(viewAssemblyName);
			return viewType;
		}
	}
}
