﻿using PruebaDoctus.ViewModel;
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
	public partial class TipListView : ContentPage
	{
		TipListViewModel VM;
		public TipListView()
		{
			InitializeComponent();
			VM = new TipListViewModel(Navigation);
			BindingContext = VM;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			VM.LoadDataCommand.Execute(string.Empty);
		}
	}
}