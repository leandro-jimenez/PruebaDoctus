using PruebaDoctus.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PruebaDoctus.ViewModel
{
	class TipViewModel : BaseViewModel
	{
		private string _id;
		public string Id {
			get { return _id; }
			set { _id = value; OnPropertyChanged(); }
		}

		private string _date;

		public string Date {
			get { return _date; }
			set { _date = value; OnPropertyChanged(); }
		}

		private string _title;

		public string Title {
			get { return _title; }
			set { _title = value; OnPropertyChanged(); }
		}

		private string _description;
		public string Description { 
			get { return _description; }
			set { _description = value; OnPropertyChanged(); }
		}

		public ICommand EditTipCommand { private set; get; }
		public ICommand GetTipCommand { private set; get; }

		public TipViewModel()
		{ 
		
		}

		public TipViewModel(string tipId) {
			_id = tipId;
			
			EditTipCommand = new Command<Type>(async (pageType) => await EditNewTip(pageType));
			GetTipCommand = new Command<string>(async (Id) => await LoadDataById(Id));

		}

		async Task EditNewTip(Type pageType)
		{
			
			var page = (Page)Activator.CreateInstance(pageType);
			page.Title = "Editar Tip";
			page.BindingContext = new CreateTipViewModel(this.GetTip());
			await this.pushPage(page);
		}

		async Task LoadDataById(string tip_id)
		{
			var tip = await App.Context.GetItemAsync<Tip>(tip_id);
			Tip tempTip = tip as Tip;
			Title = tempTip.Title;
			Description = tempTip.Description;
			Date = tempTip.Date;
		}
		public Tip GetTip()
		{
			return new Tip()
			{
				Id = this.Id,
				Date = this.Date,
				Title = this.Title,
				Description = this.Description
			};
		}
	}
}
