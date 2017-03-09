using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Command.Person
{
    public class AddPersonCommand : CommandBase
    {

        public AddPersonCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public int PersonId { get; set; }

        public Guid IntegrationCode { get; set; }

        public string Name { get; set; }

        public string FantasyName { get; set; }

        public string NameFromSecurityCheck { get; set; }

        public int? SecuritySourceId { get; set; }

        public bool? IsSafe { get; set; }

        public string FriendlyNameURL { get; set; }

        public byte PersonOriginTypeId { get; set; }

        public string PersonOriginDetails { get; set; }

        public long? CampaignId { get; set; }

        public byte CountryId { get; set; }

        public byte LanguageId { get; set; }

        public byte PersonTypeId { get; set; }

        public byte PersonProfileId { get; set; }

        public byte PersonStatusId { get; set; }

        public string PersonalWebSite { get; set; }

        public byte CurrencyId { get; set; }

        public DateTime CreationDateUTC { get; set; }

        public string ActivationCode { get; set; }

        public DateTime? ActivationDateUTC { get; set; }

        public string PhoneNumber { get; set; }

        public int? PersonFatherId { get; set; }

        public long? InviteId { get; set; }

        public short ServerInstanceId { get; set; }

        public bool Active { get; set; }

        public Guid PersonIntegrationID { get; set; }

        public List<int> listFileTemp;

        public long? UserSystemId { get; set; }

        public string Number { get; set; }
    }
}
