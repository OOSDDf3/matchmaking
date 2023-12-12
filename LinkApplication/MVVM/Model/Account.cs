﻿using LinkApplication;
using LinkApplicationGraphics.NVVM.ViewModel;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ZstdSharp.Unsafe;

namespace LinkApplicationGraphics.NVVM.Model
{
    public class Account
    {
        public static int user_ID;

        public static string NameProfile { get; set; }
        public static string BirthdateProfile { get; set; }
        public static string AddressProfile { get; set; }
        public static string GenderProfile { get; set; }
        public static string LanguageProfile { get; set; }
        public static string EmailProfile { get; set; }
        public static string PasswordProfile { get; set; }
        public static string HashedPassword {  get; set; }
        public static Byte[] ProfilePicture { get; set; }
        public static BitmapImage ProfilePictureImage { get; set; }


        //objecten voor interreses
        public static List<string> InterestsProfile { get; set; }
        public static string InterestsProfileString { get; set; }

        public static Dictionary<string, string> dataPerson = new Dictionary<string, string>();
        static Database_Connecter _connecter;

        public Account(string name, string age, string address, string gender, string Language, string email, string password)
        {
            NameProfile = name;
            BirthdateProfile = age;
            AddressProfile = address;
            GenderProfile = gender;
            EmailProfile = email;
            PasswordProfile = password;

        }

        public Account(string name, string age, string address, string gender, string Language, string email, string password , string hashedPassword, Byte[] profilePicture) 
        {
            NameProfile = name;
            BirthdateProfile = age;
            AddressProfile = address;
            GenderProfile = gender;
            EmailProfile = email;
            PasswordProfile = password;
            HashedPassword = hashedPassword;
            ProfilePicture = profilePicture;
  
        }

        public static void GetUserID(object sender, LoginEventargs e)
        {
            user_ID = e.User_ID;
        }

        public static void showUserInfo()
        {
            _connecter = new Database_Connecter();


            //Code voor ophalen informatie user en zetten gegevens voor account in profile weergaven.
            dataPerson = _connecter.ShowUserInformation(Account.user_ID, "SELECT * FROM Account WHERE user_ID = @user_ID");
            
            ProfileViewModel.NameProfile = dataPerson["name"];
            ProfileViewModel.BirthdateProfile = dataPerson["birthdate"];
            ProfileViewModel.AddressProfile = dataPerson["address"];
            ProfileViewModel.GenderProfile = dataPerson["gender"];
            ProfileViewModel.LanguageProfile = dataPerson["language"];
            ProfileViewModel.EmailProfile = dataPerson["email"];
            ProfileViewModel.PasswordProfile = dataPerson["password"];

            //Code voor ophalen en zetten interreses voor account in profile weergaven
            if(InterestsProfileString.IsNullOrEmpty())
            {
                InterestsProfile = _connecter.ShowUserInterests(Account.user_ID, "SELECT interest_ID FROM userinterestlist WHERE user_ID = @user_ID");

                for (int i = 0; i < InterestsProfile.Count; i += 3)
                {
                    for (int j = i; j < i + 3 && j < InterestsProfile.Count; j++)
                    {
                        if (j == InterestsProfile.Count - 1)
                        {
                            InterestsProfileString += $"{InterestsProfile[j]}";
                        }
                        else
                        {
                            InterestsProfileString += $"{InterestsProfile[j]}, ";
                        }
                    }
                    InterestsProfileString += Environment.NewLine;
                }
            }
            ProfileViewModel.InterestsProfileString = InterestsProfileString;

            //Code voor ophalen foto en zetten in profile weergave
            ProfilePicture = _connecter.ShowUserPicture(Account.user_ID);

            ProfileViewModel.ProfilePictureImage = Account.ImageFromBuffer(ProfilePicture);

            //code voor vinden match tijdelijk
            _connecter.GetMatchingUser(Account.user_ID);
            



        }
        public static BitmapImage ImageFromBuffer(byte[] buffer)
        {
            BitmapImage image = new BitmapImage();

            try
            {
                using (MemoryStream ms = new MemoryStream(buffer))
                {
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                }
            }
            catch (Exception ex)
            {
                // Handle exception as needed
                Console.WriteLine(ex.ToString());
            }

            return image;
        }

        public static void LogOut()
        {
            Account.user_ID = 0;
            Account.NameProfile = string.Empty;
            Account.BirthdateProfile = string.Empty;
            Account.AddressProfile = string.Empty;
            Account.GenderProfile = string.Empty;
            Account.LanguageProfile = string.Empty;
            Account.PasswordProfile = string.Empty;
            Account.InterestsProfileString = string.Empty ;

        }

    }
}