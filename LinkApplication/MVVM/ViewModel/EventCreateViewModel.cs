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
        private string eventName { get; set; }
        private int maxAttendees { get; set; }
        private string location { get; set; }
        private DateTime date { get; set; }
        private TimeOnly time { get; set; }
        private int interest_ID { get; set; }




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

        private void cancelEvent()
        {
            _connecter = new Database_Connecter();

            _connecter.DeleteFromEventsList();
        }
    }
}
