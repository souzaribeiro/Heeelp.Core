namespace Heeelp.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Infrastructure.Database;
    using Infrastructure.Messaging;

    [Table("State")]
    public partial class State : IAggregateRoot, IEventPublisher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public State()
        {
            StateRegion = new HashSet<StateRegion>();
        }
        public State(int stateId, string name, string code, int? countryRegionId, string coordinates, bool? active)
        {

            this.StateId = stateId;
            this.Name = name;
            this.Code = code;
            this.CountryRegionId = countryRegionId;
            this.Coordinates = coordinates;
            this.Active = active;

        }
        public Guid Id { get; set; }

        private List<IEvent> events = new List<IEvent>();
        public IEnumerable<IEvent> Events { get { return this.events; } }

        protected void AddEvent(IEvent @event)
        {
            this.events.Add(@event);
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StateId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(5)]
        public string Code { get; set; }

        public int? CountryRegionId { get; set; }

        public string Coordinates { get; set; }

        public bool? Active { get; set; }

        public virtual CountryRegion CountryRegion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StateRegion> StateRegion { get; set; }
    }
}
