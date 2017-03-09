namespace Heeelp.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Infrastructure.Database;
    using Infrastructure.Messaging;
    using Event;
    using Common;
    using Process.Event.Person;
    [Table("PersonRules")]

    public partial class PersonRules : IAggregateRoot, IEventPublisher
    {

        public PersonRules(int personRulesId, int personId, byte personProfileId,
            DateTime dateUTC, bool active, byte rulesStatusId)
        {
            this.PersonRulesId = personRulesId;
            this.PersonId = personId;
            this.PersonProfileId = personProfileId;
            this.DateUTC = dateUTC;
            this.Active = active;
            this.RulesStatusId = rulesStatusId;

        }
        public PersonRules()
        {

        }

        [NotMapped]
        public Guid Id { get; set; }

        private List<IEvent> events = new List<IEvent>();
        public IEnumerable<IEvent> Events { get { return this.events; } }

        protected void AddEvent(IEvent @event)
        {
            this.events.Add(@event);
        }

        public int PersonRulesId { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public byte PersonProfileId { get; set; }
        public DateTime DateUTC { get; set; }
        public bool Active { get; set; }
        public int RulesStatusId { get; set; }
}
}
