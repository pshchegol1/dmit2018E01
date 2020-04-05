using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRace.Data.POCOs
{
    public class VendorCatalogPOCO
    {
        //public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Reorder { get; set; }
        public int InStock { get; set; }
        //public decimal ItemPrice { get; set; }
        public int OnOrder { get; set; }
        public string Size { get; set; }
    }
}
