using LinkApplication;
using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.NVVM.Model;
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



        public InterestViewModel(INavigationService navService)
        {
            Navigation = navService;
            NavigateToRegisterCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<RegisterViewModel>(); }, canExecute: o => true);
            NavigateToHomePageCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<HomePageViewModel>(); }, canExecute: CanExecuteNavigateToHomePage);
            NavigateToLoginPageCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<LoginViewModel>();createAccount(); }, canExecute: CanExecuteNavigateToLoginPage);
        }

        private bool CanExecuteNavigateToHomePage(object obj)
        {
            

           
            return true;
        }   
        private bool CanExecuteNavigateToLoginPage(object obj)
        {
            

           
            return true;
        }

        private void createAccount()
        {
            _connecter = new Database_Connecter();

            _connecter.InsertAccount(Account.NameProfile, Account.EmailProfile, Account.PasswordProfile, Int32.Parse(Account.AgeProfile), Account.AddressProfile, Account.GenderProfile , "Dutch");
            _connecter.InsertIntoUserInterestList(_connecter.getUserID(Account.EmailProfile, Account.PasswordProfile), Account.InterestsProfile , Account.ProfilePicture);


        }

 
    }
}
