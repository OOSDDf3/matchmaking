using LinkApplication;
using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.NVVM.Model;
using LinkApplicationGraphics.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkApplicationGraphics.NVVM.ViewModel
{
    public class EventsViewModel: Core.ViewModel, INotifyPropertyChanged
    {

        Database_Connecter _connecter;

        //Ondersteuning voor weergave?
        public static int Event_ID { get; set; }
        public static string NameEvent { get; set; }
        public static string CurrentAttendeesEvent { get; set; }
        public static string MaxAttendeesEvent { get; set; }
        public static string LocationEvent { get; set; }
        public static string DateTimeEvent { get; set; }
        public static string InterestEvent { get; set; }
        public static string Organiser { get; set; }
        public static List<string> EventViewInterests { get; set; }

        public static ObservableCollection<Event> ListOfEvents { get; set; }

        private static int Interest_ID { get; set; }

        public static int Filter_ID { get; set; }

        /*private bool isDeleteEventButtonVisible;*/


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

        public RelayCommand NavigateToEventCreateViewCommand { get; set; }
        public RelayCommand FilterInterestCommand { get; set; }
        public RelayCommand FilterAttendanceCommand { get; set; }
        public RelayCommand ResetFilterCommand { get; set; }

        public EventsViewModel(INavigationService navService)
        {
            _connecter = new Database_Connecter();
            Navigation = navService;
            EventViewInterests = _connecter.ShowUserInterests(Account.user_ID);

            //aanmaken command voor navigeren naar eventcreateview, command execution staat in xaml
            //kijk naar commands login en registeren als je iets wel checken voor het uitvoeren van de command
            NavigateToEventCreateViewCommand = new RelayCommand(execute: o => { Navigation.NavigateToNew<EventCreateViewModel>(); }, canExecute: o => true);
            FilterInterestCommand = new RelayCommand(execute: filterInterestEvent, canExecute: o => true);
            FilterAttendanceCommand = new RelayCommand(execute: filterAttendanceEvent, canExecute: o => true);
            ResetFilterCommand = new RelayCommand(execute: resetFiltersEvent, canExecute: o => true);

            _connecter = new Database_Connecter();
            Event.AddEventsToList(_connecter.ShowAllEventInformation());


        }

        private void filterInterestEvent(object parameter)
        {
            
            _connecter = new Database_Connecter();

            Interest_ID = _connecter.SelectEventInterestID(InterestEvent);

            Event.FilterInterestEventsToList(_connecter.ShowAllEventInformation(), Interest_ID);
            
            Navigation.NavigateToNew<EventsViewModel>();
        }


        private void filterAttendanceEvent(object parameter)
        {

            _connecter = new Database_Connecter();

            Interest_ID = _connecter.SelectEventInterestID(InterestEvent);

            Event.FilterAttendanceEventsToList(_connecter.ShowAllEventInformation(), Account.user_ID);

            Navigation.NavigateToNew<EventsViewModel>();
        }


        private void resetFiltersEvent(object parameter)
        {
            _connecter = new Database_Connecter();

            Event.AddEventsToList(_connecter.ShowAllEventInformation());

            Navigation.NavigateToNew<EventsViewModel>();

        }

        public void OnUserControlLoaded()
        {
            _connecter = new Database_Connecter();
            Event.AddEventsToList(_connecter.ShowAllEventInformation());
            
        }





    }
}
