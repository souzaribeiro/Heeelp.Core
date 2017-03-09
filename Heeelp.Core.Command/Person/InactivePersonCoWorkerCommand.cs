using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Command.Person
{
    public class InactivePersonCoWorkerCommand : CommandBase
    {

        public InactivePersonCoWorkerCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid IntegrationCode { get; set; }

        public List<Guid> CompanyListId { get; set; }

        public int DeletedBy { get; set; }

    }
}
