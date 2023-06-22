using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace DB_5657_2057
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<object[]> stations;

        public MainWindow()
        {
            #region Pre-Load
            SQL.Login("localhost", "lev_project", "root", "");
            stations = (List<object[]>)SQL.Query("SELECT * FROM stations");
            #endregion

            InitializeComponent();

            lbStations.ItemsSource = stations.Select(x => x[1]).OrderBy(x => x);
        }

        private void lbStations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string selectedStationName = lbStations.SelectedItem.ToString();
            object[] station = (from s in stations
                               where s[1].ToString() == selectedStationName
                               select s).ToArray();

            new StationInfo(station).ShowDialog();
        }

        private void btnGetRoutes_Click(object sender, RoutedEventArgs e)
        {
            new Routes().ShowDialog();
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            new AdminPanel().ShowDialog();
        }
    }
}
