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
        private List<RouteRepresentation> routes;
        public Routes()
        {
            #region Pre-Load
            routes = (from route in SQL.Query(SQL.LoadQuery("../../Queries/RouteInformation.sql"))
                      let casted = (object[]) route
                      select new RouteRepresentation(casted)).ToList();
            #endregion

            InitializeComponent();

            lbRoutes.ItemsSource = routes;
        }

        private void lbRoutes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new Lines((RouteRepresentation)lbRoutes.SelectedItem).ShowDialog();
        }

    }
}
