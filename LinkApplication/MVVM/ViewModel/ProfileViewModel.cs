using LinkApplication;
using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.NVVM.Model;
using LinkApplicationGraphics.NVVM.View;
using LinkApplicationGraphics.Services;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace LinkApplicationGraphics.NVVM.ViewModel
{
    public class ProfileViewModel : Core.ViewModel
    {
        public Dictionary<string, string> dataPerson = new Dictionary<string, string>();
        Database_Connecter _connecter;

        //Tijdelijke opslag van account gegevens voor weergave.
        public static string NameProfile { get; set; }
        public static string BirthdateProfile { get; set; }
        public static string AddressProfile { get; set; }
        public static string GenderProfile { get; set; }
        public static string LanguageProfile { get; set; }
        public static string EmailProfile { get; set; }
        public static string PasswordProfile { get; set; }
        public static string InterestsProfileString { get; set; }
        public static BitmapImage ProfilePictureImage { get; set; }




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
        public RelayCommand NavigateToPasswordChangePageCommand { get; set; }
        public RelayCommand NavigateToEditInterestPageCommand { get; set; }

        public RelayCommand DeleteAccountCommand { get; set; }


        public ProfileViewModel(INavigationService navService)
        {

            //Daadwerkelijk maken van command
            Navigation = navService;



            NavigateToLoginPageCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<LoginViewModel>(); LogOut(); }, canExecute: o => true);
            NavigateToHomePageCommand = new RelayCommand(execute: Opslaan, canExecute: o => true);
            NavigateToPasswordChangePageCommand = new RelayCommand(execute: o => { Navigation.NavigateToNew<PasswordChangeViewModel>(); }, canExecute: o => true);
            NavigateToEditInterestPageCommand = new RelayCommand(execute: o => { Navigation.NavigateToNew<EditInterestViewModel>(); }, canExecute: o => true);

            //voor verwijderen van account
            DeleteAccountCommand = new RelayCommand(execute: o => { DeleteAccount(); }, canExecute: o => true);


        }

        private void Opslaan(Object obj)
        {
            if (ProfileView.isAccept)
            {
                Navigation.NavigateToNew<MatchingViewModel>();
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
        private void DeleteAccount()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _connecter = new Database_Connecter();
                _connecter.DeleteUser(Account.user_ID);
                LogOut();

                Navigation.NavigateTo<LoginViewModel>();

            }
            else
            {
                // User clicked No or closed the dialog
                // Do nothing or handle the cancellation
            }
        }


        //functie om alle tijdelijke opgeslagen account gegevens terug te zetten
        private void LogOut()
        {
            Account.user_ID = 0;
            Account.userMatches.Clear();

            NameProfile = string.Empty;
            BirthdateProfile = string.Empty;
            AddressProfile = string.Empty;
            GenderProfile = string.Empty;
            LanguageProfile = string.Empty;
            EmailProfile = string.Empty;
            PasswordProfile = string.Empty;
            InterestsProfileString = string.Empty;

            Account.NameProfile = string.Empty;
            Account.BirthdateProfile = string.Empty;
            Account.AddressProfile = string.Empty;
            Account.GenderProfile = string.Empty;
            Account.LanguageProfile = string.Empty;
            Account.PasswordProfile = string.Empty;
            Account.InterestsProfileString = string.Empty;

            MatchesViewModel.LogOut();
        }
    }
}
