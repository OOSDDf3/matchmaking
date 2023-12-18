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
            Dictionary<string, string> dataEvent = new Dictionary<string, string>();

            dataEvent = _connecter.ShowAllEventInformation();

            
            EventsViewModel.NameEvent = dataEvent["eventname"];
            EventsViewModel.MaxAttendeesEvent = dataEvent["maxattendees"];
            EventsViewModel.LocationEvent = dataEvent["location"];
            EventsViewModel.DateTimeEvent = dataEvent["date"];
            EventsViewModel.InterestEvent = dataEvent["interest_ID"];

        }
    }
}
