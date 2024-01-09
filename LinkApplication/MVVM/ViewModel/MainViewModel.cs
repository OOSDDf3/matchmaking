using LinkApplication;
using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.NVVM.View;
using LinkApplicationGraphics.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LinkApplicationGraphics.NVVM.ViewModel
{
    public class MainViewModel : Core.ViewModel
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

        public RelayCommand NavigateToLoginPageCommand { get; set; }

        public MainViewModel(INavigationService navService) 
        { 
            Navigation = navService;

            NavigateToLoginPageCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<LoginViewModel>(); }, canExecute: o => true);
        }
    }
}
