using LinkApplication;
using LinkApplicationGraphics.NVVM.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps.Serialization;

namespace LinkApplicationGraphics.NVVM.View
{
    /// <summary>
    /// Interaction logic for InterestView.xaml
    /// </summary>
    public partial class EditInterestView : UserControl
    {

        //List<CheckBox> checkBoxes = new List<CheckBox>();
        //private Account account;
        private Database_Connecter _connecter;
        private List<string> interestsPerson = new List<string>();
        private List<string> selectedInterests = new List<string>();
        private Dictionary<string, List<CheckBox>> Interests = new Dictionary<string, List<CheckBox>>();

        public EditInterestView()
        {
            InitializeComponent();
           

            _connecter = new Database_Connecter();
            List<string> categories = AddCategoriesToComboboxAndDictionary();
            foreach (string category in categories)
            {
                AddCheckBoxesToInterestDictionary(category);
            }

            //For every interest from database update checkbox to show it is checked
            List<List<string>> interestsList = _connecter.GetInterestsWithUserID(Account.user_ID);
            foreach(List<string> interest in interestsList)
            {
                UpdateCheckboxesInDictionaryToChecked(interest[0], interest[1]);
            }
            AddCheckBoxesToInterestsPage(comboBoxCategories.SelectedItem.ToString());
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            loopCheckbox();
            //debugPrint();            
            //Debug.WriteLine(Account.NameProfile);
            Account.InterestsProfile = interestsPerson;
        }

        // Retrieves categories from database and saves them in a combobox and dictionary
        private List<string> AddCategoriesToComboboxAndDictionary()
        {
            List<string> categories = _connecter.GetInterestCategories();
            foreach (string category in categories)
            {
                comboBoxCategories.Items.Add(category);
                Interests.Add(category, new List<CheckBox>());
            }
            comboBoxCategories.SelectedIndex = 1;
            return categories;
        }

        // Method that creates a new CheckBox object and returns it
        private CheckBox SetupCheckBox(string content)
        {
            CheckBox checkBox = new CheckBox()
            {
                Height = 27,
                Width = 120,
                Foreground = Brushes.White,
                Background = null,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                SnapsToDevicePixels = true,
                Content = content,
                Name = content,
                FontFamily = new FontFamily("Bahnschrift SemiLight")
            };
            Style borderStyle = new Style(typeof(Border));
            borderStyle.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(12)));
            checkBox.Resources.Add(typeof(Border), borderStyle);

            Style toggleButtonStyle = (Style)FindResource(typeof(ToggleButton));
            checkBox.Style = toggleButtonStyle;

            checkBox.Checked += CheckboxChecked;
            checkBox.Unchecked += CheckboxUnchecked;
            return checkBox;
        }

        // Method that adds checkboxes to the dictionary according to category
        private void AddCheckBoxesToInterestDictionary(string category)
        {
            List<string> interests = _connecter.GetInterestsWithCategory(category);
            foreach (string interest in interests)
            {
                CheckBox checkbox = SetupCheckBox(interest);
                Interests[category].Add(checkbox);
            }
            comboBoxCategories.SelectedIndex = 1;
        }

        private void UpdateCheckboxesInDictionaryToChecked(string category, string interest)
        {
            for(int i =0;i < Interests[category].Count; i++)
            {
                if (Interests[category][i].Name == interest)
                {
                    Interests[category][i].IsChecked = true;
                }
            }
        }

        // Method that retrieves checkboxes from dictionary and adds them to the grid
        public void AddCheckBoxesToInterestsPage(string category)
        {

            List<CheckBox> interests = Interests[category];
            int counter = 0;
            for (int i = 0; i <= interests.Count / 4; i++) // Get rid of magic numbers
            {
                for (int j = 0; j < 4; j++)
                {
                    if (counter == interests.Count)
                    {
                        break;
                    }
                    CheckBoxGrid.Children.Add(interests[counter]);
                    Grid.SetRow(interests[counter], i);
                    Grid.SetColumn(interests[counter], j);
                    counter++;
                }
            }
        }

        // Updates the selected interests shown on the interest page 
        public void UpdateSelectedInterestsToSelectedSection()
        {
            SelectedGrid.Children.Clear();
            int counter = 0;
            for (int i = 0; i <= selectedInterests.Count / 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (counter == selectedInterests.Count)
                    {
                        break;
                    }
                    Label label = new Label()
                    {
                        Content = selectedInterests[counter],
                        Foreground = Brushes.White,
                        FontSize = 16,
                        FontFamily = new FontFamily("Bahnschrift SemiLight")
                    };
                    SelectedGrid.Children.Add(label);
                    Grid.SetRow(label, i);
                    Grid.SetColumn(label, j);
                    counter++;
                }
            }
        }

        // Loops the checkboxes in the dictionary and adds the ones that are checked to the list of interests to be added to the database
        public void loopCheckbox()
        {
            foreach (KeyValuePair<string, List<CheckBox>> category in Interests)
            {
                foreach (CheckBox checkBox in category.Value)
                {
                    if (checkBox.IsChecked == true)
                    {
                        interestsPerson.Add(checkBox.Name);
                    }
                }
            }
        }

        public void CheckboxChecked(object sender, RoutedEventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            selectedInterests.Add(box.Name);
            UpdateSelectedInterestsToSelectedSection();
        }

        public void CheckboxUnchecked(object sender, RoutedEventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            selectedInterests.Remove(box.Name);
            UpdateSelectedInterestsToSelectedSection();
        }

        public void debugPrint()
        {
            foreach (String naam in interestsPerson)
            {
                Debug.WriteLine(naam);
            }
        }

        private void comboBoxCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckBoxGrid.Children.Clear();
            AddCheckBoxesToInterestsPage(comboBoxCategories.SelectedItem.ToString());
        }
    }

}