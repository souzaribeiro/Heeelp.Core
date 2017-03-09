using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Dao
{
    public class UserDao : IUserDao
    {
        private readonly Func<HeeelpReadDataContext> _contextFactory;
        public UserDao(Func<HeeelpReadDataContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }


        public User Get(User t)
        {
            var _context = _contextFactory();
            return _context.User.Where(x => x.UserId == t.UserId).FirstOrDefault();
        }

        public IEnumerable<User> List()
        {
            var _context = _contextFactory();
            return _context.User.ToList();
        }
        public IEnumerable<UserListDTO> ListUsers()
        {
            var _context = _contextFactory();
            return _context.User.Select(x => new UserListDTO() { UserId = x.UserId, Name = x.Name });
        }

        public List<Domain.User> GetUserPersonNatural(List<int> userIdList)
        {
            var _context = _contextFactory();
            var ret = (from u in _context.User
                       join p in _context.Person on u.PersonId equals p.PersonId
                       where p.PersonTypeId == (int)GeneralEnumerators.EnumPersonType.Legal_Person
                       && userIdList.Contains(u.UserId)
                       select new Domain.User()
                       {
                           UserId = u.UserId,
                           IntegrationCode = u.IntegrationCode,
                           Name = u.Name,
                           Email = u.Email,
                           SecundaryEmail = u.SecundaryEmail,
                           SmartPhoneNumber = u.SmartPhoneNumber,
                           PersonId = u.PersonId,
                           IsDefaultUser = u.IsDefaultUser,
                           UserProfileId = u.UserProfileId,
                           UserStatusId = u.UserStatusId,
                           AuthenticationModeId = u.AuthenticationModeId,
                           LanguageId = u.LanguageId,
                           CreationDateUTC = u.CreationDateUTC,
                           ValidationDateUTC = u.ValidationDateUTC,
                           FormFillTime = u.FormFillTime,
                           ValidationToken = u.ValidationToken,
                           SecurityCheckNecessary = u.SecurityCheckNecessary,
                           ValidationAttempts = u.ValidationAttempts,
                           LoginFailAttempts = u.LoginFailAttempts,
                           LastLoginFailUTC = u.LastLoginFailUTC,
                           Active = u.Active,
                           LoginPassword = u.LoginPassword,
                           EnrollmentIP = u.EnrollmentIP,
                           ValidationIP = u.ValidationIP,
                           CreatedBy = u.CreatedBy,
                           ServerInstanceId = u.ServerInstanceId,
                           IsPerpetual = u.IsPerpetual
                       }).ToList();
            return ret;
        }

        public List<Domain.User> GetUsersByPersonIntegrationCode(Guid integrationCode)
        {
            var _context = _contextFactory();

            var email = from p in _context.Person
                           join u in _context.User on p.PersonId equals u.PersonId
                           where u.IntegrationCode == integrationCode
                           select u.Email;


            var ret = (from u in _context.User
                       where u.Email == email.FirstOrDefault()
                       select new Domain.User()
                       {
                           UserId = u.UserId,
                           IntegrationCode = u.IntegrationCode,
                           Name = u.Name,
                           Email = u.Email,
                           SecundaryEmail = u.SecundaryEmail,
                           SmartPhoneNumber = u.SmartPhoneNumber,
                           PersonId = u.PersonId,
                           IsDefaultUser = u.IsDefaultUser,
                           UserProfileId = u.UserProfileId,
                           UserStatusId = u.UserStatusId,
                           AuthenticationModeId = u.AuthenticationModeId,
                           LanguageId = u.LanguageId,
                           CreationDateUTC = u.CreationDateUTC,
                           ValidationDateUTC = u.ValidationDateUTC,
                           FormFillTime = u.FormFillTime,
                           ValidationToken = u.ValidationToken,
                           SecurityCheckNecessary = u.SecurityCheckNecessary,
                           ValidationAttempts = u.ValidationAttempts,
                           LoginFailAttempts = u.LoginFailAttempts,
                           LastLoginFailUTC = u.LastLoginFailUTC,
                           Active = u.Active,
                           LoginPassword = u.LoginPassword,
                           EnrollmentIP = u.EnrollmentIP,
                           ValidationIP = u.ValidationIP,
                           CreatedBy = u.CreatedBy,
                           ServerInstanceId = u.ServerInstanceId,
                           IsPerpetual = u.IsPerpetual
                       }).ToList();

            return ret;
        }

        public User GetUserFullInfoUserPJ(int userPJId)
        {
            var _context = _contextFactory();
            var ret = (from u in _context.User
                       join p in _context.Person on u.PersonId equals p.PersonId
                       where p.PersonTypeId == (int)GeneralEnumerators.EnumPersonType.Legal_Person && u.UserId == userPJId
                       select new { u, p }).First();
            User user = ret.u;
            user.Person = ret.p;

            return user;
        }

        public User GetUserFullInfoUserPJ(string emailUserPJ)
        {
            var _context = _contextFactory();
            var ret = (from u in _context.User
                       join p in _context.Person on u.PersonId equals p.PersonId
                       where p.PersonTypeId == (int)GeneralEnumerators.EnumPersonType.Legal_Person && u.Email == emailUserPJ
                       select new { u, p }).First();
            User user = ret.u;
            user.Person = ret.p;

            return user;
        }



        public User GetUserFullInfoUserPF(int userPFId)
        {
            var _context = _contextFactory();
            var ret = (from u in _context.User
                       join p in _context.Person on u.PersonId equals p.PersonId
                       join pfather in _context.Person on p.PersonFatherId equals pfather.PersonId
                       where p.PersonTypeId == (int)GeneralEnumerators.EnumPersonType.Natural_Person && u.UserId == userPFId
                       select new { u, p, pfather }).First();
            User user = ret.u;
            user.Person = ret.p;
            //user.Person.PersonFather = ret.pfather;

            return user;
        }

        public User GetUserFullInfoUserPF(string emailUserPF)
        {
            var _context = _contextFactory();
            var ret = (from u in _context.User
                       join p in _context.Person on u.PersonId equals p.PersonId
                       join pfather in _context.Person on p.PersonFatherId equals pfather.PersonId
                       where p.PersonTypeId == (int)GeneralEnumerators.EnumPersonType.Natural_Person && u.Email == emailUserPF
                       select new { u, p, pfather }).First();
            User user = ret.u;
            user.Person = ret.p;
            //user.Person.PersonFather = ret.pfather;

            return user;
        }
        public Guid GetUserIntegrationCodeUserPF(string emailUserPF)
        {
            var _context = _contextFactory();
            var ret = (from u in _context.User
                       join p in _context.Person on u.PersonId equals p.PersonId
                       join pfather in _context.Person on p.PersonFatherId equals pfather.PersonId
                       where p.PersonTypeId == (int)GeneralEnumerators.EnumPersonType.Natural_Person && u.Email == emailUserPF
                       select new { u.IntegrationCode }).First();
                                                      

            return ret.IntegrationCode;
        }

        public EmployeeDTO GetEmployeeActive(Guid employeeId)
        {
            var context = _contextFactory();
            var ret = context.User
                .Where(x => x.IntegrationCode == employeeId
                        && x.UserStatusId == (int)GeneralEnumerators.EnumUserStatus.AguardandoAtivacao)
                .Select(y => new EmployeeDTO
                {
                    EmployeeId = y.IntegrationCode,
                    Name = y.Name,
                    Email = y.Email
                }).FirstOrDefault();

            return ret;
        }


        public User GetUserByIntegrationCode(Guid userIntegrationCode)
        {
            var context = _contextFactory();
            var ret = context.User
                .Where(x => x.IntegrationCode == userIntegrationCode).FirstOrDefault();                                    
                

            return ret;
        }


        public bool EmployeeNewPassWord(EmployeeActivationDTO employee)
        {

            var context = _contextFactory();
            try
            {
                var user = context.User
                    .Where(x => x.IntegrationCode.Equals(employee.EmployeeId))
                    .FirstOrDefault();

                user.LoginPassword = employee.PassWord;
                user.UserStatusId = (int)GeneralEnumerators.EnumUserStatus.Ativo;

                context.User.Attach(user);
                var entry = context.Entry(user);
                entry.Property(e => e.LoginPassword).IsModified = true;
                entry.Property(e => e.UserStatusId).IsModified = true;

                context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public User Get(Guid IntegrationCode)
        {
            var _context = _contextFactory();
            return _context.User.Where(x => x.IntegrationCode == IntegrationCode).FirstOrDefault();
        }

        public bool CheckCurrentPassword(Guid UserIntegrationCode, string password)
        {
            var _context = _contextFactory();
            var userPsw = _context.User.Where(x => x.IntegrationCode == UserIntegrationCode).Select(x=>x.LoginPassword).FirstOrDefault();
            if (userPsw == null)
                return false;
            return userPsw == password;
        }

        public bool ValidateTokenForgotPassword(Guid integrationCode)
        {
            var _context = _contextFactory();

            var personToActivate = from u in _context.User
                                   where u.IntegrationCode.Equals(integrationCode)
                                   select new { u.PersonId };

            return personToActivate.Count() > 0;
        }

        public PersonBenefitClubDTO GetUserPersonBenefitClub(Guid IntegrationCode)
        {
            var _context = _contextFactory();
            var ret = (from u in _context.User
                       join p in _context.Person on u.PersonId equals p.PersonId
                       join pbc in _context.PersonBenefitClub on p.PersonBenefitClubId equals pbc.PersonBenefitClubId
                       where u.IntegrationCode == IntegrationCode
                            select new { pbc }).FirstOrDefault();

            if (ret != null)
            {
                return new PersonBenefitClubDTO()
                {
                    CustomClubName = ret.pbc.CustomClubName,
                    CustomClubLogo = ret.pbc.CustomClubLogo,
                    CustomHeeelpPersonDomain = ret.pbc.CustomHeeelpPersonDomain,
                    Description = ret.pbc.Description
                };
            }
            return null;

        }

    }
}
