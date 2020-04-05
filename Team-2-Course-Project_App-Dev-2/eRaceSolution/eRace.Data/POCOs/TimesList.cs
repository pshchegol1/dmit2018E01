using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRace.Data.POCOs
{
    public class TimesList
    {
        public int RaceDetailID { get; set; }
        public string FullName { get; set; }
        public TimeSpan? Time { get; set; }
        public int? Penalty { get; set; }
    }
}
