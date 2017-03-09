using Heeelp.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Command.User
{
    public class CreateActivationRequestCommand : CommandBase
    {
        public CreateActivationRequestCommand()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid SourceId { get; set; }
        public int PersonToId { get; set; }
        public Guid UserIntegrationCode  { get; set; }
        public GeneralEnumerators.EnumEmailAcvtiveType MessageCodeType { get; set; }
        
    }
}
