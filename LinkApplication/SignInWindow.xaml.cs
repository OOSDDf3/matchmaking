using Microsoft.Windows.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LinkApplication
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        MainWindow mainWindowInstance;
        public SignInWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            mainWindowInstance = mainWindow;
        }

        private void ReturnToMainPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            mainWindowInstance.Show();
        }

        private void ForgotPassword_Button(object sender, RoutedEventArgs e)
        {

        }

        private void Register_Button(object sender, RoutedEventArgs e)
        {

        }

        private void Login_Button(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
