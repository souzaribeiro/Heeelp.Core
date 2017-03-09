using Heeelp.Core.Domain.ReadModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Interfaces
{
    public interface IPersonAddressDao : IReadBase<PersonAddress>
    {
        //PersonAddressClassifiedDTO Get(int id);                        
        PersonAddressClassifiedDTO GetPersonAddressClassified(int id);

        int GetPersonNeighbourhoodId(Guid PersonIntegrationCode);

    }
}
