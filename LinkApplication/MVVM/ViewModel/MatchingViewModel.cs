using LinkApplication;
using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.Services;
using LinkApplicationGraphics.NVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Media.Imaging;

namespace LinkApplicationGraphics.NVVM.ViewModel
{
    public class MatchingViewModel : Core.ViewModel
    {
        Database_Connecter _connecter;
        public Dictionary<string, string> dataPerson = new Dictionary<string, string>();

        private Dictionary<int, int> userMatches = new Dictionary<int, int>();
        private Byte[] MatchPicture { get; set; }

        private BitmapImage matchPictureImage;
        public BitmapImage MatchPictureImage
        {
            get { return matchPictureImage; }
            set
            {
                matchPictureImage = value;
                OnPropertyChanged();

            }
        }

        private string nameMatch;
        public string NameMatch
        {
            get { return nameMatch; }
            set
            {
                nameMatch = value;
                OnPropertyChanged();

            }
        }


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

        public RelayCommand AcceptMatchCommand {  get; set; }
        public RelayCommand DeclineMatchCommand { get; set; }


        public MatchingViewModel(INavigationService navService)
        {
            Navigation = navService;
            _connecter = new Database_Connecter();

            //haalt matches op die zelfde interesses hebben
            userMatches = _connecter.GetMatchingUser(Account.user_ID);


            AcceptMatchCommand = new RelayCommand(execute: getNewMatch, canExecute: o => true);
            DeclineMatchCommand = new RelayCommand(execute: getNewMatch, canExecute: o => true);
        }

        private void getNewMatch(Object parameter)
        {
            

            if (userMatches.Count != 0)
            {
                //haalt de userid op met de meest overeenkomende interreses
                int userIDMatch = userMatches.OrderByDescending(kvp => kvp.Value).First().Key;
                userMatches.Remove(userIDMatch);

                //haalt gegevens op van desbetreffende persoon
                dataPerson = _connecter.ShowUserInformation(userIDMatch);
                MatchPicture = _connecter.ShowUserPicture(userIDMatch);

                //zet de gegevens van de desbetreffende persoon
                MatchPictureImage = Account.ImageFromBuffer(MatchPicture);
                NameMatch = dataPerson["name"];

                dataPerson.Clear();

                Debug.WriteLine(userIDMatch);
            }
            else
            {
                MatchPictureImage = null;
                NameMatch = "Er zijn geen verdere matches gevonden";
                
            }
            

           

        }
    }
}
