using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LinkApplicationGraphics.NVVM.Model
{
    public class MessageModel
    {
        public string Username { get; set; }

        public Byte[] ImageSource { get; set; }
        public static BitmapImage Image { get; set; }

        public string Message { get; set; }

        public DateTime Time { get; set; }

        public bool IsNativeOrigin { get; set; }

        public bool? FirstMessage { get; set; }

        public MessageModel()
        {
            MessageModel.Image = Account.ImageFromBuffer(ImageSource);
        }
    }
}
