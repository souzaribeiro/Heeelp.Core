using Heeelp.Core.Domain.ReadModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Interfaces
{
    public interface IServerInstanceDao :IReadBase<ServerInstance>
    {

        IEnumerable<ServerInstanceListDTO> ListServerInstances();
        
    }
}
