namespace Heeelp.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Infrastructure.Database;
    using Infrastructure.Messaging;

    [Table("CityZone")]
    public partial class CityZone : IAggregateRoot, IEventPublisher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CityZone()
        {
            Neighbourhood = new HashSet<Neighbourhood>();
        }
        public CityZone(int cityZoneId, string name, string code, int cityId, DateTime insertedDate, bool active)
        {
            this.CityZoneId = cityZoneId;
            this.Name = name;
            this.Code = code;
            this.CityId = cityId;
            this.InsertedDate = insertedDate;
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
        public int CityZoneId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(10)]
        public string Code { get; set; }

        public int CityId { get; set; }

        public DateTime InsertedDate { get; set; }

        public bool Active { get; set; }

        public virtual City City { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Neighbourhood> Neighbourhood { get; set; }
    }
}
