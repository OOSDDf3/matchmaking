using LinkApplication;
using LinkApplicationGraphics.NVVM.Model;
using LinkApplicationGraphics.NVVM.ViewModel;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : UserControl
    {
        private List<(TextBlock, Border)> profileElements;
        Database_Connecter dbConnector = new Database_Connecter();

        public static bool magDoor;

        public ProfileView()
        {
            InitializeComponent();
            profileElements = new List<(TextBlock, Border)>
            {
            (TextBlockProfileName, UpdateInformationTextBoxName),
            (TextBlockProfileAddress, UpdateInformationTextBoxAddress),
            (TextBlockProfileGender, UpdateInformationTextBoxGender),
            (TextBlockProfileLanguage, UpdateInformationTextBoxLanguage),
            //(TextBlockProfileInterests, UpdateInformationTextBoxInterests),
            (TextBlockProfileEmail, UpdateInformationTextBoxEmail),
            //(TextBlockProfilePassword, UpdateInformationTextBoxPassword),
            };

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var (textBlock, border) in profileElements)
            {
                    textBlock.Visibility = Visibility.Hidden;
                    border.Visibility = Visibility.Visible;
            }
            buttonSave.Visibility = Visibility.Visible;
            ErrorMessageTextBlock.Visibility = Visibility.Visible;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = "";
            textBox.Foreground = Brushes.Black;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            string NameTextBoxValue = NameTextbox.Text;
            if (string.IsNullOrEmpty(NameTextBoxValue)) { NameTextBoxValue = TextBlockProfileName.Text; }

            string EmailTextBoxValue = EmailTextBox.Text;
            if (string.IsNullOrEmpty(EmailTextBoxValue)) { EmailTextBoxValue = TextBlockProfileEmail.Text; }

            //string PasswordTextBoxValue = PasswordTextBox.Text;
            //if (string.IsNullOrEmpty(PasswordTextBoxValue)) { PasswordTextBoxValue = TextBlockProfilePassword.Text; }

            string AddressTextBoxValue = AddressTextBox.Text;
            if (string.IsNullOrEmpty(AddressTextBoxValue)) { AddressTextBoxValue = TextBlockProfileAddress.Text; }

            string GenderTextBoxValue = GenderTextBox.Text;
            if (string.IsNullOrEmpty(GenderTextBoxValue)) { GenderTextBoxValue = TextBlockProfileGender.Text; }

            string LanguageTextBoxValue = LanguageTextBox.Text;
            if (string.IsNullOrEmpty(LanguageTextBoxValue)) { LanguageTextBoxValue = TextBlockProfileLanguage.Text; }

            if (IsEmailValid(EmailTextBox.Text) && IsPasswordValid(PasswordTextBox.Text))
            {
                foreach (var (textBlock, border) in profileElements)
                {
                    textBlock.Visibility = Visibility.Visible;
                    border.Visibility = Visibility.Hidden;
                }
                buttonSave.Visibility = Visibility.Hidden;            

                magDoor = true;
                dbConnector.UpdateAccount(Account.user_ID, NameTextBoxValue, EmailTextBoxValue, AddressTextBoxValue, GenderTextBoxValue, LanguageTextBoxValue);
            }
            else magDoor = false;


        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // Kies de juiste reguliere expressie op basis van de TextBox
            string regexPattern = GetRegexPatternForTextBox(textBox.Name);

            // Controleer of de ingevoerde tekst aan de voorwaarden voldoet
            if (!IsTextAllowed(e.Text, regexPattern))
            {
                e.Handled = true; // Voorkomt dat ongeldige tekens aan de TextBox worden toegevoegd
            }
        }

        private bool IsTextAllowed(string text, string regexPattern)
        {
            // Regex om te controleren of de tekst aan de voorwaarden voldoet
            Regex regex = new Regex(regexPattern);
            return regex.IsMatch(text);
        }

        private string GetRegexPatternForTextBox(string textBoxName)
        {
            // Hier kun je voor elke TextBox een andere reguliere expressie instellen
            switch (textBoxName)
            {
                case "AddressTextBox":
                    return @"^[a-zA-Z\d\s]+$";
                case "GenderTextBox":
                    return "^[a-zA-Z]+$";
                case "LanguageTextBox":
                    return "^[a-zA-Z]+$";
                // Voeg hier meer gevallen toe voor andere TextBoxes
                default:
                    return ".*"; // Standaard reguliere expressie (geen beperking)
            }
        }

        private bool IsEmailValid(string email)
        {
            // Voeg hier de e-mailvalidatielogica toe (bijv. reguliere expressie)
            return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]*@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        }

        private bool IsPasswordValid(string password)
        {
            // Voeg hier de wachtwoordvalidatielogica toe (bijv. minimale lengte)
            return password.Length >= 5;
        }
    }
}
