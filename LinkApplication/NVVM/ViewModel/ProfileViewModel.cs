using LinkApplication;
using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.NVVM.Model;
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
        public Dictionary<string, string> dataPerson = new Dictionary<string, string>();
        Database_Connecter _connecter;

        //Tijdelijke opslag van account gegevens voor weergave.
        public static string NameProfile { get; set; }
        public static string AgeProfile { get; set; }
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


        public ProfileViewModel(INavigationService navService)
        {
            
            //Daadwerkelijk maken van command
            Navigation = navService;

            

            NavigateToLoginPageCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<LoginViewModel>(); LogOut(); }, canExecute: CanExecuteCommand);


        }

        private bool CanExecuteCommand(Object obj)
        {
            return true;
        }

        //functie om alle tijdelijke opgeslagen account gegevens terug te zetten
        private void LogOut()
        {
            Account.user_ID = 0;
            NameProfile = string.Empty;
            AgeProfile = string.Empty;
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

            HomePageViewModel.count = 0;

        }








    }
}
