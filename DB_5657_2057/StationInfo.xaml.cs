// TODO: IMPLEMENT

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for StationInfo.xaml
    /// </summary>
    public partial class StationInfo : Window
    {
        private List<string> routes;

        public StationInfo(object[] station)
        {
            station = (object[])station[0];

            string stationRoutesQuery = "";
            using (StreamReader sr = new StreamReader("../../Queries/StationRoutes.sql"))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(sr.ReadToEnd().Replace("\r", "").Replace("\n", " "), station[0]);
                stationRoutesQuery = sb.ToString();
            }

            routes = (from route in SQL.Query(stationRoutesQuery)
                     let casted = (object[])route
                     select $"{casted[0]} ---> {casted[1]}").ToList();


            InitializeComponent();

            tblID.Text = station[0].ToString();
            tblName.Text = station[1].ToString();
            tblLocation.Text = $"{station[2]}, {station[3]}";

            lbRoutes.ItemsSource = routes;
        }

        private void tblLocation_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new Uri($"https://www.google.com/maps/search/?api=1&query={tblLocation.Text}").ToString());
        }
    }
}
