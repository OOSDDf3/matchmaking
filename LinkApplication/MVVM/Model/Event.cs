using LinkApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkApplicationGraphics.NVVM.Model
{
    public class Event
    {
        public static int event_ID;

        public static string NameEvent { get; set; }
        public static string MaxAttendeesEvent { get; set; }
        public static string LocationEvent { get; set; }
        public static string DateEvent { get; set; }
        public static string TimeEvent { get; set; }
        public static string InterestEvent { get; set; }


        static Database_Connecter _connecter;



        public static void showEventInfo()
        {
            _connecter = new Database_Connecter();
        }
    }
}
