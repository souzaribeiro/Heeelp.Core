using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    public class UserFullInfoDTO : Heeelp.Core.Domain.User
    {

        public Guid SourceId { get; set; }

    }
}
