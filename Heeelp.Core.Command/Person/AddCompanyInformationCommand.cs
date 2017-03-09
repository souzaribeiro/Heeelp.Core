using System;
using System.Collections.Generic;

namespace Heeelp.Core.Command.Person
{
    public class AddCompanyInformationCommand : CommandBase
    {

        public AddCompanyInformationCommand()
        {
            this.Id = Guid.NewGuid();
        }
        public string StreetName { get; set; }

        public string Number { get; set; }

        public string Neighbourhood { get; set; }

        public string PostCode { get; set; }

        public int CreatedBy { get; set; }

        public string ContactEmail { get; set; }

        public Guid IntegrationCode { get; set; }

        public string DocumentNumber { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string Complement { get;set;}
    }
}
