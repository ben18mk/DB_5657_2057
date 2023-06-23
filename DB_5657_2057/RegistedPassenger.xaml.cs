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
    /// Interaction logic for RegistedPassenger.xaml
    /// </summary>
    public partial class RegistedPassenger : Window
    {
        private int passengerId;

        public RegistedPassenger(int passengerId)
        {
            this.passengerId = passengerId;

            InitializeComponent();

            tbxID.Text = passengerId.ToString();
        }

        private void tbxName_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.Key >= Key.A && e.Key <= Key.Z) &&
                !(e.Key == Key.Space || e.Key == Key.LeftShift || e.Key == Key.OemMinus))
                e.Handled = true;
        }

        private void tbxPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) &&
                !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                e.Key == Key.Space)
                e.Handled = true;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            SQL.Query(SQL.LoadQuery("../../Queries/RegisterPassenger.sql",
                passengerId,
                tbxName.Text,
                tbxEmail.Text.ToLower(),
                tbxPhone.Text,
                tbxAddress.Text));

            MessageBox.Show("Registered!");
            Close();
        }
    }
}
