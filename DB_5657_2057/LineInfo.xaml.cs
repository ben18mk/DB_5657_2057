using MySql.Data.MySqlClient;
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
    /// Interaction logic for LineInfo.xaml
    /// </summary>
    public partial class LineInfo : Window
    {
        private int lineId;
        public LineInfo(int lineId)
        {
            #region Pre-Load
            this.lineId = lineId;

            List<object> result = SQL.Procedure("GetLineInfo",
              new List<List<object>>()
              {
                  new List<object>()
                  {
                      "line_id",
                      MySqlDbType.Int32,
                      lineId
                  }
              },
              new List<List<object>>()
              {
                  new List<object>()
                  {
                      "bike_accessible",
                      MySqlDbType.Int32
                  },
                  new List<object>()
                  {
                      "dist",
                      MySqlDbType.Decimal
                  },
                  new List<object>()
                  {
                      "travel_time",
                      MySqlDbType.Time
                  },
                  new List<object>()
                  {
                      "type_name",
                      MySqlDbType.VarChar
                  }
              }).ToList();

            List<object[]> counts = (from countInformation in SQL.Query(SQL.LoadQuery("../../Queries/CountTicketsPerLineTotal.sql"))
                                     let casted = (object[])countInformation
                                     where (int)casted[0] == lineId
                                     select casted).ToList();

            List<string> dates = (from date in SQL.Query(SQL.LoadQuery("../../Queries/GetOrderedDays.sql", lineId))
                                  let casted = (object[])date
                                  let dayDate = casted[0].ToString().Split(' ')[0].Split('/')
                                  select $"{dayDate[1]}.{dayDate[0]}.{dayDate[2]}").ToList();
            #endregion

            InitializeComponent();
            tblLineId.Text = lineId.ToString();
            tblBikeAccessible.Text = (int)result[0] == 0 ? "Yes" : "No";
            tblDist.Text = $"{result[1]} km";
            tblTravelTime.Text = $"{result[2].ToString().Split(':')[0]}h {result[2].ToString().Split(':')[1]}m";
            tblTypeName.Text = result[3].ToString();
            tblTotalTicketsSold.Text = counts[0][1].ToString();
            cmbxDates.ItemsSource = dates;
        }

        private void cmbxDates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string[] splittedDate = cmbxDates.SelectedItem.ToString().Split('.');
            tblTicketsSold.Text = (from item in SQL.Query(SQL.LoadQuery("../../Queries/CountTicketsPerLineDay.sql",
                                                                        $"'{splittedDate[2]}-{splittedDate[1]}-{splittedDate[0]}'"))
                                   let casted = (object[])item
                                   where (int)casted[0] == lineId
                                   select Convert.ToInt32(casted[1])).First().ToString();
        }
    }
}