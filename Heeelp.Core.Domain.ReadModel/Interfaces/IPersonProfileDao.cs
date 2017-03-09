using Heeelp.Core.Domain.ReadModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Interfaces
{
    public interface IPersonProfileDao :IReadBase<PersonProfile>
    {

        IEnumerable<PersonProfileListDTO> ListPersonProfiles();
        
    }
}
