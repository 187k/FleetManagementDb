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
using System.Data.Entity;

namespace FleetManagementDb.Pages
{
    public partial class RouteHistoryWindow : Window
    {
        public RouteHistoryWindow(int id, string type)
        {
            InitializeComponent();
            LoadHistory(id, type);
        }

        private void LoadHistory(int id, string type)
        {
            using (var context = new FleetManagementDbEntities())
            {
                List<Routes> history;

                if (type == "Vehicle")
                {
                    history = context.Routes
                        .Include(r => r.Vehicles)  
                        .Include(r => r.Drivers)   
                        .Where(r => r.VehicleId == id)
                        .OrderBy(r => r.StartDate)
                        .ToList();
                }
                else 
                {
                    history = context.Routes
                        .Include(r => r.Vehicles)  
                        .Include(r => r.Drivers)   
                        .Where(r => r.DriverId == id)
                        .OrderBy(r => r.StartDate)
                        .ToList();
                }

                HistoryDataGrid.ItemsSource = history;
            }
        }
    }
}
