using LinkApplication;
using LinkApplicationGraphics.NVVM.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class InterestView : UserControl
    {

        List<CheckBox> checkBoxes = new List<CheckBox>();
        Account account;
        Database_Connecter _connecter;
        List<string> interestsPerson = new List<string>();

        public InterestView()
        {
            InitializeComponent();
            List<string> interests = new() { "Basketballen", "Volleyballen", "Hondermeterstilliggen", "Fietsen", "Knikkeren", "Hardlopen", "Klootschieten", "Flierleppen",  
                "Hockeyen", "Voet", "Computeren", "Gamen", "Basketballen", "Volleyballen", "Honderdmeter", "Fietsen", "Knikkeren", "Lopen", "Klootschieten", "Flierleppen", 
                "Hockeyen", "Voetbal", "Computeren", "Gamen" };

            _connecter = new Database_Connecter();
            AddCategoriesToCombobox();
            AddCheckBoxesToInterestsPage(comboBoxCategories.SelectedItem.ToString());
        }

        private void buttonCreate_Click(object sender, RoutedEventArgs e)
        {
            loopCheckbox();
            //debugPrint();            
            Debug.WriteLine(Account.NameProfile);
            Account.InterestsProfile = interestsPerson;
           

            // Assuming you're outside of the App class



        }

        private void AddCategoriesToCombobox()
        {
            List<string> categories = _connecter.GetInterestCategories();
            foreach (string category in categories)
            {
                comboBoxCategories.Items.Add(category); 
            }
            comboBoxCategories.SelectedIndex = 1;
        }

        private CheckBox SetupCheckBoxInterestsPage(int row, int col, string content)
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
                Name = content              
            };
            Style borderStyle = new Style(typeof(Border));
            borderStyle.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(12)));
            checkBox.Resources.Add(typeof(Border), borderStyle);

            Style toggleButtonStyle = (Style)FindResource(typeof(ToggleButton)); // Assuming ToggleButton is defined in XAML resources
            checkBox.Style = toggleButtonStyle;

            CheckBoxGrid.Children.Add(checkBox);
            Grid.SetRow(checkBox, row);
            Grid.SetColumn(checkBox, col);
            return checkBox;
        }

        public void AddCheckBoxesToInterestsPage(string category)
        {
            List<string> interests = _connecter.GetInterestsWithCategory(category);
            int counter = 0;
            for (int i = 0; i <= interests.Count/4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (counter == interests.Count)
                    {
                        break;
                    }
                    checkBoxes.Add(SetupCheckBoxInterestsPage(i, j, interests[counter]));
                    counter++;
                }
            }
        }

        public void loopCheckbox()
        {
            foreach (CheckBox checkBox in checkBoxes)
            {
                if(checkBox.IsChecked == true)
                {
                    interestsPerson.Add(checkBox.Name);
                }
            }
        }

        public void debugPrint()
        {
            foreach(String naam in interestsPerson) 
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
