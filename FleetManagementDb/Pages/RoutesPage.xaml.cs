using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using System.Data.Entity;

namespace FleetManagementDb.Pages
{

    public partial class RoutesPage : Page
    {
        public RoutesPage()
        {
            InitializeComponent();
            UpdateVehicleStatusAfterRoute();
            LoadFilters();
            LoadRoutes();
        }

        private void LoadRoutes()
        {
            using (var context = new FleetManagementDbEntities())
            {
                var routesQuery = context.Routes
                    .Include(r => r.Vehicles)
                    .Include(r => r.Drivers)
                    .AsQueryable();

                if (StartDateFilter.SelectedDate.HasValue)
                {
                    routesQuery = routesQuery.Where(r => r.StartDate >= StartDateFilter.SelectedDate.Value);
                }

                if (EndDateFilter.SelectedDate.HasValue)
                {
                    routesQuery = routesQuery.Where(r => r.EndDate <= EndDateFilter.SelectedDate.Value);
                }

                if (VehicleFilter.SelectedItem is Vehicles selectedVehicle)
                {
                    routesQuery = routesQuery.Where(r => r.VehicleId == selectedVehicle.Id);
                }

                if (DriverFilter.SelectedItem is Drivers selectedDriver)
                {
                    routesQuery = routesQuery.Where(r => r.DriverId == selectedDriver.Id);
                }

                RoutesDataGrid.ItemsSource = routesQuery.ToList();
            }
        }
        private void LoadFilters()
        {
            using (var context = new FleetManagementDbEntities())
            {
                VehicleFilter.Items.Clear();
                var vehicles = context.Vehicles.ToList();
                VehicleFilter.ItemsSource = new List<Vehicles> { null }.Concat(vehicles);

                DriverFilter.Items.Clear();
                var drivers = context.Drivers.ToList();
                DriverFilter.ItemsSource = new List<Drivers> { null }.Concat(drivers);
            }
        }

        private void AddRoute_Click(object sender, RoutedEventArgs e)
        {
            Routes newRoute = new Routes
            {
                VehicleId = 1, 
                DriverId = 1,  
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                StartLocation = "Место отправления",
                EndLocation = "Место прибытия",
                Distance = 100.0f
            };
            AddRoute(newRoute);
        }

        private void AddRoute(Routes newRoute)
        {
            using (var context = new FleetManagementDbEntities())
            {
                var driverExists = context.Drivers.Any(d => d.Id == newRoute.DriverId);
                if (!driverExists)
                {
                    MessageBox.Show("Указанный водитель не существует.");
                    return;
                }

                var vehicleExists = context.Vehicles.Any(v => v.Id == newRoute.VehicleId);
                if (!vehicleExists)
                {
                    MessageBox.Show("Указанное транспортное средство не существует.");
                    return;
                }


                context.Routes.Add(newRoute);
                context.SaveChanges(); 
            }

            using (var updateContext = new FleetManagementDbEntities())
            {
                var vehicle = updateContext.Vehicles.Find(newRoute.VehicleId);
                if (vehicle != null)
                {
                    vehicle.Status = "В пути";
                    updateContext.SaveChanges(); 
                }
            }

            LoadRoutes(); 
        }

