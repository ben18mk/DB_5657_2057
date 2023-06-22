using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_5657_2057
{
    /// <summary>
    /// Class to represent line information
    /// </summary>
    public class RouteRepresentation
    {
        public string RouteID { get; }
        public string Start { get; }
        public string End { get; }
        public string Distance { get; }
        public string TravelTime { get; }
        public string Income { get; }

        /// <summary>
        /// Constructor to initialize RouteRepresentation object
        /// </summary>
        /// <param name="info">info object retrieved from the database</param>
        public RouteRepresentation(object[] info)
        {
            RouteID = info[0].ToString();
            Start = info[1].ToString();
            End = info[2].ToString();
            Distance = $"{info[3]} km";
            TravelTime = $"{info[4].ToString().Split(':')[0]} h {info[4].ToString().Split(':')[1]} m";
            Income = $"₪ {info[5]}";
        }
    }
}
