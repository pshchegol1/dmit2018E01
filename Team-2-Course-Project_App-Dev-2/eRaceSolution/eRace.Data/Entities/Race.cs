namespace eRace.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Race
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Race()
        {
            RaceDetails = new HashSet<RaceDetail>();
        }

        public int RaceID { get; set; }

        public DateTime RaceDate { get; set; }

        public int NumberOfCars { get; set; }

        public int Laps { get; set; }

        [Required(ErrorMessage = "Run is required")]
        [StringLength(1, ErrorMessage = "Run is Limited to 1 character")]
        public string Run { get; set; }

        [StringLength(1048, ErrorMessage = "Comment is Limited to 1048 characters")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Certification Level is required")]
        [StringLength(1, ErrorMessage = "Certification Level is Limited to 1 character")]
        public string CertificationLevel { get; set; }

        public virtual Certification Certification { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RaceDetail> RaceDetails { get; set; }
    }
}
