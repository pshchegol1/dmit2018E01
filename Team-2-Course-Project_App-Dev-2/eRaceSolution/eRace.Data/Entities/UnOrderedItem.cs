namespace eRace.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UnOrderedItem
    {
        [Key]
        public int ItemID { get; set; }

        public int OrderID { get; set; }

        [Required(ErrorMessage = "Item Name is required")]
        [StringLength(50, ErrorMessage = "Item Name is Limited to 50 characters")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Vendor Product ID is required")]
        [StringLength(25, ErrorMessage = "Vendor Product ID is Limited to 25 characters")]
        public string VendorProductID { get; set; }

        public int Quantity { get; set; }
    }
}
