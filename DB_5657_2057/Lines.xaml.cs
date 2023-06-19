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
    /// Interaction logic for Lines.xaml
    /// </summary>
    public partial class Lines : Window
    {
        private RouteRepresentation route;
        private List<string> lineIds;
        public Lines(RouteRepresentation route)
        {
            #region Pre-Load
            this.route = route;

            lineIds = (from line in SQL.Query(SQL.LoadQuery("../../Queries/GetRouteLines.sql", this.route.RouteID))
                     let casted = (object[])line
                     select casted[0].ToString()).ToList();
            #endregion
            InitializeComponent();

            lbLines.ItemsSource = lineIds;
        }

        private void lbLine_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            return;
        }
    }
}
