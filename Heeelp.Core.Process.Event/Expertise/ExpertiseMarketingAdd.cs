using Heeelp.Core.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Process.Event.Expertise
{
    public class ExpertiseMarketingAdd : IEvent
    {
        public ExpertiseMarketingAdd()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid SourceId { get; set; }
        public int ExpertiseId { get; set; }
        public int RegisterUserId { get; set; }
        public string ExpertiseName { get; set; }
    }
}
