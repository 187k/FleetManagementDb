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

    public partial class EditVehicleWindow : Window
    {
        private Vehicles _vehicle;
        public EditVehicleWindow(Vehicles vehicle)
        {
            InitializeComponent();
            _vehicle = vehicle;


            LicensePlateTextBox.Text = _vehicle.LicensePlate;
            ModelTextBox.Text = _vehicle.Model;
            ManufacturerTextBox.Text = _vehicle.Manufacturer;
            YearTextBox.Text = _vehicle.YearOfManufacture.ToString();
            MileageTextBox.Text = _vehicle.Mileage.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            _vehicle.LicensePlate = LicensePlateTextBox.Text;
            _vehicle.Model = ModelTextBox.Text;
            _vehicle.Manufacturer = ManufacturerTextBox.Text;
            _vehicle.YearOfManufacture = int.Parse(YearTextBox.Text);
            _vehicle.Mileage = int.Parse(MileageTextBox.Text);

            DialogResult = true; 
            Close();
        }
    }
}
