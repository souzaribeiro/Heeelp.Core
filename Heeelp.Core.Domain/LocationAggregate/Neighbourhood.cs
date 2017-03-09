namespace Heeelp.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Infrastructure.Database;
    using Infrastructure.Messaging;

    [Table("Neighbourhood")]
    public partial class Neighbourhood : IAggregateRoot, IEventPublisher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Neighbourhood()
        {
            Neighbourhood1 = new HashSet<Neighbourhood>();
            PersonAddress = new HashSet<PersonAddress>();
        }
        public Neighbourhood(int neighbourhoodId, string name, int cityId, int? neighbourhoodFatherId, string coordinates, bool active, DateTime insertedDate, int? cityZoneId, string postCode)
        {

            this.NeighbourhoodId = neighbourhoodId;
            this.Name = name;
            this.CityId = cityId;
            this.NeighbourhoodFatherId = neighbourhoodFatherId;
            this.Coordinates = coordinates;
            this.Active = active;
            this.InsertedDate = insertedDate;
            this.CityZoneId = cityZoneId;
            this.PostCode = postCode;

        }
        public Guid Id { get; set; }

        private List<IEvent> events = new List<IEvent>();
        public IEnumerable<IEvent> Events { get { return this.events; } }

        protected void AddEvent(IEvent @event)
        {
            this.events.Add(@event);
        }

        public int NeighbourhoodId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int CityId { get; set; }

        public int? NeighbourhoodFatherId { get; set; }

        public string Coordinates { get; set; }

        public bool Active { get; set; }

        public DateTime InsertedDate { get; set; }

        public int? CityZoneId { get; set; }

        [StringLength(15)]
        public string PostCode { get; set; }

        [NotMapped]
        public virtual CityZone CityZone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [NotMapped]
        public virtual ICollection<Neighbourhood> Neighbourhood1 { get; set; }

        [NotMapped]
        public virtual Neighbourhood Neighbourhood2 { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [NotMapped]
        public virtual ICollection<PersonAddress> PersonAddress { get; set; }
    }
}
