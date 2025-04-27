using System;
using System.Collections.Generic;
using System.Data.Entity;
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

    public partial class EditRouteWindow : Window
    {
        private readonly Routes _route;
        public EditRouteWindow(Routes route)
        {
            InitializeComponent();
            _route = route;

            LoadData();
        }

        private void LoadData()
        {
            using (var context = new FleetManagementDbEntities())
            {
                VehicleComboBox.ItemsSource = context.Vehicles.ToList();
                DriverComboBox.ItemsSource = context.Drivers.ToList();
            }

           
            VehicleComboBox.SelectedValue = _route.VehicleId;
            DriverComboBox.SelectedValue = _route.DriverId;
            DepartureDatePicker.SelectedDate = _route.StartDate;
            ArrivalDatePicker.SelectedDate = _route.EndDate;
            StartLocationTextBox.Text = _route.StartLocation;
            EndLocationTextBox.Text = _route.EndLocation;
            DistanceTextBox.Text = _route.Distance.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _route.VehicleId = (int)VehicleComboBox.SelectedValue;
            _route.DriverId = (int)DriverComboBox.SelectedValue;
            _route.StartDate = DepartureDatePicker.SelectedDate ?? DateTime.Now;
            _route.EndDate = ArrivalDatePicker.SelectedDate ?? DateTime.Now;
            _route.StartLocation = StartLocationTextBox.Text;
            _route.EndLocation = EndLocationTextBox.Text;
            _route.Distance = float.Parse(DistanceTextBox.Text);

            using (var context = new FleetManagementDbEntities())
            {
                
                context.Entry(_route).State = EntityState.Modified;

                var vehicle = context.Vehicles.Find(_route.VehicleId);
                if (vehicle != null)
                {
                    if (_route.StartDate <= DateTime.Now && _route.EndDate >= DateTime.Now)
                    {
                        vehicle.Status = "В пути";
                    }
                    else if (_route.EndDate < DateTime.Now)
                    {
                        vehicle.Status = "Доступен";
                    }

                    context.Entry(vehicle).State = EntityState.Modified;
                }

                context.SaveChanges();
            }

            DialogResult = true; 
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
