using LinkApplication;
using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkApplicationGraphics.NVVM.ViewModel
{
    public class EventCreateViewModel : Core.ViewModel
    {
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
        public RelayCommand NavigateToEventsViewCommand { get; set; }



        public EventCreateViewModel(INavigationService navService)
        {
            Navigation = navService;

            //aanmaken navigation command terug naar de eventsview, command execution staat in xaml
            NavigateToEventsViewCommand = new RelayCommand(execute: o => { Navigation.NavigateToNew<EventsViewModel>(); }, canExecute: o => true);

        }

        private void createEvent()
        {
            _connecter = new Database_Connecter();

            _connecter.InsertIntoEventsList();

        }
    }
}
