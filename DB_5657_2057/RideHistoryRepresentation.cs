using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_5657_2057
{
    /// <summary>
    /// Class to represent ride history information
    /// </summary>
    public class RideHistoryRepresentation
    {
        public string Date { get; }
        public string LineID { get; }
        public string Start { get; }
        public string End { get; }
        public string Price { get; }
        public string PaymentMethod { get; }

        /// <summary>
        /// Constructor to initialize RideHistoryRepresentation object
        /// </summary>
        /// <param name="info">info object retrieved from the database</param>
        public RideHistoryRepresentation(object[] info)
        {
            string[] splittedDate = info[0].ToString().Split(' ')[0].Split('/');

            Date = $"{splittedDate[1]}.{splittedDate[0]}.{splittedDate[2]}";
            LineID = info[1].ToString();
            Start = info[2].ToString();
            End = info[3].ToString();
            Price = $"₪ {info[4]}";
            PaymentMethod = info[5].ToString().ToUpper();
        }
    }
}
