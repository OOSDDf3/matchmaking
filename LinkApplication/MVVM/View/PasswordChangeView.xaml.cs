using LinkApplication;
using LinkApplicationGraphics.NVVM.Model;
using LinkApplicationGraphics.NVVM.ViewModel;
using MaterialDesignColors.Recommended;
using Microsoft.IdentityModel.Tokens;
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
    /// Interaction logic for PasswordChangeView.xaml
    /// </summary>
    public partial class PasswordChangeView : UserControl
    {
        public void ShowPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e) => ShowPasswordFunction();
        public void ShowPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e) => HidePasswordFunction();
        public void ShowPassword_MouseLeave(object sender, MouseEventArgs e) => HidePasswordFunction();

        public void ShowPassword_PreviewMouseDownNew(object sender, MouseButtonEventArgs e) => ShowPasswordFunctionNew();
        public void ShowPassword_PreviewMouseUpNew(object sender, MouseButtonEventArgs e) => HidePasswordFunctionNew();
        public void ShowPassword_MouseLeaveNew(object sender, MouseEventArgs e) => HidePasswordFunctionNew();

        public static bool magDoor = false;


        Database_Connecter _connecter;
        public PasswordChangeView()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            _connecter = new Database_Connecter();
            string clearTextPasswordCheck = passwordboxCheck.Password;
            string hashedPasswordCheck = PasswordHasher.HashPassword(clearTextPasswordCheck);

            string clearTextPasswordNew = passwordboxNew.Password;
            string hashedPasswordNew = PasswordHasher.HashPassword(clearTextPasswordNew);

            if (!passwordboxCheck.Password.IsNullOrEmpty() && !passwordboxNew.Password.IsNullOrEmpty())
            {
                if (IsPasswordValid(clearTextPasswordNew))
                {
                    if (_connecter.CheckLogin(ProfileViewModel.EmailProfile, hashedPasswordCheck))
                    {
                        _connecter.UpdatePassword(Account.user_ID, hashedPasswordNew);
                        ErrorMessage.Foreground = Brushes.Green;
                        ErrorMessage.Content = "Wachtwoord succesvol gewijzigd";
                        magDoor = true;
                    }
                    else
                    {
                        magDoor = false;
                        ErrorMessage.Content = "Het ingevulde oude wachtwoord is onjuist";
                    }
                }
                else
                {
                    ErrorMessage.Content = "Het ingevulde wachtwoord moet langer zijn dan 5 karakters";
                }
            }
            else
            {
                magDoor = false;
                ErrorMessage.Content = "Er is nog niks ingevuld!";
            }

        }

        private bool IsPasswordValid(string password)
        {
            // Voeg hier de wachtwoordvalidatielogica toe (bijv. minimale lengte)
            return password.Length >= 5;
        }

        public void ShowPasswordFunction()
        {
            PasswordUnmaskCheck.Visibility = Visibility.Visible;
            passwordboxCheck.Visibility = Visibility.Hidden;
            PasswordUnmaskCheck.Text = passwordboxCheck.Password;
        }

        public void HidePasswordFunction()
        {
            PasswordUnmaskCheck.Visibility = Visibility.Hidden;
            passwordboxCheck.Visibility = Visibility.Visible;
        }        
        
        public void ShowPasswordFunctionNew()
        {
            PasswordUnmaskNew.Visibility = Visibility.Visible;
            passwordboxNew.Visibility = Visibility.Hidden;
            PasswordUnmaskNew.Text = passwordboxNew.Password;
        }

        public void HidePasswordFunctionNew()
        {
            PasswordUnmaskNew.Visibility = Visibility.Hidden;
            passwordboxNew.Visibility = Visibility.Visible;
        }


    }
}
