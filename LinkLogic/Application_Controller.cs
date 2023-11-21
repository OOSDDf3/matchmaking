using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkApplication
{
    public class Application_Controller
    {
        //Algemene connectie beginnen met database?
        

        //Account gegevens binnenhalen van registratie
        public void RegisterAccount()
        {
            //Binnengehaalde info moet voldoen aan correcte notatie
            //Alle info d.m.v. query in database zetten
            //AccountID wordt automatisch aangemaakt
        }

        //Account gegevens binnenhalen en inloggen

        public void Login()
        {
            //Binnengehaalde info moet voldoen aan correcte notatie
            //Met query checken of account bestaat (mail, ww, accountID?)
            //Als account bestaat EN info klopt DAN inloggen
            //Als info niet klopt? Terug geven dat de combi niet klopt
        }

        //Account gegevens wijzigen

        public void UpdateAccount()
        {
            //Binnengehaalde info moet voldoen aan correcte notatie
            //Check welke gegevens er wijzigen?
            //Query om gewijzigde gegevens door te voeren 
        }

        //Interesse gegevens bijwerken/wijzigen

        public void UpdateInterests()
        {
            //Binnengehaalde info moet voldoen aan correcte notatie?
            //Check welke interesses gewijzigd worden
            //Query om de interesse lijst bij te werken
        }
    }
}
