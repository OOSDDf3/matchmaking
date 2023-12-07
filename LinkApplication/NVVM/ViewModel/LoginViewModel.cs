using LinkApplication;
using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.NVVM.Model;
using LinkApplicationGraphics.NVVM.View;
using LinkApplicationGraphics.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LinkApplicationGraphics.NVVM.ViewModel
{
    public class LoginViewModel : Core.ViewModel
    {
        public Database_Connecter _connecter;
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

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
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


            NavigateToRegisterCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<RegisterViewModel>(); ErrorMessage = "";  }, canExecute: o => true);
            NavigateToHomePageCommand = new RelayCommand(execute: Login, canExecute: o => true);
        }   


        //functie om te checken of account gegevns kloppen. Als hij niet doorkomt geeft hij een errormessage. Anders stuurt hij je door naar de main pagina.
        public void Login(object parameter)
        {
            _connecter = new Database_Connecter();
            PasswordBox passwordBox = parameter as PasswordBox;
            string clearTextPassword = passwordBox.Password;
            string hashedPassword = PasswordHasher.HashPassword(clearTextPassword);

            if (Email.IsNullOrEmpty() || clearTextPassword.IsNullOrEmpty())
            {
                ErrorMessage = "Email adres of wachtwoord is niet ingevuld";

            }
            else if (_connecter.CheckLogin(Email, hashedPassword, out user_ID))
            {
                ev_OnLoginSuccesfull.Invoke(this, new LoginEventargs(user_ID));
                ErrorMessage = "";
                Navigation.NavigateTo<HomePageViewModel>();
                Navigation.NavigateToNew<HomeViewModel>();
                
            }
            else
            {
                ErrorMessage = "Email adres of wachtwoord is verkeerd ingevuld";

            }
        }  
    }
}
