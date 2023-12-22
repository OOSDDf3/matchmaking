using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LinkApplicationGraphics.NVVM.Model
{
    public class MatchModel
    {
        private ObservableCollection<MessageModel> messages;

        public int ChatID { get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }
        public string UsernameColor { get; set; }
        public string LastMessage => (Messages.Count > 0) ? Messages.Last().Message : " ";
        public Byte[] ProfilePicture { get; set; }
        public static BitmapImage ProfilePictureImage { get; set; }
        public ObservableCollection<MessageModel> Messages { get => messages; set => messages = value; }


        public MatchModel()
        {
            Messages = new ObservableCollection<MessageModel>();
            //MatchModel.ProfilePictureImage = Account.ImageFromBuffer(ProfilePicture);
            Random r = new Random();
            UsernameColor = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255), (byte)r.Next(1, 255), (byte)r.Next(1, 255))).ToString();
        }
    }
}
