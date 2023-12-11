using LinkApplication;
using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.NVVM.Model;
using LinkApplicationGraphics.NVVM.View;
using LinkApplicationGraphics.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LinkApplicationGraphics.NVVM.ViewModel
{
    public class PasswordChangeViewModel : Core.ViewModel
    {
        MainWindow mainWindow;
        public Dictionary<string, string> dataPerson = new Dictionary<string, string>();
        Database_Connecter _connecter;


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

        public RelayCommand NavigateToProfileViewCommand { get; set; }

        public PasswordChangeViewModel(INavigationService navService)
        {
            Navigation = navService;
            NavigateToProfileViewCommand = new RelayCommand(execute: o=> { Navigation.NavigateToNew<ProfileViewModel>(); } , canExecute: CanExecuteNavigateToProfile);

        }

        private bool CanExecuteNavigateToProfile(object obj)
        {

            return true;
        }

    }
}
