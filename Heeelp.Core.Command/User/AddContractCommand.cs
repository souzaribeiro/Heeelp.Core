using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Command.User
{
    public class AddContractCommand
    {
        public int ContractId { get; set; }

        public Guid IntegrationId { get; set; }
        
    }
}
