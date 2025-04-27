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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FleetManagementDb;

namespace FleetManagementDb.Pages
{
    public partial class DriversPage : Page
    {
        public DriversPage()
        {
            InitializeComponent();
            LoadDrivers();
        }

        private void LoadDrivers()
        {
            using (var context = new FleetManagementDbEntities())
            {
                var drivers = context.Drivers.ToList();
                DriversDataGrid.ItemsSource = drivers;
            }
        }

        private void AddDriver_Click(object sender, RoutedEventArgs e)
        {
            Drivers newDriver = new Drivers
            {
                FullName = "Новый водитель",
                LicenseNumber = "Номер лицензии",
                PhoneNumber = "Телефон",
                Experience = 0
            };
            AddDriver(newDriver);
        }

        private void AddDriver(Drivers newDriver)
        {
            using (var context = new FleetManagementDbEntities())
            {
                context.Drivers.Add(newDriver);
                context.SaveChanges();
            }
            LoadDrivers(); 
        }

        private void EditDriver_Click(object sender, RoutedEventArgs e)
        {
            if (DriversDataGrid.SelectedItem is Drivers selectedDriver)
            {
                EditDriverWindow editWindow = new EditDriverWindow(selectedDriver);

                if (editWindow.ShowDialog() == true)
                {
                    using (var context = new FleetManagementDbEntities())
                    {
                        context.Entry(selectedDriver).State = EntityState.Modified;
                        context.SaveChanges();
                    }

                    LoadDrivers();
                }
            }
        }


        private void DeleteDriver_Click(object sender, RoutedEventArgs e)
        {
            if (DriversDataGrid.SelectedItem is Drivers selectedDriver)
            {
                DeleteDriver(selectedDriver.Id);
            }
        }

        private void DeleteDriver(int driverId)
        {
            using (var context = new FleetManagementDbEntities())
            {
                var driver = context.Drivers.Find(driverId);
                if (driver != null)
                {
                    context.Drivers.Remove(driver);
                    context.SaveChanges();
                }
            }
            LoadDrivers(); 
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
          ((MainWindow)Application.Current.MainWindow).ShowMenuPanel();
          ((MainWindow)Application.Current.MainWindow).MainFrame.Content = null;
            
        }
    }
}
