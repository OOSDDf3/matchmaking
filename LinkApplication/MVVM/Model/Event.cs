using LinkApplication;
using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.NVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                // Access the Event ID (Tag) here
                int eventID = _eventID;
                Debug.WriteLine(eventID);
                Debug.WriteLine(_eventID);

                _connecter.DeleteFromEventsList(eventID, Account.user_ID);
            }


        }
    }
}
