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

        public static void GetUserID(object sender, LoginEventargs e)
        {
            user_ID = e.User_ID;
        }

    }
}
