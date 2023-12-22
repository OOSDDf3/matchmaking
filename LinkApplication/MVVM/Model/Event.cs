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
        public string CurrentAttendeesEvent { get; set; }
        public string MaxAttendeesEvent { get; set; }
        public string LocationEvent { get; set; }
        public string DateTimeEvent { get; set; }
        public string InterestEvent { get; set; }
        public string Organiser {  get; set; }

        public static bool IsOrganiser {  get; set; }


        static Database_Connecter _connecter;

        public RelayCommand DeleteEventCommand { get; set; }
        public RelayCommand JoinEventCommand { get; set; }
        public RelayCommand LeaveEventCommand { get; set; }

        public static ObservableCollection<Event> ListOfEvents { get; set; }


        public Event(int event_ID, string eventname, string currentattendees,  string maxattendees, string location, string datetime, string interest_ID, string user_ID)
        {
            DeleteEventCommand = new RelayCommand(execute: deleteEvent, canExecute: o => true);
            JoinEventCommand = new RelayCommand(execute: joinEvent, canExecute: o => true);
            LeaveEventCommand = new RelayCommand(execute: leaveEvent, canExecute: o => true);
            /*IsOrganiser = false;*/

            Event_ID = event_ID;
            NameEvent = eventname;
            CurrentAttendeesEvent = currentattendees;
            MaxAttendeesEvent = maxattendees;
            LocationEvent = location;
            DateTimeEvent = datetime;
            InterestEvent = interest_ID;
            Organiser = user_ID;
        }

        public static void showEventInfo()
        {
            _connecter = new Database_Connecter();
            List<Dictionary<string, string>> dataEvent = new();

            dataEvent = _connecter.ShowAllEventInformation();

            foreach (var eventDict in dataEvent) 
            {
                EventsViewModel.NameEvent = eventDict["eventname"];
                EventsViewModel.CurrentAttendeesEvent = eventDict["currentattendees"];
                EventsViewModel.MaxAttendeesEvent = eventDict["maxattendees"];
                EventsViewModel.LocationEvent = eventDict["location"];
                EventsViewModel.DateTimeEvent = eventDict["date"];
                EventsViewModel.InterestEvent = eventDict["interest_ID"];
                EventsViewModel.Organiser = eventDict["user_id"];
            }
        }

        private void deleteEvent(object parameter)
        {
            _connecter = new Database_Connecter();
            if (parameter is int _eventID)
            {
                int eventID = _eventID;

                if (Int32.Parse(Organiser) == Account.user_ID)
                {
                    MessageBoxResult result = MessageBox.Show("Weet je zeker dat je dit event wil annuleren?", "Confirmation", MessageBoxButton.YesNo);
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
        }

        private void joinEvent(object parameter)
        {
            _connecter = new Database_Connecter();
            if (parameter is int _eventID)
            {
                int eventID = _eventID;

                if (!(_connecter.IsUserAttending(eventID, Account.user_ID)) && (_connecter.IsMaxAttendanceReached(eventID) == false))
                {
                    _connecter.InsertIntoUserEventsList(eventID, Account.user_ID);
                   _connecter.AddUserAsAttendee(eventID);
                    MessageBox.Show("Je hebt je succesvol aangemeld");
                    Event.AddEventsToList(_connecter.ShowAllEventInformation());
                }
                Event.AddEventsToList(_connecter.ShowAllEventInformation());
            }
        }

        private void leaveEvent(object parameter)
        {
            _connecter = new Database_Connecter();
            if (parameter is int _eventID)
            {
                int eventID = _eventID;

                if (_connecter.IsUserAttending(eventID, Account.user_ID))
                {
                    MessageBoxResult result = MessageBox.Show("Weet je zeker dat je je wilt afmelden voor dit event?", "Confirmation", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        MessageBox.Show("Je hebt je succesvol afgemeld");
                        _connecter.DeleteFromUserEventsList(eventID, Account.user_ID);
                        _connecter.DeleteUserAsAttendee(eventID);
                        Event.AddEventsToList(_connecter.ShowAllEventInformation());
                    }
                    else
                    {

                    }
                }
            }

        }

        public static void AddEventsToList(List<Dictionary<string, string>> eventList)
        {
            _connecter = new Database_Connecter();
            Event.ListOfEvents = new ObservableCollection<Event>();
            foreach (var eventDict in eventList)
            {
                Event newEvent = new Event(
                    Int32.Parse(eventDict["event_ID"]),
                    eventDict["eventname"],
                    eventDict["currentattendees"],
                    eventDict["maxattendees"],
                    eventDict["location"],
                    eventDict["date"],
                    _connecter.SelectEventInterestName(Int32.Parse(eventDict["interest_ID"])),
                    eventDict["user_ID"]
                );
                Event.ListOfEvents.Add(newEvent);
            }
            EventsViewModel.ListOfEvents = Event.ListOfEvents;
        }

        public static void FilterInterestEventsToList(List<Dictionary<string, string>> eventList, int interest_ID)
        {
            _connecter = new Database_Connecter();
            Event.ListOfEvents = new ObservableCollection<Event>();
            foreach (var eventDict in eventList)
            {
                Event newEvent = new Event(
                    Int32.Parse(eventDict["event_ID"]),
                    eventDict["eventname"],
                    eventDict["currentattendees"],
                    eventDict["maxattendees"],
                    eventDict["location"],
                    eventDict["date"],
                    _connecter.SelectEventInterestName(Int32.Parse(eventDict["interest_ID"])),
                    eventDict["user_ID"]
                );

                if ((Int32.Parse(eventDict["interest_ID"]) == interest_ID)) 
                {
                    Event.ListOfEvents.Add(newEvent);
                }
                
                
            }
            EventsViewModel.ListOfEvents = Event.ListOfEvents;
        }


        public static void FilterAttendanceEventsToList(List<Dictionary<string, string>> eventList, int user_ID)
        {
            _connecter = new Database_Connecter();
            Event.ListOfEvents = new ObservableCollection<Event>();
            foreach (var eventDict in eventList)
            {
                Event newEvent = new Event(
                    Int32.Parse(eventDict["event_ID"]),
                    eventDict["eventname"],
                    eventDict["currentattendees"],
                    eventDict["maxattendees"],
                    eventDict["location"],
                    eventDict["date"],
                    _connecter.SelectEventInterestName(Int32.Parse(eventDict["interest_ID"])),
                    eventDict["user_ID"]
                );

                if (_connecter.IsUserAttending(Int32.Parse(eventDict["event_ID"]), user_ID))
                {
                    Event.ListOfEvents.Add(newEvent);
                }


            }
            EventsViewModel.ListOfEvents = Event.ListOfEvents;
        }

    }
}
