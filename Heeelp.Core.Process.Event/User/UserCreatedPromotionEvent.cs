using Heeelp.Core.Infrastructure.Messaging;
using System;

namespace Heeelp.Core.Process.Event.User
{
    public class UserCreatedPromotionEvent : IEvent
    {
        public UserCreatedPromotionEvent()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid SourceId { get; set; }
        public Guid IntegrationCode { get; set; }
        public int UserId { get; set; }
        public int PersonId { get; set; }
        public bool IsDefaultUser { get; set; }
        public byte? LanguageId { get; set; }
        public int UserStatusId { get; set; }
        public bool Active { get; set; }

    }
}
