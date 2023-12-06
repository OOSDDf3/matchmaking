using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.NVVM.View;
using LinkApplicationGraphics.Services;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LinkApplicationGraphics.NVVM.ViewModel
{
    public class RegisterViewModel : Core.ViewModel
    {
        //tijdelijke opslag voor gegevens, zodat ik deze kan checken.
        public string NameProfile { get; set; }
        public string EmailProfile { get; set; }
        public string AgeProfile { get; set; }
        public string StreetProfile { get; set; }
        public string PostalCodeProfile { get; set; }
        public string GenderProfile { get; set; }
        public string LanguageProfile { get; set; }
        public bool IsMale {  get; set; }
        public bool IsFemale { get; set; }
        public bool IsOther { get; set; }

        //Objecten voor alle errormessages, deze staan gebind aan de kleur van de textBox. Zo kan je de textboxes rood maken als deze nog niet zijn ingevuld
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

        private string _errorMessageName;
        public string ErrorMessageName
        {
            get => _errorMessageName;
            set
            {
                _errorMessageName = value;
                OnPropertyChanged(nameof(ErrorMessageName));
            }
        }

        private string _errorMessageEmail;
        public string ErrorMessageEmail
        {
            get => _errorMessageEmail;
            set
            {
                _errorMessageEmail = value;
                OnPropertyChanged(nameof(ErrorMessageEmail));
            }
        }

        private string _errorMessageAge;
        public string ErrorMessageAge
        {
            get => _errorMessageAge;
            set
            {
                _errorMessageAge = value;
                OnPropertyChanged(nameof(ErrorMessageAge));
            }
        }
        private string _errorMessageStreet;
        public string ErrorMessageStreet
        {
            get => _errorMessageStreet;
            set
            {
                _errorMessageStreet = value;
                OnPropertyChanged(nameof(ErrorMessageStreet));
            }
        }
        private string _errorMessagePostalCode;
        public string ErrorMessagePostalCode
        {
            get => _errorMessagePostalCode;
            set
            {
                _errorMessagePostalCode = value;
                OnPropertyChanged(nameof(ErrorMessagePostalCode));
            }
        }

        private string _errorMessageGender;
        public string ErrorMessageGender
        {
            get => _errorMessageGender;
            set
            {
                _errorMessageGender = value;
                OnPropertyChanged(nameof(ErrorMessageGender));
            }
        }

        private string _errorMessagePassword;
        public string ErrorMessagePassword
        {
            get => _errorMessagePassword;
            set
            {
                _errorMessagePassword = value;
                OnPropertyChanged(nameof(ErrorMessagePassword));
            }
        } 
        private string _errorMessagePasswordToShort;
        public string ErrorMessagePasswordToShort
        {
            get => _errorMessagePasswordToShort;
            set
            {
                _errorMessagePasswordToShort = value;
                OnPropertyChanged(nameof(ErrorMessagePasswordToShort));
            }
        }


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

        public RelayCommand NavigateToInterestCommand { get; set; }

        public RelayCommand NavigateToBasePageCommand {  get; set; }

        public RelayCommand NavigateToLoginPageCommand { get; set; }
        

        public RegisterViewModel(INavigationService navService)
        {
            //code voor de commmands die uitgevoerd worden binnen de xaml knop
            Navigation = navService;
            NavigateToInterestCommand = new RelayCommand(execute: RegisterCheck, canExecute: o=> true);
            NavigateToLoginPageCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<LoginViewModel>(); ResetErrorMessage(); }, canExecute: o => true);


        }
        //Methode voor het resetten van de errormessages. Zodat als je opniew de pagina opent je weer opnieuw begint
        private void ResetErrorMessage()
        {
            ErrorMessage = "";
            ErrorMessageName = "";
            ErrorMessageAge = "";
            ErrorMessageEmail = "";
            ErrorMessageStreet = "";
            ErrorMessagePostalCode = "";
            ErrorMessageGender = "";
            ErrorMessagePassword = "";

        }

        private void RegisterCheck(Object parameter)
        {
            //Code om password te binden
            PasswordBox? passwordBox = parameter as PasswordBox;
            string clearTextPassword = passwordBox.Password;

            //gaat elke methode langs, deze checkt of hij leeg staat. Zo ja zet hij de foutmelding voor die desbetreffende textBox om hem rood te maken
            bool hasError = false;
            hasError |= CheckNaam();
            hasError |= CheckEmail();
            hasError |= CheckAge();
            hasError |= CheckStreet();
            hasError |= CheckPostalCode();
            hasError |= CheckGender();
            hasError |= passwordBox.Password.IsNullOrEmpty();
            hasError |= passwordBox.Password.Length < 3;

            //Checkt of ingevulde password leeg is en of hij langer is dan 5 karakters.
            if (passwordBox.Password.Length < 5 && passwordBox.Password.Length > 1) { ErrorMessagePasswordToShort = "Het wachtwoord moet langer zijn dan 5 karakters"; ErrorMessagePassword = "Password"; } 
            if(passwordBox.Password.IsNullOrEmpty()) { ErrorMessagePassword = "Password"; }
            if(passwordBox.Password.Length > 5 && !passwordBox.Password.IsNullOrEmpty())
            {
                ErrorMessagePasswordToShort = "";
                ErrorMessagePassword = "";
            }

            //Checkt hier of alle velden zijn ingevuld, zo niet krijg je een error message. Zo wel gaat hij door naar de interesse pagina
            if (hasError)
            {
                ErrorMessage = $"Een aantal gegevens ontbreken, vul deze in!";
            }
            else

            {
                Navigation.NavigateTo<InterestViewModel>();
                ResetErrorMessage();
            }
        }

        //hieronder de methodes voor het checken van requirements die we stellen
        private bool CheckNaam()
        {
            if (NameProfile.IsNullOrEmpty())
            {
                ErrorMessageName = "Name";
                return true;
            }
            else
            {
                ErrorMessageName = "";
                return false;
            }
                
        }

        private bool CheckAge()
        {
            if (AgeProfile.IsNullOrEmpty())
            {
                ErrorMessageAge = "Age";
                return true;
            }
            else
            {
                ErrorMessageAge = "";
                return false;
            }

        }
        private bool CheckEmail()
        {
            if (EmailProfile.IsNullOrEmpty())
            {
                ErrorMessageEmail = "Email";
                return true;
            }
            else
            {
                ErrorMessageEmail = "";
                return false;
            }
        }

        private bool CheckStreet()
        {
            if (string.IsNullOrEmpty(StreetProfile))
            {
                ErrorMessageStreet= "Address";
                return true;
            }
            else
            {
                ErrorMessageStreet ="";
                return false;
            }
        }        
        private bool CheckPostalCode()
        {
            if (string.IsNullOrEmpty(PostalCodeProfile))
            {
                ErrorMessagePostalCode = "Address";
                return true;
            }
            else
            {
                ErrorMessagePostalCode ="";
                return false;
            }
        }

        private bool CheckGender()
        {
            if (!IsMale && !IsFemale && !IsOther )
            {
                ErrorMessageGender = "❗";
                return true;
            }
            else
            {
                ErrorMessageGender = "";
                return false;
            }
        }    
    }
}
