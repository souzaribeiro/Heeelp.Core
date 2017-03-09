
using Heeelp.Core.Infrastructure.Messaging;
using System;

namespace Heeelp.Core.Process.Event.Person
{
    public class PersonActivationUpdatedSucessEvent : IEvent
    {
        public PersonActivationUpdatedSucessEvent()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid SourceId { get; set; }
        public int PersonId { get; set; }

    }
}
