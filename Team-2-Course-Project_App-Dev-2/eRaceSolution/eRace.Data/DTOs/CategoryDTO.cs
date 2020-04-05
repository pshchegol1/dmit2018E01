using eRace.Data.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRace.Data.DTOs
{
    public class CategoryDTO
    {
        public string Category { get; set; }
        public List<VendorCatalogPOCO> Products { get; set; }
    }
}
