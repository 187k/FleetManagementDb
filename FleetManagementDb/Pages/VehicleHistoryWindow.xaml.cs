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
    public partial class VehicleHistoryWindow : Window
    {
        private int _vehicleId;
        public VehicleHistoryWindow(int vehicleId)
        {
            InitializeComponent();
            _vehicleId = vehicleId;
            LoadVehicleHistory();
        }

        private void LoadVehicleHistory()
        {
            using (var context = new FleetManagementDbEntities())
            {
                var history = context.Routes
                    .Where(r => r.VehicleId == _vehicleId)
                    .Include(r => r.Drivers)
                    .ToList();
                VehicleHistoryDataGrid.ItemsSource = history;
            }
        }
    }
}
