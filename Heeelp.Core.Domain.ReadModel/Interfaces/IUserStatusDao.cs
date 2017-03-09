using Heeelp.Core.Domain.ReadModel.DTO;
using System;
using System.Collections.Generic;

namespace Heeelp.Core.Domain.ReadModel.Interfaces
{
    public interface IUserStatusDao : IReadBase<UserStatus>
    {

        IEnumerable<UserStatusListDTO> ListUserStatus();
        IEnumerable<UserListDTO> ListUserByPerson(Guid personIntegrationId);
    }
}
