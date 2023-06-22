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
    public class PassengerRepresentation
    {
        public string ID { get; }
        public string Name { get; }
        public string Email { get; }
        public string Phone { get; }
        public string Address { get; }

        /// <summary>
        /// Constructor to initialize RouteRepresentation object
        /// </summary>
        /// <param name="info">info object retrieved from the database</param>
        public PassengerRepresentation(object[] info)
        {
            ID = info[0].ToString();
            Name = info[1].ToString();
            Email = info[2].ToString();
            Phone = info[3].ToString();
            Address = info[4].ToString();
        }
    }
}
