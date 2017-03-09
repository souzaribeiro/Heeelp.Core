using Heeelp.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Command.ExternalModules
{
    public class DeletedPromotionCommand : CommandBase
    {
        public DeletedPromotionCommand()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid PromotionIntegrationCode { get; set; }
        public Guid UserSessionId { get; set; }
        public int UserSystemId { get; set; }
    }
}
