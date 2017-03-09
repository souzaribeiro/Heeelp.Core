namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Neighbourhood")]
    public partial class Neighbourhood
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Neighbourhood()
        {
            Neighbourhood1 = new HashSet<Neighbourhood>();
            PersonAddress = new HashSet<PersonAddress>();
        }

        public int NeighbourhoodId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int CityId { get; set; }

        public int? NeighbourhoodFatherId { get; set; }

        public DbGeography Coordinates { get; set; }

        public bool Active { get; set; }

        public DateTime InsertedDate { get; set; }

        public int? CityZoneId { get; set; }

        [StringLength(15)]
        public string PostCode { get; set; }

        public virtual CityZone CityZone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Neighbourhood> Neighbourhood1 { get; set; }

        public virtual Neighbourhood Neighbourhood2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonAddress> PersonAddress { get; set; }
    }
}
