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
    /// Interaction logic for Passengers.xaml
    /// </summary>
    public partial class Passengers : Window
    {
        private List<PassengerRepresentation> passengers;

        public Passengers()
        {
            #region Pre-Load
            passengers = (from passenger in SQL.Query(SQL.LoadQuery("../../Queries/GetPassengers.sql"))
                          let casted = (object[])passenger
                          select new PassengerRepresentation(casted)).ToList();
            #endregion

            InitializeComponent();

            lbPassengers.ItemsSource = passengers;
        }

        private void tbxIdFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) &&
                !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                e.Key == Key.Space)
                e.Handled = true;
        }

        private void tbxNameFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.Key >= Key.A && e.Key <= Key.Z) &&
                !(e.Key == Key.Space || e.Key == Key.LeftShift || e.Key == Key.OemMinus))
                e.Handled = true;
        }

        private void lbPassengers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbPassengers.SelectedItem == null)
                return;

            new PassengerInfo((PassengerRepresentation)lbPassengers.SelectedItem).ShowDialog();
        }

        private void btnIdFilterC_Click(object sender, RoutedEventArgs e)
        {
            lbPassengers.ItemsSource = tbxIdFilter.Text == "" ?
                passengers : passengers.Where(x => x.ID.Contains(tbxIdFilter.Text));
        }

        private void btnIdFilterS_Click(object sender, RoutedEventArgs e)
        {
            lbPassengers.ItemsSource = tbxIdFilter.Text == "" ?
                passengers : passengers.Where(x => x.ID.StartsWith(tbxIdFilter.Text));
        }

        private void btnNameFilterS_Click(object sender, RoutedEventArgs e)
        {
            lbPassengers.ItemsSource = tbxNameFilter.Text == "" ?
                passengers : passengers.Where(x => x.Name.ToLower().StartsWith(tbxNameFilter.Text.ToLower()));
        }

        private void btnNameFilterC_Click(object sender, RoutedEventArgs e)
        {
            lbPassengers.ItemsSource = tbxNameFilter.Text == "" ?
                passengers : passengers.Where(x => x.Name.ToLower().Contains(tbxNameFilter.Text.ToLower()));
        }
    }
}
