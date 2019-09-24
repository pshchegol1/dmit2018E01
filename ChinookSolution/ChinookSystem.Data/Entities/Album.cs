namespace ChinookSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Album
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Album()
        {
            Tracks = new HashSet<Track>();
        }

        public int AlbumId { get; set; }

        [Required(ErrorMessage ="Album Title is Required")]
        [StringLength(160, ErrorMessage = "Album Title is Limited to 160 characters")]
        public string Title { get; set; }

        public int ArtistId { get; set; }
        //The Range validation annotation can check the field for a range of values
        //The minimum aqnd maximum values MUST be constants
        public int ReleaseYear { get; set; }

        [StringLength(50, ErrorMessage = "Album Label is Limited to 50 characters")]
        public string ReleaseLabel { get; set; }

        public virtual Artist Artist { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
