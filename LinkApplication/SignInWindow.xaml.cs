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
        public void ShowPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e) => ShowPasswordFunction();
        public void ShowPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e) => HidePasswordFunction();
        public void ShowPassword_MouseLeave(object sender, MouseEventArgs e) => HidePasswordFunction();

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
            this.Hide();
            mainWindowInstance.registerWindow.Show();
        }

        private void Login_Button(object sender, RoutedEventArgs e)
        {

            if(EmailBox.Text == "spichon01@hotmail.com" & WachtwoordBox.Password.ToString()== "Samuel")
            {
                mainWindowInstance.Show();
                this.Hide();
            }
            
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

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
