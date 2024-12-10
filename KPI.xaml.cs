using System.Windows;
using System.Windows.Controls;
using Microsoft.Office.Interop.Excel;
using Range = Microsoft.Office.Interop.Excel.Range;

namespace Afsluttende_GF2_Projekt.Views
{
    /// <summary>
    /// Interaction logic for KPI.xaml
    /// </summary>
    public partial class KPI : UserControl
    {
        public KPI()
        {
            InitializeComponent();
        }

        private void Data_Button_Click(object sender, RoutedEventArgs e)
        {
            readExcel();
            if (ExcelDataGrid.Visibility == Visibility.Hidden)
            {
                ExcelDataGrid.Visibility = Visibility.Visible;
            }
            else
            {
                ExcelDataGrid.Visibility = Visibility.Hidden;
                ExcelDataGrid.ItemsSource = null;
            }
        }

        private void readExcel()
        {
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "KPI - Data.xlsx");

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = null;
            Worksheet ws = null;

            try
            {
                wb = excel.Workbooks.Open(filePath);
                ws = wb.Worksheets[1];

                // Brug UsedRange til at finde celler med data
                Range usedRange = ws.UsedRange;
                int rowCount = usedRange.Rows.Count;
                int colCount = usedRange.Columns.Count;

                // Opret en tabel til at holde data
                System.Data.DataTable dataTable = new System.Data.DataTable();

                // Tilføj kolonnenavne fra første række
                for (int col = 1; col <= colCount; col++)
                {
                    string columnName = usedRange.Cells[1, col].Value2?.ToString() ?? $"Column {col}";
                    dataTable.Columns.Add(columnName);
                }

                // Tilføj data fra resten af rækkerne
                for (int row = 2; row <= rowCount; row++) // Start fra række 2 for at springe overskrifter over
                {
                    var dataRow = dataTable.NewRow();
                    bool hasData = false;

                    for (int col = 1; col <= colCount; col++)
                    {
                        object cellValue = usedRange.Cells[row, col].Value2;
                        if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
                        {
                            dataRow[col - 1] = cellValue.ToString();
                            hasData = true; // Marker, at der er data i denne række
                        }
                    }

                    // Tilføj kun rækker med data til tabellen
                    if (hasData)
                    {
                        dataTable.Rows.Add(dataRow);
                    }
                }

                // Bind datatabellen til DataGrid
                ExcelDataGrid.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl ved indlæsning af Excel: {ex.Message}");
            }
            finally
            {
                wb?.Close(false);
                excel.Quit();
            }
        }
    }
}
