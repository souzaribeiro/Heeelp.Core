namespace Heeelp.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Infrastructure.Database;
    using Infrastructure.Messaging;

    [Table("PersonFile")]
    public partial class PersonFile : IAggregateRoot, IEventPublisher
    {
        public PersonFile()
        {
            this.Id = Guid.NewGuid();
        }

        public PersonFile(int personFileId, int personId, long fileId, DateTime associatedDateUTC, int? associetedBy, bool active)
        {
            this.Id = Guid.NewGuid();
            this.PersonFileId = personFileId;
            this.PersonId = personId;
            this.FileId = fileId;
            this.AssociatedDateUTC = associatedDateUTC;
            this.AssocietedBy = associetedBy;
            this.Active = active;
        }
        [NotMapped]
        public Guid Id { get; set; }

        [NotMapped]
        private List<IEvent> events = new List<IEvent>();
        [NotMapped]
        public IEnumerable<IEvent> Events { get { return this.events; } }

        protected void AddEvent(IEvent @event)
        {
            this.events.Add(@event);
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonFileId { get; set; }

        public int PersonId { get; set; }

        public long FileId { get; set; }

        public DateTime AssociatedDateUTC { get; set; }

        public int? AssocietedBy { get; set; }

        public bool Active { get; set; }
        [NotMapped]
        public virtual Person Person { get; set; }
        [NotMapped]
        public virtual User User { get; set; }
    }
}
