using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heeelp.Core.Common;

namespace Heeelp.Core.Command.Person
{
    public class UpdatePersonStatusCommand: CommandBase
    {

        public UpdatePersonStatusCommand()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid SourceId { get; set; }

        public int PersonId { get; set; }
        public GeneralEnumerators.EnumPersonStatus Status { get; set; }

    }
}
