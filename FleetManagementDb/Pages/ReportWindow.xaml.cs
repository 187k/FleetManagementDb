using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Microsoft.Win32;

namespace FleetManagementDb.Pages
{

    public partial class ReportWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private string _reportDate;
        public string ReportDate
        {
            get => _reportDate;
            set
            {
                _reportDate = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _reportLines;
        public ObservableCollection<string> ReportLines
        {
            get => _reportLines;
            set
            {
                _reportLines = value;
                OnPropertyChanged();
            }
        }

        public ReportWindow(string title, string reportText)
        {
            InitializeComponent();

            Title = title;
            ReportDate = $"Дата формирования: {DateTime.Now:dd.MM.yyyy HH:mm}";
            ReportLines = new ObservableCollection<string>(reportText.Split(new[] { Environment.NewLine }, StringSplitOptions.None));

            DataContext = this;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция печати будет реализована в следующей версии", "Печать", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ExportToPdf_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                Title = "Экспорт отчета в PDF",
                FileName = $"Отчет по пробегу_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (var fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate());
                        var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, fs);

                        document.Open();

                        document.AddTitle(Title);
                        document.AddSubject("Отчет о пробеге транспортных средств");
                        document.AddKeywords("Fleet Management, Отчет, Пробег");
                        document.AddCreator("Fleet Management System");
                        document.AddAuthor(Environment.UserName);

                        // шрифт
                        var baseFont = iTextSharp.text.pdf.BaseFont.CreateFont(
                            @"C:\Windows\Fonts\arial.ttf",
                            iTextSharp.text.pdf.BaseFont.IDENTITY_H,
                            iTextSharp.text.pdf.BaseFont.EMBEDDED);
                        var titleFont = new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD);
                        var headerFont = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.BOLD);
                        var normalFont = new iTextSharp.text.Font(baseFont, 10);
                        var smallFont = new iTextSharp.text.Font(baseFont, 8);

                        var titleParagraph = new iTextSharp.text.Paragraph(Title, titleFont)
                        {
                            Alignment = iTextSharp.text.Element.ALIGN_CENTER,
                            SpacingAfter = 20
                        };
                        document.Add(titleParagraph);

                        var dateParagraph = new iTextSharp.text.Paragraph(ReportDate, normalFont)
                        {
                            Alignment = iTextSharp.text.Element.ALIGN_CENTER,
                            SpacingAfter = 15
                        };
                        document.Add(dateParagraph);

                        var table = new iTextSharp.text.pdf.PdfPTable(5) // 5 колонок
                        {
                            WidthPercentage = 100,
                            SpacingBefore = 10,
                            SpacingAfter = 15
                        };

                        // настройка колонок
                        float[] columnWidths = { 15f, 20f, 25f, 15f, 15f };
                        table.SetWidths(columnWidths);

                        // заголовки таблицы
                        AddHeaderCell(table, "Гос. номер", headerFont);
                        AddHeaderCell(table, "Производитель", headerFont);
                        AddHeaderCell(table, "Модель", headerFont);
                        AddHeaderCell(table, "Год выпуска", headerFont);
                        AddHeaderCell(table, "Пробег (км)", headerFont);

                        foreach (var line in ReportLines)
                        {
                            if (line.Contains("|") && !line.Contains("===") && !line.Contains("ОТЧЕТ"))
                            {
                                var parts = line.Split('|')
                                    .Select(p => p.Trim())
                                    .Where(p => !string.IsNullOrEmpty(p))
                                    .ToArray();

                                if (parts.Length == 5)
                                {
                                    AddCell(table, parts[0], normalFont);
                                    AddCell(table, parts[1], normalFont);
                                    AddCell(table, parts[2], normalFont);
                                    AddCell(table, parts[3], normalFont);
                                    AddCell(table, parts[4], normalFont);
                                }
                            }
                        }

                        document.Add(table);

                        // инфа
                        var summary = ReportLines
                            .Where(line => line.StartsWith("Всего") || line.StartsWith("Общий") || line.StartsWith("Средний"))
                            .ToList();

                        foreach (var line in summary)
                        {
                            document.Add(new iTextSharp.text.Paragraph(line, normalFont));
                        }

                        var footerParagraph = new iTextSharp.text.Paragraph(
                            $"Сформировано в Fleet Management System, {DateTime.Now:dd.MM.yyyy HH:mm}", smallFont)
                        {
                            Alignment = iTextSharp.text.Element.ALIGN_RIGHT,
                            SpacingBefore = 20
                        };
                        document.Add(footerParagraph);

                        document.Close();
                    }

                    MessageBox.Show("Отчет успешно экспортирован в PDF", "Экспорт завершен",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    // открытие PDF после создания
                    Process.Start(saveFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при экспорте в PDF: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AddHeaderCell(iTextSharp.text.pdf.PdfPTable table, string text, iTextSharp.text.Font font)
        {
            var cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(text, font))
            {
                BackgroundColor = new iTextSharp.text.BaseColor(106, 53, 177),
                HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,
                VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,
                Padding = 5
            };
            table.AddCell(cell);
        }

        private void AddCell(iTextSharp.text.pdf.PdfPTable table, string text, iTextSharp.text.Font font)
        {
            var cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(text, font))
            {
                HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,
                VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,
                Padding = 5
            };
            table.AddCell(cell);
        }

        private void MileageReport_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new FleetManagementDbEntities())
            {
                var vehicles = context.Vehicles
                    .OrderBy(v => v.Manufacturer)
                    .ThenBy(v => v.Model)
                    .ToList();

                StringBuilder report = new StringBuilder();
                report.AppendLine("ОТЧЕТ О СУММАРНОМ ПРОБЕГЕ ТРАНСПОРТНЫХ СРЕДСТВ");
                report.AppendLine();
                report.AppendLine(new string('=', 80));
                report.AppendLine(string.Format("| {0,-10} | {1,-15} | {2,-20} | {3,-15} | {4,-10} |",
                    "Гос.номер", "Производитель", "Модель", "Год выпуска", "Пробег (км)"));
                report.AppendLine(new string('=', 80));

                foreach (var vehicle in vehicles)
                {
                    report.AppendLine(string.Format("| {0,-10} | {1,-15} | {2,-20} | {3,-15} | {4,-10} |",
                        vehicle.LicensePlate,
                        vehicle.Manufacturer,
                        vehicle.Model,
                        vehicle.YearOfManufacture,
                        vehicle.Mileage));
                }

                report.AppendLine(new string('=', 80));
                report.AppendLine();
                report.AppendLine($"Всего транспортных средств: {vehicles.Count}");
                report.AppendLine($"Общий пробег всех ТС: {vehicles.Sum(v => v.Mileage):N0} км");
                report.AppendLine($"Средний пробег: {vehicles.Average(v => v.Mileage):N0} км");

                var reportWindow = new ReportWindow("Отчет по пробегу транспортных средств", report.ToString());
                reportWindow.ShowDialog();
            }
        }
    }

}
