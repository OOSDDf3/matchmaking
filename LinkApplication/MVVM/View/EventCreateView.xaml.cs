using LinkApplication;
using LinkApplicationGraphics.NVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LinkApplicationGraphics.NVVM.View
{
    /// <summary>
    /// Interaction logic for EventCreateView.xaml
    /// </summary>
    public partial class EventCreateView : UserControl
    {
        Database_Connecter _connecter;
        public EventCreateView()
        {
            InitializeComponent();
        }

        private void DatePicker_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true; // Prevent keyboard input
        }

        private void TimePicker_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true; // Prevent keyboard input
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
