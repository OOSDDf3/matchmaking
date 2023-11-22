using LinkApplication;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
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
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();

        }
        
        String gender;
        public void ShowPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e) => ShowPasswordFunction();
        public void ShowPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e) => HidePasswordFunction();
        public void ShowPassword_MouseLeave(object sender, MouseEventArgs e) => HidePasswordFunction();

        private void ReturnToMainPageButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();

            Window.GetWindow(this).Close();
        }

        private void buttonCreate_Click(object sender, RoutedEventArgs e)
        {

            //Test of alle gegevens doorkomen

            Debug.WriteLine(textBoxName.Text);
            Debug.WriteLine(textBoxEmail.Text);
            Debug.WriteLine(textBoxBirth.Text);
            Debug.WriteLine(textBoxStreet.Text);
            Debug.WriteLine(textBoxPostalCode.Text);
            SecureString password = textBoxPassword.SecurePassword;
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(password);
            string plainPassword = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            Debug.WriteLine(plainPassword);

            if (radioButtonGender1.IsChecked== true) 
            {
                gender = "Man";
            }
            if(radioButtonGender2.IsChecked== true) 
            {
                gender = "Vrouw";
            }
            else
            {
                gender = "Anders";
            }
            Debug.WriteLine(gender);

        }

        //code voor wachtwoord
        public void ShowPasswordFunction()
        {
            ShowPassword.Text = "👁️";
            PasswordUnmask.Visibility = Visibility.Visible;
            textBoxPassword.Visibility = Visibility.Hidden;
            PasswordUnmask.Text = textBoxPassword.Password;
        }

        public void HidePasswordFunction()
        {
            ShowPassword.Text = "👁️";
            PasswordUnmask.Visibility = Visibility.Hidden;
            textBoxPassword.Visibility = Visibility.Visible;

        }
    }
}
