using Heeelp.Core.Infrastructure.Messaging;
using System;

namespace Heeelp.Core.Process.Event.User
{
    public class UserSyncedSuccessEvent : IEvent
    {
        public UserSyncedSuccessEvent()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid SourceId { get; set; }
        public Guid IntegrationCode { get; set; }
        public int UserId { get; set; }
        public int PersonId { get; set; }
    }
}
