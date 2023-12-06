using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkApplication
{
    public class Events_Info
    {
        //Algemene event info
        public string eventDescription { get; protected set; }
        public DateTime eventDate { get; protected set; }

        //Ook info qua type interesse?

        //Event locatie info
        public string eventCity { get; protected set; }
        public string eventZipcode { get; protected set;}
        public string eventStreet { get; protected set;}
        public string eventHouseNumber { get; protected set;}

        //Organisator is een gebruiker
    }
}
