namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StateRegion")]
    public partial class StateRegion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StateRegion()
        {
            City = new HashSet<City>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StateRegionId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int StateId { get; set; }

        public DbGeography Coordinates { get; set; }

        public bool? Active { get; set; }

        public DateTime? InsertedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<City> City { get; set; }

        public virtual State State { get; set; }
    }
}
