using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRace.Data.POCOs
{
    public class DriverList
    {
        public int RaceID { get; set; }
        public int RaceDetailID { get; set; }
        public string FullName { get; set; }
        public decimal RaceFee { get; set; }
        public decimal RentalFee { get; set; }
        public int? Placement { get; set; }
        public bool Refunded { get; set; }
    }
}
