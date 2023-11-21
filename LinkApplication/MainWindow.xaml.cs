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
        public InterestsWindow interestsWindow;
        public ProfileWindow profileWindow;
        public RegisterWindow registerWindow;
        public SignInWindow signInWindow;
        public EventsWindow eventsWindow;
        public MainWindow()
        {
            InitializeComponent();
            interestsWindow = new InterestsWindow(this);
            profileWindow = new ProfileWindow(this);
            registerWindow = new RegisterWindow(this);
            signInWindow = new SignInWindow(this);
            eventsWindow = new EventsWindow(this);
        }

        private void ShowSignInWindowButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            signInWindow.Show();
        }

        private void ShowInterestsWindowButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            interestsWindow.Show();
        }

        private void ShowRegisterWindowButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            registerWindow.Show();
        }

        private void ShowProfileWindowButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            profileWindow.Show();
        }

        private void ShowEventsWindowButton_Click(object sender, RoutedEventArgs e)
        {
            eventsWindow.Show();
            this.Hide();
        }
    }
}
