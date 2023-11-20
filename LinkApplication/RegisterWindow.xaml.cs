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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace LinkApplication
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        MainWindow mainWindowInstance;

        GenderOptions genderOptions;
        // Buttons
        Button mainPageButton;
        Button createButton;
        Button cancelButton;

        // TextBoxes
        TextBox nameTextBox;
        TextBox birthdayTextBox;
        TextBox streetTextBox;
        TextBox postalCodeTextBox;

        // Comboboxes
        ComboBox genderComboBox;

        // Password 
        public void ShowPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e) => ShowPasswordFunction();
        public void ShowPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e) => HidePasswordFunction();
        public void ShowPassword_MouseLeave(object sender, MouseEventArgs e) => HidePasswordFunction();


        public RegisterWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            mainWindowInstance = mainWindow;
            createButton = buttonCreate;
            cancelButton = buttonCancel;
            nameTextBox = textBoxName;
            birthdayTextBox = textBoxBirth;
            streetTextBox = textBoxStreet;
            postalCodeTextBox = textBoxPostalCode;
            genderComboBox = comboBoxGender;
            SetupUserInputSection();
        }

        private void ReturnToMainPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            mainWindowInstance.Show();
        }

        private void SetupUserInputSection()
        {
            // Buttons
            //mainPageButton = SetupButton("Hoofdpagina", 400, 150, MainGrid, ReturnToMainPageButton_Click, 0, 0, Brushes.White);
            // Labels

            // TextBoxes
        }

        private Button SetupButton(string content, int width, int height, Grid grid, RoutedEventHandler onClick, int row, int col, Brush foreGround)
        {
            Button button = new()
            {
                Content = content,
                Width = width,
                Height = height,
                Foreground = foreGround,
                Background = Brushes.Black,
                BorderBrush = foreGround,
                BorderThickness = new Thickness(2, 2, 2, 2),
                FontSize = 24,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            button.Click += onClick;
            grid.Children.Add(button);
            Grid.SetRow(button, row);
            Grid.SetColumn(button, col);
            return button;
        }

        private Label SetupLabel(string content, int fontsize, Grid grid, int row, int col, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            Label label = new()
            {
                Content = content,
                FontSize = fontsize,
                HorizontalAlignment = horizontalAlignment,
                VerticalAlignment = verticalAlignment,
                Foreground = Brushes.White,
            };
            grid.Children.Add(label);
            Grid.SetRow(label, row);
            Grid.SetColumn(label, col);
            return label;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            mainWindowInstance.signInWindow.Show();
        }

        private void buttonCreate_Click(object sender, RoutedEventArgs e)
        {
            // TODO: check information in real time, give user feedback on input
            // TODO: get information from textboxes and make account in database
            Console.WriteLine(nameTextBox.Text);
            Console.WriteLine(genderComboBox.Text);
            Console.WriteLine(birthdayTextBox.Text);
            Console.WriteLine(streetTextBox.Text);
            Console.WriteLine(postalCodeTextBox.Text);
            this.Hide();
            mainWindowInstance.profileWindow.Show();
        }

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
        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}