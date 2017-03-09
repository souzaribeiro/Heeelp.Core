using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Command.Person
{
    public class AddPersonCompanyExpertiseCommand : CommandBase
    {

        public AddPersonCompanyExpertiseCommand()
        {
            this.Id = Guid.NewGuid();
        }

       
        public Guid IntegrationCode { get; set; }

        public int ExpertiseId { get; set; }

        public int CreatedBy { get; set; }

    }
}
