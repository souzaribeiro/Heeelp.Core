namespace Heeelp.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Infrastructure.Database;
    using Infrastructure.Messaging;

    [Table("CountryRegion")]
    public partial class CountryRegion : IAggregateRoot, IEventPublisher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CountryRegion()
        {
            State = new HashSet<State>();
        }
        public CountryRegion(int countryRegionId, string name, byte countryId)
        {
            this.CountryRegionId = countryRegionId;
            this.Name = name;
            this.CountryId = countryId;
        }
        public Guid Id { get; set; }

        private List<IEvent> events = new List<IEvent>();
        public IEnumerable<IEvent> Events { get { return this.events; } }

        protected void AddEvent(IEvent @event)
        {
            this.events.Add(@event);
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CountryRegionId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public byte CountryId { get; set; }

        public virtual Country Country { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<State> State { get; set; }
    }
}
