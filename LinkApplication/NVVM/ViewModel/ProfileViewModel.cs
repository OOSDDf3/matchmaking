using LinkApplication;
using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.NVVM.Model;
using LinkApplicationGraphics.NVVM.View;
using LinkApplicationGraphics.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LinkApplicationGraphics.NVVM.ViewModel
{
    class ProfileViewModel : Core.ViewModel
    {
        Database_Connecter _connecter;

        //Tijdelijke opslag van account gegevens voor weergave.
        public static string NameProfile { get; set; }
        public static string BirthdateProfile { get; set; }
        public static string AddressProfile { get; set; }
        public static string GenderProfile { get; set; }
        public static string LanguageProfile { get; set; }
        public static string EmailProfile { get; set; }
        public static string PasswordProfile { get; set; }


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
        //Command voor navigeren naar login page, uitvoering staat in xaml bij de button
        public RelayCommand NavigateToLoginPageCommand { get; set; }
        public RelayCommand NavigateToHomePageCommand { get; set; }
        public RelayCommand NavigateToProfileViewCommand { get; set; }


        public ProfileViewModel(INavigationService navService)
        {
            
            //Daadwerkelijk maken van command
            Navigation = navService;

            

            NavigateToLoginPageCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<LoginViewModel>(); LogOut(); }, canExecute: CanExecuteCommand);
            NavigateToHomePageCommand = new RelayCommand(execute: Opslaan, canExecute: CanExecuteCommand);
            NavigateToProfileViewCommand = new RelayCommand(execute: o => { Account.showUserInfo(); Navigation.NavigateToNew<ProfileViewModel>();  }, canExecute: o => true);



        }

        private bool CanExecuteCommand(Object obj)
        {
            return true;
        }

        private void Opslaan(Object obj)
        {
            if (ProfileView.magDoor)
            {
                Navigation.NavigateToNew<HomeViewModel>();
                ErrorMessage = "";
            }
            else
            {
                ErrorMessage = "Het E-mail voldoet niet aan het standaard format en het wachtwoord moet minimaal 5 tekens lang zijn";
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



        //functie om alle tijdelijke opgeslagen account gegevens terug te zetten
        private void LogOut()
        {
            Account.user_ID = 0;
            NameProfile = string.Empty;
            BirthdateProfile = string.Empty;
            AddressProfile = string.Empty;
            GenderProfile = string.Empty;
            LanguageProfile = string.Empty;
            EmailProfile = string.Empty;
            PasswordProfile = string.Empty;

            Account.NameProfile = string.Empty;
            Account.AgeProfile = string.Empty;
            Account.AddressProfile = string.Empty;
            Account.GenderProfile = string.Empty;
            Account.LanguageProfile = string.Empty;
            Account.PasswordProfile = string.Empty;

        }








    }
}
