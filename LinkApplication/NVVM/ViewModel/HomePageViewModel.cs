using LinkApplication;
using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkApplicationGraphics.NVVM.ViewModel
{
    class HomePageViewModel : Core.ViewModel
    {
        MainWindow mainWindow;

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


        public HomePageViewModel(INavigationService navService)
        {
            Navigation = navService;
            NavigateToHomeViewCommand = new RelayCommand(execute: o => { Navigation.NavigateToNew<HomeViewModel>(); }, canExecute: o => true);
            NavigateToProfileViewCommand = new RelayCommand(execute: o => { Navigation.NavigateToNew<ProfileViewModel>(); }, canExecute: o => true);

        }


    }
}
