using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Command.Person
{
    public class AddPersonRulesCommand : CommandBase
    {

        public AddPersonRulesCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public int PersonRulesId { get; set; }
        public int PersonId { get; set; }

        public Guid IntegrationCode { get; set; }

        public bool Active { get; set; }
        public byte PersonProfileId { get; set; }
        public byte RulesStatusId { get; set; }
        public DateTime DateUTC { get; set; }

    }
}
