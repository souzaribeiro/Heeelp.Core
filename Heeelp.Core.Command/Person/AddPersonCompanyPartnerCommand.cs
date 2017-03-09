using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heeelp.Core.Common;

namespace Heeelp.Core.Command.Person
{
    public class AddPersonCompanyPartnerCommand : CommandBase
    {

        public AddPersonCompanyPartnerCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public int PersonId { get; set; }

        public Guid IntegrationCode { get; set; }

        public Guid ManagerEmployeeId { get; set; }

        public string CompanyName { get; set; }

        public string CompanyFantasyName { get; set; }

        public string CompanyPhoneNumber { get; set; }

        public string ManagerName { get; set; }

        public string ManagerPhoneNumber { get; set; }

        public string ManagerEmail { get; set; }

        public Guid PersonIntegrationID { get; set; }

        public GeneralEnumerators.EnumPersonOriginType PersonOriginType { get; set; }

        public GeneralEnumerators.EnumPersonOriginType PersonProfile { get; set; }

        public List<int> listFileTemp;

        public long? UserSystemId { get; set; }

        public int? PersonFatherId { get; set; }

        public int CreatedBy { get; set; }
        public string EnrollmentIP { get; set; }

        public byte SkinId  { get; set; }
        public string CustomHeeelpPersonDomain { get; set; }
        public string CustomClubName { get; set; }
        public Guid PersonIntegrationFatherId { get; set; }

    }
}
