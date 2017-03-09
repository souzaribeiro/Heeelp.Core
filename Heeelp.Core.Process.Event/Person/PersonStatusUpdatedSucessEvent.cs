
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Common;
using System;

namespace Heeelp.Core.Event
{
    public class PersonStatusUpdatedSucessEvent : IEvent
    {
        public PersonStatusUpdatedSucessEvent()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid SourceId { get; set; }

        public int PersonId { get; set; }
        public GeneralEnumerators.EnumPersonStatus PersonStatusId { get; set; }

    }
}
