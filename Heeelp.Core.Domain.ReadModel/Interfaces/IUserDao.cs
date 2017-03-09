using Heeelp.Core.Domain.ReadModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Interfaces
{
    public interface IUserDao :IReadBase<User>
    {
        IEnumerable<UserListDTO> ListUsers();
        List<Domain.User> GetUserPersonNatural(List<int> id);
        User GetUserFullInfoUserPF(int userPFId);
        User GetUserFullInfoUserPF(string emailUserPF);
        User GetUserFullInfoUserPJ(int userPJId);
        User GetUserFullInfoUserPJ(string emailUserPJ);
        Guid GetUserIntegrationCodeUserPF(string emailUserPF);
        EmployeeDTO GetEmployeeActive(Guid employeeId);
        bool EmployeeNewPassWord(EmployeeActivationDTO employee);
        User Get(Guid IntegrationCode);
        List<Domain.User> GetUsersByPersonIntegrationCode(Guid integrationCode);
        bool CheckCurrentPassword(Guid UserIntegrationCode, string password);
        bool ValidateTokenForgotPassword(Guid integrationCode);
        User GetUserByIntegrationCode(Guid userIntegrationCode);
        PersonBenefitClubDTO GetUserPersonBenefitClub(Guid IntegrationCode);
    }
}
