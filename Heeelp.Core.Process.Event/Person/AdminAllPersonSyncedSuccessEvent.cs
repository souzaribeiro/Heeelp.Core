
using Heeelp.Core.Infrastructure.Messaging;
using System;

namespace Heeelp.Core.Event
{
    public class AdminAllPersonSyncedSuccessEvent : IEvent
    {
        public AdminAllPersonSyncedSuccessEvent()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid SourceId { get; set; }

    }
}
