using PruebaDoctus.Services;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using TinyIoC;
using Xamarin.Forms;


namespace PruebaDoctus.ViewModel.Base
{
	class ViewModelLocator
	{
		private static Container ioCContainer;

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        => (bool)bindable.GetValue(AutoWireViewModelProperty);

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
            => bindable.SetValue(AutoWireViewModelProperty, value);

        
        public static bool UseMockService { get; set; }

        static ViewModelLocator()
        {
            ioCContainer = new Container();

            ioCContainer.Register<TipListViewModel>();
            ioCContainer.Register<CreateTipViewModel>();
            ioCContainer.Register<TipViewModel>();

            ioCContainer.Register<INavigationService, NavigationService>();
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;

            var viewType = view?.GetType();
            if (viewType?.FullName == null)
            {
                return;
            }

            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }

            var viewModel = Container.GetInstance(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}
