namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EventType")]
    public partial class EventType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EventType()
        {
            EventQuee = new HashSet<EventQuee>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short EventTypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public byte Priority { get; set; }

        public bool Ative { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventQuee> EventQuee { get; set; }
    }
}
