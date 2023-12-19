using LinkApplication;
using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.NVVM.Model;
using LinkApplicationGraphics.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkApplicationGraphics.NVVM.ViewModel
{
    public class EventsViewModel: Core.ViewModel
    {

        Database_Connecter _connecter;

        //Ondersteuning voor weergave?
        public static int event_ID;
        public static string NameEvent { get; set; }
        public static string MaxAttendeesEvent { get; set; }
        public static string LocationEvent { get; set; }
        public static string DateTimeEvent { get; set; }
        public static string InterestEvent { get; set; }

        public ObservableCollection<Event> ListOfEvents { get; set; }


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





        public EventsViewModel(INavigationService navService)
        {
            Navigation = navService;

            //aanmaken command voor navigeren naar eventcreateview, command execution staat in xaml
            //kijk naar commands login en registeren als je iets wel checken voor het uitvoeren van de command
            NavigateToEventCreateViewCommand = new RelayCommand(execute: o => { Navigation.NavigateToNew<EventCreateViewModel>(); }, canExecute: o => true);

            _connecter = new Database_Connecter();
            AddEventsToList(_connecter.ShowAllEventInformation());


        }

        public void OnUserControlLoaded()
        {
                _connecter = new Database_Connecter();
                AddEventsToList(_connecter.ShowAllEventInformation());
        }

        public void AddEventsToList(List<Dictionary<string, string>> eventList)
        {
            ListOfEvents = new ObservableCollection<Event>();
            foreach(var eventDict in eventList) {
                Event newEvent = new Event(
                    event_ID = Int32.Parse(eventDict["event_ID"]),
                    NameEvent = eventDict["eventname"],
                    MaxAttendeesEvent = eventDict["maxattendees"],
                    LocationEvent = eventDict["location"],
                    DateTimeEvent = eventDict["date"],
                    InterestEvent = eventDict["interest_ID"]
                );
                ListOfEvents.Add(newEvent);
            }
        }

    }
}
