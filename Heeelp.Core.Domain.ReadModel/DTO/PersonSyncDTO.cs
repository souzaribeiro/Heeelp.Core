using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    public class PersonSyncDTO
    {
        public Guid Id { get; set; }
        public Guid SourceId { get; set; }

        public int PersonId { get; set; }
        public Guid IntegrationCode { get; set; }        
        public byte? CountryId { get; set; }
        public byte LanguageId { get; set; }
        public byte PersonStatusId { get; set; }
        public byte CurrencyId { get; set; }
        public bool Active { get; set; }
        public int PersonTypeId { get; set; }
        public byte SkinId { get; set; }
        public string CustomHeeelpPersonDomain { get; set; }
        public string CustomClubName { get; set; }

        
    }
}
