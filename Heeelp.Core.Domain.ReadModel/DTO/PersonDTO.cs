using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    public class PersonDTO
    {
        public int PersonId { get; set; }

        public string FriendlyNameURL { get; set; }

        public string PersonalWebSite { get; set; }
        public int? PersonFatherId { get; set; }
        public string CustomClubName { get; set; }
        public string CustomClubLogo { get; set; }
        public string CustomHeeelpPersonDomain { get; set; }
        public ICollection<PersonRules> PersonRules { get; set; }
        public byte SkinId { get; set; }
        public string Name { get; set; }

        public string FantasyName { get; set; }

        public byte PersonOriginTypeId { get; set; }

        public long? CampaignId { get; set; }

        public byte? CountryId { get; set; }

        public byte LanguageId { get; set; }

        public byte PersonTypeId { get; set; }

        //        public byte PersonProfileId { get; set; }

        public byte PersonStatusId { get; set; }

        public byte CurrencyId { get; set; }

        public DateTime CreationDateUTC { get; set; }

        public DateTime? ActivationDateUTC { get; set; }

        public string PhoneNumber { get; set; }

        public bool Active { get; set; }


        public string Description { get; set; }
        public Guid IntegrationCode { get; set; }
        public long? InviteId { get; set; }
        public ICollection<PersonContract> PersonContract { get; set; }
        public PersonType PersonType { get; set; }
        public PersonStatus PersonStatus { get; set; }
        public string UrlOrigin { get; set; }

        public ICollection<PersonFile> PersonFile { get; set; }

        public int? InviteAvailable { get; set; }



    }
}
