namespace eRace.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VendorCatalog
    {
        public int VendorCatalogID { get; set; }

        public int ProductID { get; set; }

        public int VendorID { get; set; }

        [Required(ErrorMessage = "Order Unit Type is required")]
        [StringLength(6, ErrorMessage = "Order Unit Type is Limited to 6 characters")]
        public string OrderUnitType { get; set; }

        public int OrderUnitSize { get; set; }

        [Column(TypeName = "money")]
        public decimal OrderUnitCost { get; set; }

        public virtual Product Product { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}
