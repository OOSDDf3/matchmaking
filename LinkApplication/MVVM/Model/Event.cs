using LinkApplication;
using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.NVVM.ViewModel;
using LinkApplicationGraphics.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LinkApplicationGraphics.NVVM.Model
{
    public class Event
    {
        public int Event_ID { get; set; }

        public string NameEvent { get; set; }
        public string MaxAttendeesEvent { get; set; }
        public string LocationEvent { get; set; }
        public string DateTimeEvent { get; set; }
        public string InterestEvent { get; set; }


        static Database_Connecter _connecter;

        public RelayCommand DeleteEventCommand { get; set; }

        public static ObservableCollection<Event> ListOfEvents { get; set; }


        public Event(int event_ID, string eventname, string maxattendees, string location, string datetime, string interest_ID)
        {
            DeleteEventCommand = new RelayCommand(execute: deleteEvent, canExecute: o => true);

            Event_ID = event_ID;
            NameEvent = eventname;
            MaxAttendeesEvent = maxattendees;
            LocationEvent = location;
            DateTimeEvent = datetime;
            InterestEvent = interest_ID;
        }

        public static void showEventInfo()
        {
            _connecter = new Database_Connecter();
            List<Dictionary<string, string>> dataEvent = new();

            dataEvent = _connecter.ShowAllEventInformation();

            foreach (var eventDict in dataEvent) 
            {
                EventsViewModel.NameEvent = eventDict["eventname"];
                EventsViewModel.MaxAttendeesEvent = eventDict["maxattendees"];
                EventsViewModel.LocationEvent = eventDict["location"];
                EventsViewModel.DateTimeEvent = eventDict["date"];
                EventsViewModel.InterestEvent = eventDict["interest_ID"];
            }
        }

        private void deleteEvent(object parameter)
        {
            _connecter = new Database_Connecter();
            if (parameter is int _eventID)
            {
                int eventID = _eventID;

                MessageBoxResult result = MessageBox.Show("Weet je zeker dat je dit event wil verwijderen", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Event is verwijderd, herlaad de pagina");
                    _connecter.DeleteFromEventsList(eventID, Account.user_ID);
                    Event.AddEventsToList(_connecter.ShowAllEventInformation());
                }
                else
                {
                    
                }
            }
        }

        public static void AddEventsToList(List<Dictionary<string, string>> eventList)
        {
            Event.ListOfEvents = new ObservableCollection<Event>();
            foreach (var eventDict in eventList)
            {
                Event newEvent = new Event(
                    Int32.Parse(eventDict["event_ID"]),
                    eventDict["eventname"],
                    eventDict["maxattendees"],
                    eventDict["location"],
                    eventDict["date"],
                    eventDict["interest_ID"]
                );
                Event.ListOfEvents.Add(newEvent);
            }
            EventsViewModel.ListOfEvents = Event.ListOfEvents;
        }
    }
}
