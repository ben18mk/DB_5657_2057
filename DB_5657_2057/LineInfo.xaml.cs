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
        public LineInfo(int LineId)
        {
            #region Pre-Load
            List<object> result = SQL.Procedure("GetLineInfo",
              new List<List<object>>()
              {
                  new List<object>()
                  {
                      "line_id",
                      MySqlDbType.Int32,
                      LineId
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
                                     where (int)casted[0] == LineId
                                     select casted).ToList();

            List<string> dates = (from date in SQL.Query(SQL.LoadQuery("../../Queries/GetOrderedDays.sql", LineId))
                                  let casted = (object[])date
                                  select casted[0].ToString()).ToList();
            #endregion

            InitializeComponent();
            tblLineId.Text = LineId.ToString();
            if ((int)result[0] == 0)
                tblBikeAccessible.Text = "Yes";
            else
                tblBikeAccessible.Text = "No";
            tblDist.Text = result[1].ToString();
            tblTravelTime.Text = result[2].ToString();
            tblTypeName.Text = result[3].ToString();
            tblTotal_Tickets_Sold.Text = counts[0][1].ToString();
            cmbxDates.ItemsSource = dates;
        }
    }
}