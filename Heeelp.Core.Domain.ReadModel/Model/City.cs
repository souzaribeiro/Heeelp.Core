namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("City")]
    public partial class City
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public City()
        {
            CityZone = new HashSet<CityZone>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CityId { get; set; }

        [Required]
        [StringLength(70)]
        public string Name { get; set; }

        public int StateRegionId { get; set; }

        [Required]
        public DbGeography Coordinates { get; set; }

        [StringLength(15)]
        public string PostCode { get; set; }

        public bool Active { get; set; }

        public DateTime InsertedDate { get; set; }

        [StringLength(2)]
        public string PhoneCode { get; set; }

        public virtual StateRegion StateRegion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CityZone> CityZone { get; set; }
    }
}
