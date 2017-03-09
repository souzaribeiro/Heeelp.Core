namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EventQuee")]
    public partial class EventQuee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EventQuee()
        {
            EventWorkflow = new HashSet<EventWorkflow>();
        }

        [Key]
        public Guid EventId { get; set; }

        public Guid CommandId { get; set; }

        [Required]
        public string EventPayload { get; set; }

        public byte EventStatusId { get; set; }

        public DateTime CreatedDateUTC { get; set; }

        public short EventTypeId { get; set; }

        public byte ProcessAttempts { get; set; }

        public virtual CommandQuee CommandQuee { get; set; }

        public virtual EventStatus EventStatus { get; set; }

        public virtual EventType EventType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventWorkflow> EventWorkflow { get; set; }
    }
}
