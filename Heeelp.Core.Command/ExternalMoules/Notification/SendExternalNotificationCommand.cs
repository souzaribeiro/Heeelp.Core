using Heeelp.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Command.ExternalModules
{
    public class SendExternalNotificationCommand : CommandBase
    {
        public SendExternalNotificationCommand()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid SourceId { get; set; }
        public int PersonFromId { get; set; }

        public int PersonToId { get; set; }

        public string MessageCodeType { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

    }
}
