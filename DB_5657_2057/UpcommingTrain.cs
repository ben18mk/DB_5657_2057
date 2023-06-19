using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_5657_2057
{
    /// <summary>
    /// This class represents the info for an upcomming train in a station
    /// </summary>
    public class UpcommingTrain
    {
        public string LineID { get; }
        public string Destination { get; }
        public string OperationTime { get; }
        public string MinutesLeft { get; }

        /// <summary>
        /// Constructor to initialize an UpcommingTrain object
        /// </summary>
        /// <param name="info">info object retrieved from the database</param>
        public UpcommingTrain(object[] info)
        {
            LineID = info[0].ToString();
            Destination = info[1].ToString();
            OperationTime = info[2].ToString();
            MinutesLeft = info[3].ToString();
        }
    }
}
