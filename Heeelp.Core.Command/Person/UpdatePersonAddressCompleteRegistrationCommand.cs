using System;
using System.Collections.Generic;

namespace Heeelp.Core.Command.Person
{
    public class UpdatePersonAddressCompleteRegistrationCommand : CommandBase
    {

        public UpdatePersonAddressCompleteRegistrationCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public int PersonAddressId { get; set; }

        public int PersonId { get; set; }

        public string StreetName { get; set; }

        public string Number { get; set; }

        public string Neighbourhood { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string PostCode { get; set; }

        public short ServerInstanceId { get; set; }

        public Guid PersonIntegrationId { get; set; }

        public string ContactPhoneNumber { get; set; }

        public int CreatedBy { get; set; }

        public string ContactEMail { get; set; }
    }
}
