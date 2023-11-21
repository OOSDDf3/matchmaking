using LinkApplication.Core;
using LinkApplication.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkApplication.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand InteressesViewCommand { get; set; }
        public RelayCommand ProfileViewCommand { get; set; }
        public HomeViewModel HomeVM { get; set; }
        public InteressesViewModel InteressesVM { get; set; }
        public ProfileViewModel ProfileVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged();    
            }
        }

        public MainViewModel() 
        {
            HomeVM = new HomeViewModel();
            InteressesVM = new InteressesViewModel();
            ProfileVM = new ProfileViewModel();
            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o => 
            {
                CurrentView = HomeVM;
            });

            InteressesViewCommand = new RelayCommand(o =>
            {
                CurrentView = InteressesVM;
            });

            ProfileViewCommand = new RelayCommand(o =>
            {
                CurrentView = ProfileVM;
            });
        }
    }
}