        private void EditRoute_Click(object sender, RoutedEventArgs e)
        {
            if (RoutesDataGrid.SelectedItem is Routes selectedRoute)
            {
                var editableRoute = new Routes
                {
                    Id = selectedRoute.Id,
                    VehicleId = selectedRoute.VehicleId,
                    DriverId = selectedRoute.DriverId,
                    StartDate = selectedRoute.StartDate,
                    EndDate = selectedRoute.EndDate,
                    StartLocation = selectedRoute.StartLocation,
                    EndLocation = selectedRoute.EndLocation,
                    Distance = selectedRoute.Distance
                };

                EditRouteWindow editWindow = new EditRouteWindow(editableRoute);

                if (editWindow.ShowDialog() == true)
                {
                    using (var context = new FleetManagementDbEntities())
                    {

                        var routeToUpdate = context.Routes.Find(editableRoute.Id);
                        if (routeToUpdate != null)
                        {
                            routeToUpdate.VehicleId = editableRoute.VehicleId;
                            routeToUpdate.DriverId = editableRoute.DriverId;
                            routeToUpdate.StartDate = editableRoute.StartDate;
                            routeToUpdate.EndDate = editableRoute.EndDate;
                            routeToUpdate.StartLocation = editableRoute.StartLocation;
                            routeToUpdate.EndLocation = editableRoute.EndLocation;
                            routeToUpdate.Distance = editableRoute.Distance;

                            context.SaveChanges();
                        }
                    }
                    LoadRoutes(); 
                }
            }
        }

        private void DeleteRoute_Click(object sender, RoutedEventArgs e)
        {
            if (RoutesDataGrid.SelectedItem is Routes selectedRoute)
            {
                DeleteRoute(selectedRoute.Id);
            }
        }

        private void DeleteRoute(int routeId)
        {
            using (var context = new FleetManagementDbEntities())
            {
                var route = context.Routes.Find(routeId);
                if (route != null)
                {
                    context.Routes.Remove(route);
                    context.SaveChanges();
                }
            }
            LoadRoutes(); 
        }

        private void UpdateVehicleStatusAfterRoute()
        {
            using (var context = new FleetManagementDbEntities())
            {
                var completedRoutes = context.Routes
                    .Where(r => r.EndDate < DateTime.Now)
                    .ToList();

                foreach (var route in completedRoutes)
                {
                    var vehicle = context.Vehicles.Find(route.VehicleId);
                    if (vehicle != null)
                    {
                        vehicle.Status = "Доступен";
                    }
                }
                context.SaveChanges();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).ShowMenuPanel();
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = null;
            
        }

        private void ShowVehicleHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (RoutesDataGrid.SelectedItem is Routes selectedRoute)
            {
                var vehicleHistoryWindow = new RouteHistoryWindow(selectedRoute.VehicleId, "Vehicle");
                vehicleHistoryWindow.ShowDialog();
            }
        }

        private void ShowDriverHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (RoutesDataGrid.SelectedItem is Routes selectedRoute)
            {
                var driverHistoryWindow = new RouteHistoryWindow(selectedRoute.DriverId, "Driver");
                driverHistoryWindow.ShowDialog();
            }
        }

        private void ApplyFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new FleetManagementDbEntities())
            {
                var startDate = StartDateFilter.SelectedDate;
                var endDate = EndDateFilter.SelectedDate;
                var selectedVehicle = VehicleFilter.SelectedItem as Vehicles;
                var selectedDriver = DriverFilter.SelectedItem as Drivers;

                var filteredRoutes = context.Routes
                    .Include(r => r.Vehicles)
                    .Include(r => r.Drivers)
                    .AsQueryable();

                if (startDate.HasValue)
                    filteredRoutes = filteredRoutes.Where(r => DbFunctions.TruncateTime(r.StartDate) >= DbFunctions.TruncateTime(startDate.Value));

                if (endDate.HasValue)
                    filteredRoutes = filteredRoutes.Where(r => DbFunctions.TruncateTime(r.EndDate) <= DbFunctions.TruncateTime(endDate.Value));

                if (selectedVehicle != null)
                    filteredRoutes = filteredRoutes.Where(r => r.VehicleId == selectedVehicle.Id);

                if (selectedDriver != null)
                    filteredRoutes = filteredRoutes.Where(r => r.DriverId == selectedDriver.Id);

                RoutesDataGrid.ItemsSource = filteredRoutes.ToList();
            }
        }

        private void ResetFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            StartDateFilter.SelectedDate = null;
            EndDateFilter.SelectedDate = null;
            VehicleFilter.SelectedIndex = -1; 
            DriverFilter.SelectedIndex = -1;  

            LoadRoutes(); 
        }
    }
}
