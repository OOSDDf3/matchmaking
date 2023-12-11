using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkApplicationGraphics.NVVM.Model
{
    public class LoginEventargs : EventArgs
    {
        public int _user_ID;
        public int User_ID { get => _user_ID; set => _user_ID = value; }
        public LoginEventargs(int user_id)
        {
            this.User_ID = user_id;
        }
    }
}
