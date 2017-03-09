using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    public class AddUserSessionDto
    {                                                 
        public Guid IntegrationCode { get; set; }

        public int? UserId { get; set; }

        public DateTime AuthStartDateUTC { get; set; }

        public DateTime? AuthFinishDateUTC { get; set; }

        public string IP { get; set; }

        public string Localization { get; set; }

        public string ClientID { get; set; }

        public byte AuthenticationTypeId { get; set; }

        public short ClientApplicationId { get; set; }

        public string Origin { get; set; }

        public long? InviteId { get; set; }

        public byte LanguageId { get; set; }

        public string AuthToken { get; set; }

        public byte UserSessionStatusId { get; set; }

        public bool Active { get; set; }

        public string Product { get; set; }

        public string Version { get; set; }

        public string OperationalSystem { get; set; }

        public string Resolution { get; set; }

        public string UrlOrigin { get; set; }
    }
}
