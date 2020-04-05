using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRace.Data.POCOs
{
    public class OrderList
    {
        public int ProductID { get; set; }
        public string Item { get; set; }
        public double QuantityOrdered { get; set; }
        public string OrderedUnits { get; set; }
        public double QuantityOutstanding { get; set; }
        public int ReceivedUnits { get; set; }
        public string ReceivedUnitsLabel { get; set; }
        public int RejectedUnits { get; set; }
        public string Reason { get; set; }
        public int SalvagedItems { get; set; }
    }
}
