namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("State")]
    public partial class State
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public State()
        {
            StateRegion = new HashSet<StateRegion>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StateId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(5)]
        public string Code { get; set; }

        public int? CountryRegionId { get; set; }

        public DbGeography Coordinates { get; set; }

        public bool? Active { get; set; }

        public virtual CountryRegion CountryRegion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StateRegion> StateRegion { get; set; }
    }
}
