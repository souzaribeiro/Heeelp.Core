using Heeelp.Core.Infrastructure.Messaging;
using System;

namespace Heeelp.Core.Event
{
    public class PersonCreatedPersonRulesEvent : IEvent
    {
        public PersonCreatedPersonRulesEvent()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid SourceId { get; set; }

        public int PersonId { get; set; }
        public Guid IntegrationCode { get; set; }
        public bool Active { get; set; }
        public byte PersonProfileId { get; set; }
        public byte RulesStatusId { get; set; }
    }
}
