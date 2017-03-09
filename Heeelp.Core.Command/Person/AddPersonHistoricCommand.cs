using System;

namespace Heeelp.Core.Command.Person
{
    public class AddPersonHistoricCommand : CommandBase
    {
        public AddPersonHistoricCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public long PersonHistoricId { get; set; }

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
    }
}
