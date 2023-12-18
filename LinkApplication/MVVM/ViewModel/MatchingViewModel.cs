﻿using LinkApplication;
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
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using ZstdSharp.Unsafe;

namespace LinkApplicationGraphics.NVVM.ViewModel
{
    public class MatchingViewModel : Core.ViewModel
    {
        Database_Connecter _connecter;
        public Dictionary<string, string> dataPerson = new Dictionary<string, string>();
        private Byte[] MatchPicture { get; set; }

        private int userIDMatch;

        private List<string> InterestsMatchList { get; set; }

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

        private string interestsMatch;
        public string InterestsMatch
        {
            get { return interestsMatch; }
            set
            {
                interestsMatch = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand AcceptMatchCommand {  get; set; }
        public RelayCommand DeclineMatchCommand { get; set; }


        public MatchingViewModel(INavigationService navService)
        {
            _connecter = new Database_Connecter();

            getNewMatch();

            AcceptMatchCommand = new RelayCommand(execute: o => { likeMatch(); getNewMatch();}, canExecute: o => true);
            DeclineMatchCommand = new RelayCommand(execute: o => { disLikeMatch(); getNewMatch();}, canExecute: o => true);
        }

        private void getNewMatch()
        {

            if (Account.count == 0 && Account.userMatches.IsNullOrEmpty())
            {
                //haalt matches op die zelfde interesses hebben
                Account.userMatches = _connecter.GetMatchingUser(Account.user_ID);
                Account.count++;
            }

            if (Account.userMatches.Count != 0)
            {
                int age = 0;

                //haalt de userid op met de meest overeenkomende interreses
                userIDMatch = Account.userMatches.OrderByDescending(kvp => kvp.Value).First().Key;
                Account.userMatches.Remove(userIDMatch);

                //haalt gegevens op van desbetreffende persoon
                dataPerson = _connecter.ShowUserInformation(userIDMatch);
                MatchPicture = _connecter.ShowUserPicture(userIDMatch);

                //zet de gegevens van de desbetreffende persoon
                MatchPictureImage = Account.ImageFromBuffer(MatchPicture);

                //zet de naam en leeftijd van de persoon
                DateTime currentDate = DateTime.Today;
                age = currentDate.Year - Int32.Parse(dataPerson["birthdate"]);
                NameMatch = $"{dataPerson["name"]}, {age} jaar";

                //zet de interesses van de persoon
                InterestsMatchList = _connecter.ShowUserInterests(userIDMatch);
                InterestsMatch = Account.FormatInterests(InterestsMatchList, 5);

                dataPerson.Clear();

                Debug.WriteLine(userIDMatch);
            }
            else
            {
                userIDMatch = 0;
                InterestsMatch = string.Empty;
                MatchPictureImage = null;
                NameMatch = "Er zijn geen verdere matches gevonden";

            }
        } 

        private void likeMatch()
        {
            if(userIDMatch != 0)
            {
                _connecter.InsertIntoLikesDislikes(Account.user_ID, userIDMatch, "like");
            }
            
        }

        private void disLikeMatch()
        {
            if (userIDMatch != 0)
            {
                _connecter.InsertIntoLikesDislikes(Account.user_ID, userIDMatch, "dislike");
            }
        }
    }
}
