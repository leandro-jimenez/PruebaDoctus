using Acr.UserDialogs;
using PruebaDoctus.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PruebaDoctus.ViewModel
{
	public class CreateTipViewModel : BaseViewModel
	{
		private Tip _tip;
		public Tip Tip {
			get { return _tip; }
			set { _tip = value; OnPropertyChanged(); }
		}

		private DateTime _dateCreated;

		public DateTime DateCreated {
			get { return _dateCreated; }
			set { _dateCreated = value; OnPropertyChanged(); }
		}
		public ICommand SaveCommand { private set; get; }
		public CreateTipViewModel() {
			Tip = new Tip();
			SaveCommand = new Command(async () => await CreateTip());
		}

		public CreateTipViewModel(Tip tip) {
			Tip = tip;
			SaveCommand = new Command(async () => await CreateTip());

		}
		async Task CreateTip()
		{
			var isInsert = false;

			var tipToSave = Tip;
			if (string.IsNullOrWhiteSpace(tipToSave.Id))
			{
				tipToSave.Id = Guid.NewGuid().ToString();
				isInsert = true;
			}

			
			tipToSave.Date = DateCreated.ToString();
			var success = await App.Context.SaveItemAsync<Tip>(tipToSave, isInsert);
			
			await UserDialogs.Instance.AlertAsync((success > 0) ? "Success!" : "Error!", "Saving...", "OK");
			
			if (success > 0)
			{
				await this.popPage();
			}
		}

	}
}
