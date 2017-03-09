using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Command.Person
{
    public class AddPersonContractCommand : CommandBase
    {
        public AddPersonContractCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public int PersonContractId { get; set; }

        public int PersonId { get; set; }

        public short ContractId { get; set; }

        public long FileId { get; set; }

        public int UserId { get; set; }

    }
}
