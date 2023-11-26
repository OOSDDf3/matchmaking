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
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        public void ShowPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e) => ShowPasswordFunction();
        public void ShowPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e) => HidePasswordFunction();
        public void ShowPassword_MouseLeave(object sender, MouseEventArgs e) => HidePasswordFunction();

        private void ReturnToMainPageButton_Click(object sender, RoutedEventArgs e)
        {

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


        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true; // Cancel the close action
            
        }

        //code voor wachtwoord
        public void ShowPasswordFunction()
        {
            ShowPassword.Text = "👁️";
            PasswordUnmask.Visibility = Visibility.Visible;
            WachtwoordBox.Visibility = Visibility.Hidden;
            PasswordUnmask.Text = WachtwoordBox.Password;
        }

        public void HidePasswordFunction()
        {
            ShowPassword.Text = "👁️";
            PasswordUnmask.Visibility = Visibility.Hidden;
            WachtwoordBox.Visibility = Visibility.Visible;
        }
    }
}
