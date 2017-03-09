using Heeelp.Core.Infrastructure.Messaging;
using System;

namespace Heeelp.Core.Process.Event.User
{
    public class NewUserWelcomeMessageEvent : IEvent
    {
        public NewUserWelcomeMessageEvent()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid SourceId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int MessageCodeType { get; set; }
        public string ClubBenefits { get; set; }
        public string LogoTipo { get; set; }

    }
}
