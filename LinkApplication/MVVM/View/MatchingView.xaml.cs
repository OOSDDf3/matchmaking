using LinkApplicationGraphics.Core;
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
    /// Interaction logic for matchingView.xaml
    /// </summary>
    public partial class MatchingView : UserControl
    {
        public MatchingView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is MatchingViewModel viewModel)
            {
                viewModel.OnUserControlLoaded();
            }
        }
    }
}
