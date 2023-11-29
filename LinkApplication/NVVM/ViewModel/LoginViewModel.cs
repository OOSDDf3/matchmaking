using LinkApplication;
using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.NVVM.Model;
using LinkApplicationGraphics.NVVM.View;
using LinkApplicationGraphics.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public delegate void dg_Loginsuccesfull(object sender, LoginEventargs e);

        public static event dg_Loginsuccesfull? ev_OnLoginSuccesfull;


        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();

            }
        }

        private String password;
        public String Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();

            }
        }

        private int user_ID;


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

            ev_OnLoginSuccesfull += Account.GetUserID;

            NavigateToRegisterCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<RegisterViewModel>(); }, canExecute: o => true);
            NavigateToHomePageCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<HomePageViewModel>();  }, canExecute: CanExecuteNavigateToHomePage);
        }   


        

        private bool CanExecuteNavigateToHomePage(object parameter)
        {
            _connecter = new Database_Connecter();
            PasswordBox passwordBox = parameter as PasswordBox;
            string clearTextPassword = passwordBox.Password;

            

            if (_connecter.CheckLogin(Email, clearTextPassword, out user_ID))
            {
                Debug.WriteLine(user_ID);
                ev_OnLoginSuccesfull.Invoke(this, new LoginEventargs(user_ID));
                return true;
            }

            else
            {
                return false;
            }
        }
    }

}
