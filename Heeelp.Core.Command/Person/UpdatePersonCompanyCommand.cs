using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Command.Person
{
    public class UpdatePersonCompanyCommand : CommandBase
    {

        public UpdatePersonCompanyCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public int PersonId { get; set; }

        public Guid IntegrationCode { get; set; }

        public string Name { get; set; }

        public string FantasyName { get; set; }

        public string FriendlyNameURL { get; set; }

        public byte? PersonTypeId { get; set; }

        public byte? PersonProfileId { get; set; }

        public byte? PersonStatusId { get; set; }

        public string PersonalWebSite { get; set; }

        public string PhoneNumber { get; set; }

        public int UpdatedBy { get; set; }
    }
}
