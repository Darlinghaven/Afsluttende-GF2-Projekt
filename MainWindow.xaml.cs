using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Afsluttende_GF2_Projekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenHome(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new Views.Home();
        }
        private void OpenPass(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new Password();
        }

        private void OpenKPI(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new Views.KPI();
        }

        private void Toggle_btn_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}