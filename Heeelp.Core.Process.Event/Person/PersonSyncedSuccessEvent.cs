﻿
using Heeelp.Core.Infrastructure.Messaging;
using System;

namespace Heeelp.Core.Event
{
    public class PersonSyncedSuccessEvent : IEvent
    {
        public PersonSyncedSuccessEvent()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid SourceId { get; set; }

        public int PersonId { get; set; }
    }
}
