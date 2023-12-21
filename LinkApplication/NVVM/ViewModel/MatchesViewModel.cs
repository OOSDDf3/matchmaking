using LinkApplication;
using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.NVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkApplicationGraphics.NVVM.ViewModel
{
    public class MatchesViewModel : Core.ViewModel
    {
        private Database_Connecter _Connecter;
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<MatchModel> Matches { get; set; }

        public string Username { get; set; }
        public string Status { get; set; }

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

        public RelayCommand SendCommand { get; set; }

        public MatchesViewModel()
        {
            _Connecter = new Database_Connecter();
            Messages = new ObservableCollection<MessageModel>();
            Matches = new ObservableCollection<MatchModel>();

            SendCommand = new RelayCommand(o =>
            {
                Messages.Add(new MessageModel
                {
                    Message = Message,
                    FirstMessage = false

                });
                Message = "";
            }, canExecute: o => true);

            Username = _Connecter.ShowUserInformation(Account.user_ID, "SELECT * FROM Account WHERE user_ID = @user_ID")["name"];
            Status = "Online";

            List<Dictionary<string, string>> MatchesData = _Connecter.GetMatchesWithUserID(Account.user_ID);
            foreach (var match in MatchesData)
            {
                MatchModel matchModel = new MatchModel();
                matchModel.Username = match["name"];
                matchModel.ProfilePicture = _Connecter.ShowUserPicture(Int32.Parse(match["user_ID"]));
                List<Dictionary<string, string>> messagesData = _Connecter.GetMessagesWithUserIDs(Account.user_ID, Int32.Parse(match["user_ID"]));
                foreach (var message in messagesData)
                {
                    MessageModel messageModel = new MessageModel();
                    messageModel.Username = message["name"];
                    messageModel.ImageSource = _Connecter.ShowUserPicture(Int32.Parse(message["user_ID"]));
                    messageModel.Message = message["message"];
                    messageModel.Time = DateTime.Parse(message["time"]);
                    matchModel.Messages.Add(messageModel);
                }
                Matches.Add(matchModel);
            }
        }


    }
}
