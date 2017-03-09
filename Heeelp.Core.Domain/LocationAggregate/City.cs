namespace Heeelp.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Infrastructure.Database;
    using Infrastructure.Messaging;

    [Table("City")]
    public partial class City : IAggregateRoot, IEventPublisher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public City()
        {
            CityZone = new HashSet<CityZone>();
        }
        public City(int cityId, string name, int stateRegionId, string coordinates, string postCode, bool active, DateTime insertedDate, string phoneCode)
        {
            this.CityId = cityId;
            this.Name = name;
            this.StateRegionId = stateRegionId;
            this.Coordinates = coordinates;
            this.PostCode = postCode;
            this.Active = active;
            this.InsertedDate = insertedDate;
            this.PhoneCode = phoneCode;

        }

        public Guid Id { get; set; }

        private List<IEvent> events = new List<IEvent>();
        public IEnumerable<IEvent> Events { get { return this.events; } }

        protected void AddEvent(IEvent @event)
        {
            this.events.Add(@event);
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CityId { get; set; }

        [Required]
        [StringLength(70)]
        public string Name { get; set; }

        public int StateRegionId { get; set; }

        [Required]
        public string Coordinates { get; set; }

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
