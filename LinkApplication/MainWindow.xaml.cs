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

namespace LinkApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        InterestsWindow interestsWindow;
        ProfileWindow profileWindow;
        RegisterWindow registerWindow;
        SignInWindow signInWindow;
        EventsWindow eventsWindow;
        public MainWindow()
        {
            InitializeComponent();

        }

        private void ShowSignInWindowButton_Click(object sender, RoutedEventArgs e)
        {
            signInWindow = new SignInWindow(this);
            signInWindow.Show();

            this.Close();

        }

        private void ShowInterestsWindowButton_Click(object sender, RoutedEventArgs e)
        {
            interestsWindow = new InterestsWindow(this);
            interestsWindow.Show();

            this.Close();
        }

        private void ShowRegisterWindowButton_Click(object sender, RoutedEventArgs e)
        {
            registerWindow = new RegisterWindow(this);
            registerWindow.Show();

            this.Close();
        }

        private void ShowProfileWindowButton_Click(object sender, RoutedEventArgs e)
        {
            profileWindow = new ProfileWindow(this);
            profileWindow.Show();

            this.Close();
            
        }

        private void ShowEventsWindowButton_Click(object sender, RoutedEventArgs e)
        {
            eventsWindow = new EventsWindow(this);
            eventsWindow.Show();

            this.Close();
        }
    }
}
