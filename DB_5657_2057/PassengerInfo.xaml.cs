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
    /// Interaction logic for PassengerInfo.xaml
    /// </summary>
    public partial class PassengerInfo : Window
    {
        private PassengerRepresentation passenger;
        private IEnumerable<RideHistoryRepresentation> rideHistory;
        private object[] paymentStatistics;

        public PassengerInfo(PassengerRepresentation passenger)
        {
            #region Pre-Load
            this.passenger = passenger;

            rideHistory = from item in SQL.Query(SQL.LoadQuery("../../Queries/PassengerRideHistory.sql", passenger.ID))
                          let casted = (object[])item
                          select new RideHistoryRepresentation(casted);

            paymentStatistics = SQL.Procedure("CountPassengerPaymentMethod",
                new List<List<object>>()
                {
                    new List<object>()
                    {
                        "passenger_id",
                        MySqlDbType.Int32,
                        passenger.ID
                    }
                },
                new List<List<object>>()
                {
                    new List<object>()
                    {
                        "cash_count",
                        MySqlDbType.Int32
                    },
                    new List<object>()
                    {
                        "card_count",
                        MySqlDbType.Int32
                    },
                    new List<object>()
                    {
                        "total_payments",
                        MySqlDbType.Int32
                    }
                }).ToArray();
            #endregion

            InitializeComponent();

            tbxID.Text = passenger.ID;
            tbxName.Text = passenger.Name;
            lbRideHistory.ItemsSource = rideHistory;
            tblCash.Text = paymentStatistics[0].ToString();
            tblCard.Text = paymentStatistics[1].ToString();
            tblTotal.Text = paymentStatistics[2].ToString();
        }
    }
}
