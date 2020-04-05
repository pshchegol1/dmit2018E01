using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRace.Data.POCOs
{
    public class InvoiceRefunds
    {

        public int InvoiceDetailID { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }

        public decimal Price { get; set; }
        public decimal Amount { get; set; }

        public decimal ReStockChg { get; set; }

        public string Reason { get; set; }
    }
}
