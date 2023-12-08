using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.NVVM.View;
using LinkApplicationGraphics.NVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkApplicationGraphics.Services
{
    public interface INavigationService 
    { 
        public ViewModel CurrentView { get; }
        public ViewModel NewView { get; }

        void NavigateTo<T>() where T : ViewModel;
        void NavigateToNew<T>() where T : ViewModel;
    }


    public class NavigationService : ObservableObject, INavigationService
    {
        private ViewModel _currentView;
        private ViewModel _newView;
        private readonly Func<Type, ViewModel> _viewModelFactory;


        public ViewModel NewView
        {
            get => _newView;
            private set
            {
                _newView = value;
                OnPropertyChanged();
            }
        }
        public ViewModel CurrentView
        {
            get => _currentView;
            private set
            {
                _currentView = value;
                OnPropertyChanged();
            }

        }
        public NavigationService(Func<Type, ViewModel> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }


        public void NavigateTo<TViewModel>() where TViewModel : ViewModel
        {

            ViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentView = viewModel;
            
        }
        public void NavigateToNew<TViewModel>() where TViewModel : ViewModel
        {

            ViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            
            NewView = viewModel;

        }
    }
}
