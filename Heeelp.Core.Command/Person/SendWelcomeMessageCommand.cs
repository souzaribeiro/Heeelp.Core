using Heeelp.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Command.Person
{
    public class SendWelcomeMessageCommand : CommandBase
    {
        public SendWelcomeMessageCommand()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid SourceId { get; set; }
        public Guid UserIntegrationCode { get; set; }        

    }
}
