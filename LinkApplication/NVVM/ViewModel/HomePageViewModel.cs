using LinkApplication;
using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.NVVM.Model;
using LinkApplicationGraphics.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Effects;

namespace LinkApplicationGraphics.NVVM.ViewModel
{
    class HomePageViewModel : Core.ViewModel
    {
        MainWindow mainWindow;
        public Dictionary<string, string> dataPerson = new Dictionary<string, string>();


        Database_Connecter _connecter;

        

        

        public INavigationService _navigation;
        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand NavigateToHomeViewCommand { get; set; }
        public RelayCommand NavigateToProfileViewCommand { get; set; }
        public RelayCommand NavigateToEventsViewCommand { get; set; }
        public RelayCommand NavigateToMatchesViewCommand { get; set; }


        public HomePageViewModel(INavigationService navService)
        {
            Navigation = navService;
            NavigateToHomeViewCommand = new RelayCommand(execute: o => { Navigation.NavigateToNew<HomeViewModel>(); }, canExecute: o => true);
            NavigateToEventsViewCommand = new RelayCommand(execute: o => { Navigation.NavigateToNew<EventsViewModel>(); }, canExecute: o => true);
            NavigateToMatchesViewCommand = new RelayCommand(execute: o => { Navigation.NavigateToNew<MatchesViewModel>(); }, canExecute: o => true);
            NavigateToProfileViewCommand = new RelayCommand(execute: o => { Navigation.NavigateToNew<ProfileViewModel>(); Account.showUserInfo(); }, canExecute: CanExecuteNavigateToProfile);

        }

        private bool CanExecuteNavigateToProfile(object obj)
        {
            
            return true;
        }



       
    }
}
