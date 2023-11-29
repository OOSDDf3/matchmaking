using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.NVVM.View;
using LinkApplicationGraphics.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LinkApplicationGraphics.NVVM.ViewModel
{
    public class RegisterViewModel : Core.ViewModel
    {

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


        public RelayCommand NavigateToInterestCommand { get; set; }

        public RelayCommand NavigateToBasePageCommand {  get; set; }

        public RelayCommand NavigateToLoginPageCommand { get; set; }

        public RegisterViewModel(INavigationService navService)
        {
            Navigation = navService;
            NavigateToInterestCommand = new RelayCommand(execute: RegisterCheck, canExecute: o=> true);
            NavigateToBasePageCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<MainViewModel>(); }, canExecute: o => true);
            NavigateToLoginPageCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<LoginViewModel>(); ErrorMessage = ""; }, canExecute: o => true);

        }


        private void RegisterCheck(Object parameter)
        {
            if (CheckNaam() && CheckEmail())
            {
                Navigation.NavigateTo<InterestViewModel>();
                ErrorMessage = "";
            }
            ErrorMessage = "Er ontbreken een aantal gegevens";



        }

        private bool CheckNaam()
        {
            if (NameProfile.IsNullOrEmpty())
            {
               
                return false;
            }
            else return true;
        }

        private bool CheckEmail()
        {
            if (EmailProfile.IsNullOrEmpty())
            {
                
                return false;
            }
            else return true;
        }
        private bool CanExecuteNavigateToInterestPage(object obj)
        {
            return true;
        }

    }
}
