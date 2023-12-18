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
    public class EditInterestViewModel : Core.ViewModel
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

        public RelayCommand NavigateToProfileViewCommand { get; set; }  

        public EditInterestViewModel(INavigationService navService)
        {
            Navigation = navService;
            NavigateToProfileViewCommand = new RelayCommand(execute: o => { Navigation.NavigateToNew<ProfileViewModel>(); UpdateUserInterestList(); }, canExecute: CanExecuteNavigateToProfile);
        }

        private bool CanExecuteNavigateToProfile(object obj)
        {
            return true;
        }

        private void UpdateUserInterestList()
        {
            _connecter = new Database_Connecter();
            _connecter.DeleteFromUserInterestListWithUserID(Account.user_ID);
            _connecter.InsertIntoUserInterestList(Account.user_ID, Account.InterestsProfile);


            //haalt nieuwe match lijst op met nieuwe interreses
            Account.userMatches = _connecter.GetMatchingUser(Account.user_ID);
        }
    }
}
