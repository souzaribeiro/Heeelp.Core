using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.DTO.ExternalModules.Integration
{
    public class CheckPermissionInputDTO
    {
        public int PersonId { get; set; }
        public int UserId { get; set; }
    }
}
