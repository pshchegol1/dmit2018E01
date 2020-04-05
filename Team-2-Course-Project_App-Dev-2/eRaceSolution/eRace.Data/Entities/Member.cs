namespace eRace.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Member
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Member()
        {
            Cars = new HashSet<Car>();
            RaceDetails = new HashSet<RaceDetail>();
        }

        public int MemberID { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [StringLength(30, ErrorMessage = "Last Name is Limited to 30 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        [StringLength(30, ErrorMessage = "First Name is Limited to 30 characters")]
        public string FirstName { get; set; }

        [StringLength(10, ErrorMessage = "Phone is Limited to 10 characters")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is Required")]
        [StringLength(30, ErrorMessage = "Address is Limited to 30 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is Required")]
        [StringLength(30, ErrorMessage = "City is Limited to 30 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "PostalCode is Required")]
        [StringLength(6, ErrorMessage = "PostalCode is Limited to 6 characters")]
        public string PostalCode { get; set; }

        [StringLength(30, ErrorMessage = "Email Address is Limited to 30 characters")]
        public string EmailAddress { get; set; }

        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Certification Level is Required")]
        [StringLength(1, ErrorMessage = "Certification Level is Limited to 1 character")]
        public string CertificationLevel { get; set; }

        [StringLength(1, ErrorMessage = "Gender is Limited to 1 character")]
        public string Gender { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Car> Cars { get; set; }

        public virtual Certification Certification { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RaceDetail> RaceDetails { get; set; }
    }
}
