using LinkApplication;
using LinkApplicationGraphics.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkApplicationGraphics.NVVM.ViewModel
{
    class ProfileViewModel : Core.ViewModel
    {
        public Dictionary<string, string> dataPerson = new Dictionary<string, string>();
        Database_Connecter _connecter;

        
        public string NameProfile { get; set; }
        public string AgeProfile { get; set; }
        public string AddressProfile { get; set; }
        public string GenderProfile { get; set; }
        public string LanguageProfile { get; set; }
        public string EmailProfile { get; set; }
        public string PasswordProfile { get; set; }





        public ProfileViewModel() 
        {
            _connecter = new Database_Connecter();


            dataPerson = _connecter.ShowUserInformation("spichon01@hotmail.com", "Hallo123!", "SELECT * FROM Account WHERE email = @email AND password = @password");

            NameProfile = dataPerson["name"];
            AgeProfile = dataPerson["age"];
            AddressProfile = dataPerson["address"];
            GenderProfile = dataPerson["gender"];
            LanguageProfile = dataPerson["language"];
            EmailProfile = dataPerson["email"];
            PasswordProfile = dataPerson["password"];

            
        }

    }
}
