using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.NVVM.Model;
using LinkApplicationGraphics.NVVM.View;
using LinkApplicationGraphics.Services;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private string _errorMessageAgeNotANumber;
        public string ErrorMessageAgeNotANumber
        {
            get => _errorMessageAgeNotANumber;
            set
            {
                _errorMessageAgeNotANumber = value;
                OnPropertyChanged(nameof(ErrorMessageAgeNotANumber));
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
            NavigateToLoginPageCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<LoginViewModel>(); ResetErrorMessage(); LogOut(); }, canExecute: o => true);


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
            ErrorMessagePasswordToShort = "";

        }

        private void LogOut()
        {
            Account.user_ID = 0;

            Account.NameProfile = string.Empty;
            Account.BirthdateProfile = string.Empty;
            Account.AddressProfile = string.Empty;
            Account.GenderProfile = string.Empty;
            Account.LanguageProfile = string.Empty;
            Account.PasswordProfile = string.Empty;

            NameProfile = string.Empty;
            AgeProfile = string.Empty;
            StreetProfile = string.Empty;
            PostalCodeProfile = string.Empty;
            GenderProfile = string.Empty;
            LanguageProfile = string.Empty;
            EmailProfile = string.Empty;

            IsFemale = false;
            IsMale = false;
            IsOther = false;

        }

        private void RegisterCheck(Object parameter)
        {
            //Code om password te binden
            PasswordBox? passwordBox = parameter as PasswordBox;
            string clearTextPassword = passwordBox.Password;

            

            //gaat elke methode langs, deze checkt of hij leeg staat. Zo ja zet hij de foutmelding voor die desbetreffende textBox om hem rood te maken
            bool hasErrorEmpty = false;
            hasErrorEmpty |= CheckNaam();
            hasErrorEmpty |= CheckEmail();
            hasErrorEmpty |= CheckAge();
            hasErrorEmpty |= CheckStreet();
            hasErrorEmpty |= CheckPostalCode();
            hasErrorEmpty |= CheckGender();
            hasErrorEmpty |= CheckPassword(clearTextPassword);

            //Checkt hier of alle velden zijn ingevuld, zo niet krijg je een error message. Zo wel gaat hij door naar de interesse pagina
            if (hasErrorEmpty)
            {
                ErrorMessage = $"Een aantal gegevens kloppen niet, vul deze in!";
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
            if (!EmailProfile.IsNullOrEmpty())
            {
                if (Regex.IsMatch(EmailProfile, @"^[a-zA-Z0-9._%+-]*@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                {
                    ErrorMessageEmail = "";
                    return false;
                }
                else
                {
                    ErrorMessageEmail = "Email";
                    return true;
                }
            }
            else
            {

                ErrorMessageEmail = "Email"; 
                return true;
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
                ErrorMessageGender = "Gender";
                return true;
            }
            else
            {
                ErrorMessageGender = "";
                return false;
            }
        } 
        
        private bool CheckPassword(String password) 
        {
            if (password.Length < 5 && password.Length > 0) 
            { 
                ErrorMessagePasswordToShort = "Het wachtwoord moet langer zijn dan 5 karakters"; ErrorMessagePassword = "Password"; 
                return true;
            }

            if (password.IsNullOrEmpty()) { 
                ErrorMessagePassword = "Password"; 
                return true;
            }



            if (password.Length > 5 && !password.IsNullOrEmpty())
            {
                ErrorMessagePasswordToShort = "";
                ErrorMessagePassword = "";
                return false;
            }
            return false;
        }
    }
}
