namespace Heeelp.Core.Domain
{
    using Infrastructure.Database;
    using Infrastructure.Messaging;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("PersonInterest")]
    public partial class PersonInterest : IAggregateRoot, IEventPublisher
    {
        public PersonInterest(int personInterestId, int personId, int expertiseId, int? displayOrder, DateTime insertedDateUTC, int? insertedBy, short serverInstanceId, bool active)
        {
            this.PersonInterestId = personInterestId;
            this.PersonId = personId;
            this.ExpertiseId = expertiseId;
            this.DisplayOrder = displayOrder;
            this.InsertedDateUTC = insertedDateUTC;
            this.InsertedBy = insertedBy;
            this.ServerInstanceId = serverInstanceId;
            this.Active = active;
        }

        public Guid Id { get; set; }

        private List<IEvent> events = new List<IEvent>();
        public IEnumerable<IEvent> Events { get { return this.events; } }

        protected void AddEvent(IEvent @event)
        {
            this.events.Add(@event);
        }


        public int PersonInterestId { get; set; }

        public int PersonId { get; set; }

        public int ExpertiseId { get; set; }

        public int? DisplayOrder { get; set; }

        public DateTime InsertedDateUTC { get; set; }

        public int? InsertedBy { get; set; }

        public short ServerInstanceId { get; set; }

        public bool Active { get; set; }

        public virtual Expertise Expertise { get; set; }

        public virtual Person Person { get; set; }

        public virtual ServerInstance ServerInstance { get; set; }

        public virtual User User { get; set; }
    }
}
