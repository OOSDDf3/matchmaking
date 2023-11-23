using LinkApplication;
using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LinkApplicationGraphics.NVVM.ViewModel
{
    public class InterestViewModel : Core.ViewModel
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

        public RelayCommand NavigateToRegisterCommand { get; set; }
        public RelayCommand NavigateToHomePageCommand { get; set; }



        public InterestViewModel(INavigationService navService)
        {
            Navigation = navService;
            NavigateToRegisterCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<RegisterViewModel>(); }, canExecute: o => true);
            NavigateToHomePageCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<HomePageViewModel>(); }, canExecute: CanExecuteNavigateToHomePage);
        }

        private bool CanExecuteNavigateToHomePage(object obj)
        {
            return true;
        }

    }
}
