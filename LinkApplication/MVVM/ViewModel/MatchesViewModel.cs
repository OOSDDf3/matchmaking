﻿using LinkApplication;
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
using System.Windows.Media;

namespace LinkApplicationGraphics.NVVM.ViewModel
{
    public class MatchesViewModel : Core.ViewModel
    {

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

        private Database_Connecter _Connecter;
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<MatchModel> Matches { get; set; }

        public string Username { get; set; }
        public string Status { get; set; }
        public string UsernameColor { get; set; }

        private MatchModel _selectedMatch;
        public MatchModel SelectedMatch
        {
            get { return _selectedMatch; }
            set { _selectedMatch = value;
                OnPropertyChanged();
            }
        }


        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand NavigateToLoginPageCommand { get; set; }
        public RelayCommand SendCommand { get; set; }

        public MatchesViewModel(INavigationService navService)
        {

            Navigation = navService;

            _Connecter = new Database_Connecter();
            
            Messages = new ObservableCollection<MessageModel>();
            Matches = new ObservableCollection<MatchModel>();

            Random r = new Random();
            UsernameColor = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255), (byte)r.Next(1, 255), (byte)r.Next(1, 255))).ToString();

            NavigateToLoginPageCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<LoginViewModel>(); LogOut(); }, canExecute: CanExecuteCommand);

            SendCommand = new RelayCommand(o =>
            {                
                _Connecter.InsertIntoMessages(SelectedMatch.ChatID, Account.user_ID, Message);
                MessageModel model = new MessageModel();
                model.Username = Username;
                model.UsernameColor = UsernameColor;
                model.Message = Message;
                model.Time = DateTime.Now;
                SelectedMatch.Messages.Add(model);
                //UpdateMessages();
                OnPropertyChanged();
                Message = "";
            }, canExecute: o => true);

            GetMatches();
            UpdateMessages();
        }

        private void GetMatches()
        {
            Username = _Connecter.ShowUserInformation(Account.user_ID)["name"];
            Status = "Online";

            List<Dictionary<string, string>> MatchesData = _Connecter.GetMatchesWithUserID(Account.user_ID);
            foreach (var match in MatchesData)
            {
                MatchModel matchModel = new MatchModel();
                matchModel.ChatID = _Connecter.GetChatIDWithUserIDs(Account.user_ID, Int32.Parse(match["user_ID"]));
                matchModel.UserID = Int32.Parse(match["user_ID"]);
                matchModel.Username = match["name"];
                matchModel.ProfilePicture = _Connecter.ShowUserPicture(Int32.Parse(match["user_ID"]));
                Matches.Add(matchModel);
            }
        }


        private void UpdateMessages()
        {            
            foreach (var matchModel in Matches)
            {
                Messages = new ObservableCollection<MessageModel>();
                matchModel.Messages = new ObservableCollection<MessageModel>();
                List<Dictionary<string, string>> messagesData = _Connecter.GetMessagesWithUserIDs(Account.user_ID, matchModel.UserID);
                foreach (var message in messagesData)
                {
                    MessageModel messageModel = new MessageModel();
                    messageModel.Username = message["name"];
                    messageModel.UsernameColor = UsernameColor;
                    messageModel.ImageSource = _Connecter.ShowUserPicture(Int32.Parse(message["user_ID"]));
                    messageModel.Message = message["message"];
                    messageModel.Time = DateTime.Parse(message["time"]);
                    matchModel.Messages.Add(messageModel);
                }
            }
        }

        private bool CanExecuteCommand(Object obj)
        {
            return true;
        }

        private void LogOut()
        {
            Username = string.Empty;
            UsernameColor = string.Empty;

            Debug.WriteLine("LOGOUT SUCCESFUL");

            Messages = new ObservableCollection<MessageModel>();
            Matches.Clear();

        }
    }
}
