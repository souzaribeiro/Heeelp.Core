using Heeelp.Core.Domain.ReadModel.DTO;
using System;
using System.Collections.Generic;
using static Heeelp.Core.Domain.DomainEnumerators;

namespace Heeelp.Core.Domain.ReadModel.Interfaces
{
    public interface IContractDao :IReadBase<Contract>
    {
        Contract GetLastestVersionContract(enumContractType type);

        IEnumerable<Contract> GetUserContracts(int userId);

        IEnumerable<ContractListDTO> ListUserContracts(Guid UserIntegrationCode);

        IEnumerable<Contract> GetPersonContracts(Guid PersonItegrationCode);
    }
}
