using Heeelp.Core.Infrastructure.Messaging;
using System;

namespace Heeelp.Core.Event
{
    public class PersonCreatedNotificationEvent : IEvent
    {
        public PersonCreatedNotificationEvent()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid SourceId { get; set; }

        public int PersonId { get; set; }
        public Guid IntegrationCode { get; set; }
        public byte? CountryId { get; set; }
        public byte LanguageId { get; set; }
        public byte PersonStatusId { get; set; }
        public byte CurrencyId { get; set; }
        public bool Active { get; set; }
        public int PersonTypeId { get; set; }
    }
}
