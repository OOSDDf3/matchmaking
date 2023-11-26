using LinkApplication;
using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.NVVM.View;
using LinkApplicationGraphics.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LinkApplicationGraphics.NVVM.ViewModel
{
    public class MainViewModel : Core.ViewModel
    {
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

        public RelayCommand NavigateToRegisterCommand { get; set; }
        public RelayCommand NavigateToHomePageCommand { get; set; }

        public RelayCommand NavigateToLoginPageCommand { get; set; }




        public MainViewModel(INavigationService navService) 
        { 
            Navigation = navService;
            NavigateToRegisterCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<RegisterViewModel>(); }, canExecute: o => true);
            NavigateToHomePageCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<HomePageViewModel>(); }, canExecute: CanExecuteNavigateToHomePage);
            NavigateToLoginPageCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<LoginViewModel>(); }, canExecute: o => true);

            //CurrentView = new LoginView();


        }

        //private UserControl _currentView;
        //public UserControl CurrentView
        //{
        //    get => _currentView;
        //    set
        //    {
        //        _currentView = value;
        //        OnPropertyChanged(nameof(CurrentView));
        //    }
        //}

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
                // Notify command about the change
            }
        }

        private SecureString _password;
        public SecureString Password
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
            _connecter = new Database_Connecter();



            if (_connecter.CheckLogin(Email, "Hallo123!"))
            {
                
                return true;

            }

            else return false;
        }
    }
}
