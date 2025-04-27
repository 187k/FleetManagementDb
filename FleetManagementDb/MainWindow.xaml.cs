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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FleetManagementDb.Pages;


namespace FleetManagementDb
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        public void ShowMenuPanel()
        {
            MenuPanel.Visibility = Visibility.Visible;
        }

        private void HideMenuPanel()
        {
            MenuPanel.Visibility = Visibility.Collapsed;
        }

        private void ShowVehiclesPage_Click(object sender, RoutedEventArgs e)
        {
            HideMenuPanel();
            MainFrame.Navigate(new Pages.VehiclesPage());
        }

        private void ShowDriversPage_Click(object sender, RoutedEventArgs e)
        {
            HideMenuPanel();
            MainFrame.Navigate(new DriversPage());
        }

        private void RoutesPageButton_Click(object sender, RoutedEventArgs e)
        {
            HideMenuPanel();
            MainFrame.Content = new RoutesPage();
        }
    }

}
