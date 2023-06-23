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
    /// Interaction logic for TicketOrder.xaml
    /// </summary>
    public partial class TicketOrder : Window
    {
        private int routeId;
        private int lineId;
        private bool cash;

        public TicketOrder()
        {
            InitializeComponent();

            routeId = -1;
            lineId = -1;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (tbxPassengerID.Text == "")
            {
                MessageBox.Show("Passenger ID not specified",
                    "Oops",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (SQL.Query(SQL.LoadQuery("../../Queries/PassengerExists.sql", tbxPassengerID.Text)).Count() == 0)
                new RegistedPassenger(int.Parse(tbxPassengerID.Text)).ShowDialog();
            else
            {
                gLogin.IsEnabled = false;
                gRoutes.Visibility = Visibility.Visible;
            }
        }

        private void gRoutes_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            switch (gRoutes.Visibility)
            {
                case Visibility.Visible:
                    lbRoutes.ItemsSource = from item in SQL.Query(SQL.LoadQuery("../../Queries/RouteInformation.sql"))
                                           let casted = (object[])item
                                           select $"{casted[1]} ---> {casted[2]}";
                    break;
                default:
                    lbRoutes.ItemsSource = null;
                    break;
            }
        }

        private void lbRoutes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbRoutes.SelectedItem == null)
                return;

            routeId = lbRoutes.SelectedIndex + 1;
            gRoutes.Visibility = Visibility.Collapsed;
            gLines.Visibility = Visibility.Visible;
        }

        private void gLines_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            switch (gLines.Visibility)
            {
                case Visibility.Visible:
                    lbLines.ItemsSource = from item in SQL.Query(SQL.LoadQuery("../../Queries/GetRouteLines.sql", routeId))
                                          let casted = (object[])item
                                          select casted[0];
                    break;
                default:
                    lbLines.ItemsSource = null;
                    break;
            }
        }

        private void lbLines_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbLines.SelectedItem == null)
                return;

            lineId = int.Parse(lbLines.SelectedItem.ToString());
            gLines.Visibility = Visibility.Collapsed;
            gPayment.Visibility = Visibility.Visible;
        }

        private void gPayment_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            switch (gPayment.Visibility)
            {
                case Visibility.Visible:
                    tbxPrice.Text = SQL.Procedure("GetRoutePrice",
                        new List<List<object>>()
                        {
                            new List<object>()
                            {
                                "route_id",
                                MySqlDbType.Int32,
                                routeId
                            }
                        },
                        new List<List<object>>()
                        {
                            new List<object>()
                            {
                                "price",
                                MySqlDbType.Int32
                            }
                        }).First().ToString();
                    break;
                default:
                    tbxPrice.Text = "";
                    break;
            }
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            cash = (string)((Button)sender).Content == "Cash";

            object[] tpIds = SQL.Procedure("OrderTicket",
                new List<List<object>>()
                {
                    new List<object>()
                    {
                        "passenger_id",
                        MySqlDbType.Int32,
                        tbxPassengerID.Text
                    },
                    new List<object>()
                    {
                        "line_id",
                        MySqlDbType.Int32,
                        lineId
                    },
                    new List<object>()
                    {
                        "payment_method",
                        MySqlDbType.VarChar,
                        cash ? "cash" : "card"
                    },
                    new List<object>()
                    {
                        "pdate",
                        MySqlDbType.Date,
                        $"{DateTime.Now.Year:0000}-{DateTime.Now.Month:00}-{DateTime.Now.Day:00}"
                    }
                },
                new List<List<object>>()
                {
                    new List<object>()
                    {
                        "ticket_id",
                        MySqlDbType.Int32
                    },
                    new List<object>()
                    {
                        "payment_id",
                        MySqlDbType.Int32
                    }
                }).ToArray();

            tbxTicketID.Text = tpIds[0].ToString();
            tbxPaymentID.Text = tpIds[1].ToString();

            gPayment.Visibility = Visibility.Collapsed;
            gConfirmation.Visibility = Visibility.Visible;
        }

        private void tbxPassengerID_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) &&
                !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                e.Key == Key.Space)
                e.Handled = true;
        }
    }
}