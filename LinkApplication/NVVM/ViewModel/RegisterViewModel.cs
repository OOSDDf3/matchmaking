using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.NVVM.View;
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
    public class RegisterViewModel : Core.ViewModel
    {

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

        public RegisterViewModel(INavigationService navService)
        {
            Navigation = navService;
            NavigateToInterestCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<InterestViewModel>(); }, canExecute: CanExecuteNavigateToInterestPage);
            NavigateToBasePageCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<MainViewModel>(); }, canExecute: o => true);

        }

        private bool CanExecuteNavigateToInterestPage(object obj)
        {
            return true;
        }

    }
}
