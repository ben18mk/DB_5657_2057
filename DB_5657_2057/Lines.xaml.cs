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
        private List<string> lines_id;
        public Lines(int route_id)
        {
            #region Pre-Load
            lines_id = (from line in SQL.Query("SELECT line_id " +
                                                          "FROM route_tlines rtl " +
                                                          $"WHERE rtl.route_id = {route_id}")
                                     let casted = (object[])line
                                     select $"{casted[0]}").ToList();
            #endregion
            InitializeComponent();

            lbLines.ItemsSource = lines_id;
        }

        private void lbLine_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            return;
        }
    }
}
