using PruebaDoctus.Services;
using PruebaDoctus.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PruebaDoctus.ViewModel
{
	public class BaseViewModel : ExtendedBindableObject//, INotifyPropertyChanged
	{
        protected readonly INavigationService NavigationService;
        /*public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs((propertyName)));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }
            storage = value;
            OnPropertyChanged(propertyName);

            return true;
        }

        protected async Task popPage() {
            await(Application.Current.MainPage as NavigationPage).PopAsync();
        }

        protected async Task pushPage(Page page)
        {
            await (Application.Current.MainPage as NavigationPage).PushAsync(page);
        }*/

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}
