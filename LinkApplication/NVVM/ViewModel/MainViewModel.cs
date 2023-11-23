using LinkApplication;
using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LinkApplicationGraphics.NVVM.ViewModel
{
    public class MainViewModel : Core.ViewModel
    {
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


        public MainViewModel(INavigationService navService) 
        { 
            Navigation = navService;
            NavigateToRegisterCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<RegisterViewModel>(); }, canExecute: o => true);
            NavigateToHomePageCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<HomePageViewModel>(); }, canExecute: CanExecuteNavigateToHomePage);


        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                

                // Notify command about the change
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
               
            }
        }

        private bool CanExecuteNavigateToHomePage(object obj)
        {
            if (Email == "spichon01@hotmail.com" && Password == "Samuel")
            {
                return true;

            }
            else return false;
        }
    }
}
