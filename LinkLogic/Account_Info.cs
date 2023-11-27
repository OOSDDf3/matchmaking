using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace LinkApplication
{
    public class Account_Info : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public static int User_ID;

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Email)));

            }
        }

        private SecureString password;
        public SecureString Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
                
            }
        }

        public Account_Info() 
        {
            Email = "<Default email>";
        }

      



    }
}
