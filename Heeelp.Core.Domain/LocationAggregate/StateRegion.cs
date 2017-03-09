namespace Heeelp.Core.Domain
{
    using Infrastructure.Database;
    using Infrastructure.Messaging;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("StateRegion")]
    public partial class StateRegion : IAggregateRoot, IEventPublisher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StateRegion()
        {
            City = new HashSet<City>();
        }
        public StateRegion(int stateRegionId, string name, int stateId, string coordinates, bool? active, DateTime? insertedDate)
        {
            this.StateRegionId = stateRegionId;
            this.Name = name;
            this.StateId = stateId;
            this.Coordinates = coordinates;
            this.Active = active;
            this.InsertedDate = insertedDate;
        }

        public Guid Id { get; set; }

        private List<IEvent> events = new List<IEvent>();
        public IEnumerable<IEvent> Events { get { return this.events; } }

        protected void AddEvent(IEvent @event)
        {
            this.events.Add(@event);
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StateRegionId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int StateId { get; set; }

        public string Coordinates { get; set; }

        public bool? Active { get; set; }

        public DateTime? InsertedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<City> City { get; set; }

        public virtual State State { get; set; }
    }
}
