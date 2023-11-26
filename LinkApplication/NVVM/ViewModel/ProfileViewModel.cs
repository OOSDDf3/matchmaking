using LinkApplication;
using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkApplicationGraphics.NVVM.ViewModel
{
    class ProfileViewModel : Core.ViewModel
    {
        public Dictionary<string, string> dataPerson = new Dictionary<string, string>();
        Database_Connecter _connecter;

        public string NameProfile { get; set; }
        public string AgeProfile { get; set; }
        public string AddressProfile { get; set; }
        public string GenderProfile { get; set; }
        public string LanguageProfile { get; set; }
        public string EmailProfile { get; set; }
        public string PasswordProfile { get; set; }

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
            _connecter = new Database_Connecter();

            //Code voor ophalen informatie user en inzettend naar de pagina
            dataPerson = _connecter.ShowUserInformation("spichon01@hotmail.com", "Hallo123!", "SELECT * FROM Account WHERE email = @email AND password = @password");

            NameProfile = dataPerson["name"];
            AgeProfile = dataPerson["age"];
            AddressProfile = dataPerson["address"];
            GenderProfile = dataPerson["gender"];
            LanguageProfile = dataPerson["language"];
            EmailProfile = dataPerson["email"];
            PasswordProfile = dataPerson["password"];

            //Daadwerkelijk maken van command
            Navigation = navService;

            NavigateToLoginPageCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<LoginViewModel>(); }, canExecute: o => true);


        }




    }
}
