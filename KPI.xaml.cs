using System;
using System.Data;
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

                // Use UsedRange to find cells with data
                Range usedRange = ws.UsedRange;
                int rowCount = usedRange.Rows.Count;
                int colCount = usedRange.Columns.Count;

                // Create a DataTable to hold the data
                System.Data.DataTable dataTable = new System.Data.DataTable();

                // Add columns to DataTable (using the first row as header)
                for (int col = 1; col <= colCount; col++)
                {
                    string columnName = usedRange.Cells[1, col].Value2?.ToString() ?? $"Column {col}";
                    dataTable.Columns.Add(columnName);
                }

                // Add data to the DataTable (skip the first row since it’s the header)
                for (int row = 2; row <= rowCount; row++) // Skip headers
                {
                    DataRow dataRow = dataTable.NewRow();

                    bool hasDataInRow = false;

                    for (int col = 1; col <= colCount; col++)
                    {
                        object cellValue = usedRange.Cells[row, col].Value2;

                        if (cellValue != null && !string.IsNullOrEmpty(cellValue.ToString()))
                        {
                            dataRow[col - 1] = cellValue.ToString();
                            hasDataInRow = true; // Mark if there's any data in this row
                        }
                        else
                        {
                            dataRow[col - 1] = DBNull.Value; // If no data, set it as null
                        }
                    }

                    // Only add rows with data
                    if (hasDataInRow)
                    {
                        dataTable.Rows.Add(dataRow);
                    }
                }

                // Bind the DataTable to the DataGrid
                ExcelDataGrid.ItemsSource = dataTable.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Excel data: {ex.Message}");
            }
            finally
            {
                wb?.Close(false);
                excel.Quit();
            }
        }
    }
}