using System;
using System.Collections.Generic;

namespace Heeelp.Core.Command.Person
{
    public class AddPersonAddressCommand : CommandBase
    {

        public AddPersonAddressCommand()
        {
            this.Id = Guid.NewGuid();
        }
        public int PersonAddressId { get; set; }

        public int PersonId { get; set; }

        public byte AddressTypeId { get; set; }

        public DateTime StartDateUTC { get; set; }

        public string StreetName { get; set; }

        public string Number { get; set; }

        public int NeighbourhoodId { get; set; }

        public string Neighbourhood { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string PostCode { get; set; }

        public string Coordinates { get; set; }

        public string ContactPhoneNumber { get; set; }

        public short ServerInstanceId { get; set; }

        public int CreatedBy { get; set; }

        public string ContactEMail { get; set; }

        public bool Active { get; set; }

        public Guid PersonIntegrationId { get; set; }

        public List<int> listFileTemp;
    }
}
