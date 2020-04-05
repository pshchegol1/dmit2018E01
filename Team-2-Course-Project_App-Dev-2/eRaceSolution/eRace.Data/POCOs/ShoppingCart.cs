using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRace.Data.POCOs
{
    public class ShoppingCart
    {
        public int ProductID { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }

        public decimal Price { get; set; }
        public decimal Amount { get; set; }

    }
}
