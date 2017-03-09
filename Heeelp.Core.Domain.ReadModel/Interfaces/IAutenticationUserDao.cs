using Heeelp.Core.Domain.ReadModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Interfaces
{
    public interface IAutenticationUserDao
    {

        Authentication CheckAuthenticationFirstAccess(Guid integrationCode);

        Authentication CheckAuthentication(string email, string password);

        Authentication CheckAuthenticationHash(Guid integrationCode);

        bool CheckAuthenticationActive(string email, string password, string token);

        bool CheckActivationUser(Guid UserIntegrationCode);


    }
}
