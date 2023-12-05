using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkApplicationGraphics.NVVM.Model
{
    public class Account
    {
        public static int user_ID;

        public static string? NameProfile { get; set; }
        public static string? AgeProfile { get; set; }
        public static string? AddressProfile { get; set; }
        public static string? GenderProfile { get; set; }
        public static string? LanguageProfile { get; set; }
        public static string? EmailProfile { get; set; }
        public static string? PasswordProfile { get; set; }
        public static List<string>? InterestsProfile { get; set; }

        public Account(string name, string age, string address, string gender, string Language, string email, string password) 
        {
            NameProfile = name;
            AgeProfile = age;
            AddressProfile = address;
            GenderProfile = gender;
            EmailProfile = email;
            PasswordProfile = password;
        }

        public static void GetUserID(object sender, LoginEventargs e)
        {
            user_ID = e.User_ID;
        }       

    }
}
