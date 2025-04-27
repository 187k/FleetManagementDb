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

namespace FleetManagementDb.Pages
{

    public partial class EditDriverWindow : Window
    {
        private Drivers _driver;
        public EditDriverWindow(Drivers driver)
        {
            InitializeComponent();
            _driver = driver;

          
            FullNameTextBox.Text = _driver.FullName;
            LicenseNumberTextBox.Text = _driver.LicenseNumber;
            PhoneNumberTextBox.Text = _driver.PhoneNumber;
            ExperienceTextBox.Text = _driver.Experience.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            
            _driver.FullName = FullNameTextBox.Text;
            _driver.LicenseNumber = LicenseNumberTextBox.Text;
            _driver.PhoneNumber = PhoneNumberTextBox.Text;

            if (int.TryParse(ExperienceTextBox.Text, out int experience))
            {
                _driver.Experience = experience;
            }
            else
            {
                MessageBox.Show("Введите корректное значение для стажа", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DialogResult = true; 
            Close();
        }
    }
}
