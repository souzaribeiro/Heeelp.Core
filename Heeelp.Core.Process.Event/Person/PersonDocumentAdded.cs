using Heeelp.Core.Infrastructure.Messaging;
using System;

namespace Heeelp.Core.Process.Event.Person
{
    public class PersonDocumentAdded : IEvent
    {
        public PersonDocumentAdded()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid SourceId { get; set; }

        public short DocumentTypeId { get; set; }
        public string Number { get; set; }
        public bool Active { get; set; }
        public int UserSystemId { get; set; }
        public int PersonId { get; set; }

        public Guid IntegrationCode { get; set; }
    }
}
