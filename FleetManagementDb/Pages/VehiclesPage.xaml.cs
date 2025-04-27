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

namespace FleetManagementDb.Pages
{
    public partial class VehiclesPage : Page
    {
        public VehiclesPage()
        {
            InitializeComponent();
            LoadVehicles();
        }

        private void LoadVehicles()
        {
            using (var context = new FleetManagementDbEntities())
            {
                var vehicles = context.Vehicles.ToList();
                VehiclesDataGrid.ItemsSource = vehicles;
            }
        }
        private void AddVehicle_Click(object sender, RoutedEventArgs e)
        {
            Vehicles newVehicle = new Vehicles
            {
                LicensePlate = "Новый номер",
                Model = "Новая модель",
                Manufacturer = "Производитель",
                YearOfManufacture = DateTime.Now.Year,
                Mileage = 0,
                Status = "Доступен"
            };
            AddVehicle(newVehicle);
        }

        private void AddVehicle(Vehicles newVehicle)
        {
            using (var context = new FleetManagementDbEntities())
            {
                context.Vehicles.Add(newVehicle);
                context.SaveChanges();
            }
            LoadVehicles(); 
        }
        private void EditVehicle_Click(object sender, RoutedEventArgs e)
        {
            if (VehiclesDataGrid.SelectedItem is Vehicles selectedVehicle)
            {
                EditVehicleWindow editWindow = new EditVehicleWindow(selectedVehicle);

                if (editWindow.ShowDialog() == true)
                {
                    using (var context = new FleetManagementDbEntities())
                    {
                        context.Entry(selectedVehicle).State = EntityState.Modified;
                        context.SaveChanges();
                    }

                    LoadVehicles();
                }
            }
        }



        private void DeleteVehicle_Click(object sender, RoutedEventArgs e)
        {
            if (VehiclesDataGrid.SelectedItem is Vehicles selectedVehicle)
            {
                DeleteVehicle(selectedVehicle.Id);
            }
        }

        private void DeleteVehicle(int vehicleId)
        {
            using (var context = new FleetManagementDbEntities())
            {
                var vehicle = context.Vehicles.Find(vehicleId);
                if (vehicle != null)
                {
                    context.Vehicles.Remove(vehicle);
                    context.SaveChanges();
                }
            }
            LoadVehicles();
        }
        private void MileageReport_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new FleetManagementDbEntities())
            {
                // Получаем список транспортных средств с пробегом
                var vehicles = context.Vehicles
                    .OrderBy(v => v.Manufacturer)
                    .ThenBy(v => v.Model)
                    .ToList();

                // Формируем текст отчета
                StringBuilder report = new StringBuilder();
                report.AppendLine("ОТЧЕТ О СУММАРНОМ ПРОБЕГЕ ТРАНСПОРТНЫХ СРЕДСТВ");
                report.AppendLine($"Дата формирования: {DateTime.Now:dd.MM.yyyy HH:mm}");
                report.AppendLine();
                report.AppendLine("------------------------------------------------");
                report.AppendLine("| № | Гос. номер | Модель | Производитель | Пробег |");
                report.AppendLine("------------------------------------------------");

                int counter = 1;
                foreach (var vehicle in vehicles)
                {
                    report.AppendLine($"| {counter++} | {vehicle.LicensePlate} | {vehicle.Model} | {vehicle.Manufacturer} | {vehicle.Mileage} км |");
                }

                report.AppendLine("------------------------------------------------");
                report.AppendLine();
                report.AppendLine($"Всего транспортных средств: {vehicles.Count}");
                report.AppendLine($"Общий пробег: {vehicles.Sum(v => v.Mileage)} км");

                // Показываем отчет в диалоговом окне
                ReportWindow reportWindow = new ReportWindow("Отчет по пробегу транспортных средств", report.ToString());
                reportWindow.ShowDialog();
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
           ((MainWindow)Application.Current.MainWindow).ShowMenuPanel();
           ((MainWindow)Application.Current.MainWindow).MainFrame.Content = null;
            
        }
    }
}
