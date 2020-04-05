using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRace.Data.POCOs
{
    public class RaceList
    {
        public int RaceID { get; set; }
        public DateTime RaceTime { get ; set; }
        public string Competition { get; set; }
        public string Run { get; set; }
        public int NumberOfDrivers { get; set; }
    }
}
