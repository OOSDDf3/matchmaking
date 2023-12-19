using LinkApplication;
using LinkApplicationGraphics.NVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkApplicationGraphics.NVVM.Model
{
    public class Event
    {
        public static int Event_ID;

        public static string NameEvent { get; set; }
        public static string MaxAttendeesEvent { get; set; }
        public static string LocationEvent { get; set; }
        public static string DateTimeEvent { get; set; }
        public static string InterestEvent { get; set; }


        static Database_Connecter _connecter;

        public Event(int event_ID, string eventname, string maxattendees, string location, string datetime, string interest_ID)
        {
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
    }
}
