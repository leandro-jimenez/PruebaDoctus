using PruebaDoctus.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PruebaDoctus.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TipView : ContentPage
	{
		public TipView()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			var VM = this.BindingContext as TipViewModel;
			VM.GetTipCommand.Execute(VM.Id);
		}
	}
}