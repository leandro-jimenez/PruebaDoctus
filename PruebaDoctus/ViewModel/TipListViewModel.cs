
using PruebaDoctus.Model;
using PruebaDoctus.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Acr.UserDialogs;

namespace PruebaDoctus.ViewModel
{
	class TipListViewModel : BaseViewModel
	{
		private ObservableCollection<Tip> _tipsList;
		public ObservableCollection<Tip> TipsList {
			get { return _tipsList; }
			set { _tipsList = value; OnPropertyChanged(); }
		}
		
		private TipViewModel _selectedTip;
		public TipViewModel SelectedTip {
			get { return _selectedTip; }
			set { _selectedTip = value; OnPropertyChanged(); }
		}

		public ICommand LoadDataCommand { private set; get; }
		public ICommand GoToDetailsCommand { private set; get; }
		public ICommand AddNewTipCommand { private set; get; }
		public ICommand DeleteTipCommand { private set; get; }
		public INavigation Navigation { get; set; }

		public TipListViewModel(INavigation navigation) {
			Navigation = navigation;
			LoadDataCommand = new Command<string>(async (name) => await LoadData(name));
			AddNewTipCommand = new Command<Type>(async (pageType) => await AddNewTip(pageType));
			GoToDetailsCommand = new Command<Tip>(async (tip) => await DetailsTip(tip));
			DeleteTipCommand = new Command<Tip>(async (tip) => await DeleteTip(tip));

		}
		async Task LoadData(string name)
		{
			TipsList = new ObservableCollection<Tip>();
			var tips = string.IsNullOrWhiteSpace(name)
				? await App.Context.GetItemsAsync<Tip>()
				: await App.Context.FilterItemsAsync<Tip>("Tip", $"Title LIKE '%{name}%'");

			foreach (var item in tips)
				TipsList.Add(item);
		}

		

		async Task AddNewTip(Type pageType)
		{
			SelectedTip = null;

			var page = (Page)Activator.CreateInstance(pageType);
			page.BindingContext = new CreateTipViewModel(new Tip());
			await Navigation.PushAsync(page);
		}

		async Task DeleteTip(Tip tipToDelete) {
			var confirm = await UserDialogs.Instance.ConfirmAsync("", "Confirmar eliminación del tip", "Si", "No");
			
			if (confirm) { 
				var progress = UserDialogs.Instance.Progress("Eliminando...");
				progress.Show();
				progress.PercentComplete = 0;

				await App.Context.DeleteItemAsync<Tip>(tipToDelete);
				progress.Title = "Actualizando...";
				progress.PercentComplete = 50;
				await LoadData(String.Empty);
				progress.PercentComplete = 100;
				progress.Hide();
			}

		}

		async Task DetailsTip(Tip tip) {
			var page = new TipView();
			page.BindingContext = new TipViewModel(tip.Id);
			await Navigation.PushAsync(page);
		}
	}
}
