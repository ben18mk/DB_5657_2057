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

namespace DB_5657_2057
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Routes : Window
    {
        private List<string> routes;
        public Routes()
        {
            #region Pre-Load
            routes = (from route in SQL.Query("SELECT r.route_id AS id, ss.name AS origin, es.name AS destination " +
                                                 "FROM stations ss, stations es, routes r " +
                                                 "WHERE ss.station_id = r.start_station_id AND es.station_id = r.end_station_id")
                      let casted = (object[])route
                      select $"{casted[0]}  {casted[1]} ---> {casted[2]}").ToList();
            #endregion

            InitializeComponent();

            lbRoutes.ItemsSource = routes;
        }

        private void lbRoutes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string selectedRoute = lbRoutes.SelectedItem.ToString();

            new Lines(int.Parse(selectedRoute.Substring(0, selectedRoute.IndexOf(" ")))).ShowDialog();
        }

    }
}
