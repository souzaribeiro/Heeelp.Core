using Heeelp.Core.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Process.Event.Expertise
{
    public class ExpertiseNotAdded : IEvent
    {
        public ExpertiseNotAdded()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid SourceId { get; set; }
    }
}
