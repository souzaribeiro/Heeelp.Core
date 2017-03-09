using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    public class UserSyncDTO
    {

        public Guid SourceId { get; set; }
        public Guid IntegrationCode { get; set; }
        public int UserId { get; set; }
        public int PersonId { get; set; }
        public string Name { get; set; }
        public bool IsDefaultUser { get; set; }
        public byte? LanguageId { get; set; }
        public int UserStatusId { get; set; }
        public byte UserProfileId { get; set; }
        public bool Active { get; set; }
        public string Email { get; set; }

    }
}
