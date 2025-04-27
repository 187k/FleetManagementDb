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
    /// <summary>
    /// Логика взаимодействия для PdfExportConfigWindow.xaml
    /// </summary>
    public partial class PdfExportConfigWindow : Window
    {
        public bool IsLandscape { get; private set; }

        public PdfExportConfigWindow(bool currentOrientation)
        {
            InitializeComponent();
            LandscapeRadio.IsChecked = currentOrientation;
            PortraitRadio.IsChecked = !currentOrientation;
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            IsLandscape = LandscapeRadio.IsChecked == true;
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
