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
        private object[] station;

        private List<string> routes;
        private List<UpcommingTrain> upcommingTrains;

        public StationInfo(object[] station)
        {
            #region Pre-Load
            this.station = (object[])station[0];

            routes = (from route in SQL.Query(loadQuery("../../Queries/StationRoutes.sql", this.station[0]))
                      let casted = (object[]) route
                      select $"{casted[0]} ---> {casted[1]}").ToList();

            upcommingTrains = (from upcommingTrain in SQL.Query(loadQuery("../../Queries/StationUpcommingTrains.sql", this.station[0]))
                               let casted = (object[])upcommingTrain
                               select new UpcommingTrain(casted)).ToList();
            #endregion


            InitializeComponent();

            tblID.Text = this.station[0].ToString();
            tblName.Text = this.station[1].ToString();
            tblLocation.Text = $"{this.station[2]}, {this.station[3]}";

            lbRoutes.ItemsSource = routes;
            lbUpcommingTrains.ItemsSource = upcommingTrains;
        }

        private string loadQuery(string strQueryFile, params object[] info)
        {
            string query = "";
            using (StreamReader sr = new StreamReader(strQueryFile))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(sr.ReadToEnd().Replace("\r", "").Replace("\n", " "), info);
                query = sb.ToString();
            }

            return query;
        }

        private void tblLocation_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new Uri($"https://www.google.com/maps/search/?api=1&query={tblLocation.Text}").ToString());
        }

        private void tbxLineId_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key !=)
        }

        private void lbRoutes_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // TODO: Implement Route info
        }
    }
}
