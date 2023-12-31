﻿using LinkApplication;
using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.Services;
using LinkApplicationGraphics.NVVM.Model;
using MaterialDesignThemes.Wpf.Converters;
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
        public string EventName { get; set; }
        public int MaxAttendees { get; set; }
        public string Location { get; set; }
        public DateTime DateEvent { get; set; }
        public string TimeEvent { get; set; }
        public string Interest { get; set; }

        private int Interest_ID { get; set; }

        public List<string> EventInterests { get; set; }



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

        public RelayCommand CreateEventCommand { get; set; }

        public EventCreateViewModel(INavigationService navService)
        {
            Navigation = navService;

            _connecter = new Database_Connecter();

            DateEvent = DateTime.Now.Date;

            EventInterests = _connecter.ShowUserInterests(Account.user_ID);
            


            //aanmaken navigation command terug naar de eventsview, command execution staat in xaml
            NavigateToEventsViewCommand = new RelayCommand(execute: o => { Navigation.NavigateToNew<EventsViewModel>(); }, canExecute: o => true);
            CreateEventCommand = new RelayCommand(execute: o => { createEvent(); }, canExecute: o => true);

        }

        private void createEvent()
        {
            _connecter = new Database_Connecter();

            

            Interest_ID = _connecter.SelectEventInterestID(Interest);

            _connecter.InsertIntoEventsList(EventName, MaxAttendees, Location , DateEvent, TimeOnly.Parse(TimeEvent) , Interest_ID , Account.user_ID);
            Navigation.NavigateToNew<EventsViewModel>();

        }

        //private void cancelEvent()
        //{
        //    _connecter = new Database_Connecter();

        //    _connecter.DeleteFromEventsList();
        //}
    }
}
