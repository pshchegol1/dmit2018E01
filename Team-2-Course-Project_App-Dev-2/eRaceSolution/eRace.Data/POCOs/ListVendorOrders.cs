using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRace.Data.POCOs
{
    public class ListVendorOrders
    {
        public int OrderDetailID { get; set; }
        public int ProductID { get; set; }
        public int OrderID { get; set; }
        public string ProductName { get; set; }
        public int OrderQuantity { get; set; }
        public int UnitSize { get; set; }
        public decimal UnitCost { get; set; }
        public decimal PerItemCost { get; set; }
        public decimal ExtendedCost { get; set; }
    }
}
