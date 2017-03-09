using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heeelp.Core.Common;

namespace Heeelp.Core.Command.Person
{
    public class AddPersonEmployeeCommand : CommandBase
    {

        public AddPersonEmployeeCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public int PersonId { get; set; }

        public Guid IntegrationCode { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string SecundaryEmail { get; set; }

        public string PhoneNumber { get; set; }

        public GeneralEnumerators.EnumPersonOriginType PersonOriginType { get; set; }

        public GeneralEnumerators.EnumUserProfile UserProfileId { get; set; }

        public Guid PersonIntegrationFatherId { get; set; }
        
        public long? UserSystemId { get; set; }
      
        public int CreatedBy { get; set; }

        public string EnrollmentIP { get; set; }
        public byte SkinId  { get; set; }

    }
}
