using LinkApplication;
using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.NVVM.View;
using LinkApplicationGraphics.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LinkApplicationGraphics.NVVM.ViewModel
{
    class LoginViewModel : Core.ViewModel
    {
        Database_Connecter _connecter;
        Account_Info _account;
        public event EventHandler<string> EmailChanged;


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


        public LoginViewModel(INavigationService navService)
        {
            

            Navigation = navService;
            NavigateToRegisterCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<RegisterViewModel>(); }, canExecute: o => true);
            NavigateToHomePageCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<HomePageViewModel>(); }, canExecute: CanExecuteNavigateToHomePage);
        }   


        private bool CanExecuteNavigateToHomePage(object obj)
        {
            _connecter = new Database_Connecter();
            _account = new Account_Info();



            if (_connecter.CheckLogin("spichon01@Hotmail.com", "Hallo123!"))
            {
                
                return true;

            }

            else
            {
               ;

                return true;
            }
        }
    }

}
