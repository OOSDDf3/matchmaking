using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkApplication
{
    public class Account_Info
    {
        //Voornaam, eventueel tussenvoegsel en achternaam toevoegen
        public string firstName { get; protected set;}

        //Voor het inloggen
        public string email { get; protected set;}
        public string password { get; protected set;}

        //Het adres (dit ook in database)
        public string cityName { get; protected set;}
        public string zipCode { get; protected set;}
        public string streetName { get; protected set;}
        public string houseNumber { get; protected set;}

        //Overige info account
        public string accountDescription { get; protected set;}
        public string language { get; protected set; }
        public string gender { get; protected set; }
        public DateTime birthDate { get; protected set;}

        //Lijst met interesses (of de interesseID's)?

        
    }
}
