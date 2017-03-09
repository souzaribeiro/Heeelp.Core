using System;
using System.Collections.Generic;

namespace Heeelp.Core.Command.Person
{
    public class UpdatePersonCompleteRegistrationCommand : CommandBase
    {

        public UpdatePersonCompleteRegistrationCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public int PersonId { get; set; }

        public Guid IntegrationCode { get; set; }

        public string PhoneNumber { get; set; }

        public Guid PersonIntegrationID { get; set; }
        
        public long? UserSystemId { get; set; }

        public string Document { get; set; }

        public short ContractId { get; set; }

        public short  PrivacyPolicyId { get; set; }

        public string Password { get; set; }
    }
}
