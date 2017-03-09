using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Heeelp.Core.Common.GeneralEnumerators;

namespace Heeelp.Core.Domain.ReadModel.Dao
{
    public class AutenticationUserDao : IAutenticationUserDao
    {
        private readonly Func<HeeelpReadDataContext> _contextFactory;
        public AutenticationUserDao(Func<HeeelpReadDataContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }


        public Authentication CheckAuthentication(string email, string password)
        {
            var _context = _contextFactory();

            var userPerson = (from u in _context.User
                              join p in _context.Person on u.PersonId equals p.PersonId
                              where u.Email.Equals(email) && u.LoginPassword.Equals(password)
                              && u.Active && u.UserStatusId == (byte)GeneralEnumerators.EnumUserStatus.Ativo
                              select new { u.UserId, u.Email, u.PersonId, u.Name, u.UserProfileId,u.UrlImagemLogo, p.PersonRules, p.PersonTypeId, p.IntegrationCode, UserIntegrationCode = u.IntegrationCode, Complete = string.IsNullOrEmpty(u.SmartPhoneNumber) ? 0 : 1 }).ToList();



            if (userPerson.Count == 0)
                return null;

            var auth = userPerson.Select(x => new Authentication
            {
                UserId = x.UserId,
                Email = x.Email,
                Name = x.Name,
                PersonId = x.PersonId,
                PersonTypeId = x.PersonTypeId,
                PersonRules = x.PersonRules,
                UserProfileId = x.UserProfileId,
                PersonIntegrationCode = x.IntegrationCode,
                UserIntegrationCode = x.UserIntegrationCode,
                Complete = x.Complete,
                ImgProfileLogo = x.UrlImagemLogo

            }).ToList();

            auth.ForEach(x => x.ProfileClaims = CustomProfile.ListProfiles(x.UserProfileId, x.PersonRules.Select(y => y.PersonProfileId).ToList()));

            List<int> managerProfiles = Constants.DefaultManagerAccessProfile();
            Authentication user = null;
            foreach (var item in auth)
            {
                foreach (var profileClaim in item.ProfileClaims)
                {
                    if (managerProfiles.Contains(profileClaim))
                        return item;      // in case of manager
                }
            }
            // else return natural person 
            return auth.Where(x => x.PersonTypeId == (int)EnumPersonType.Natural_Person).First();
        }

        public Authentication CheckAuthenticationFirstAccess(Guid integrationCode)
        {
            var _context = _contextFactory();

            var email = (from u in _context.User where u.IntegrationCode.Equals(integrationCode) select new { u.Email }).FirstOrDefault();

            var userPerson = (from u in _context.User
                              join p in _context.Person on u.PersonId equals p.PersonId
                              //where u.IntegrationCode.Equals(integrationCode)
                              where u.Email.Equals(email.Email)
                              && u.Active
                              && (u.UserStatusId == (byte)GeneralEnumerators.EnumUserStatus.Ativo
                              || u.UserStatusId == (byte)GeneralEnumerators.EnumUserStatus.AguardandoAtivacao)
                              select new { u.UserId, u.Email, u.PersonId, u.Name, u.UserProfileId, u.UrlImagemLogo, p.PersonRules, p.PersonTypeId, p.IntegrationCode, UserIntegrationCode = u.IntegrationCode, Complete = string.IsNullOrEmpty(u.SmartPhoneNumber) ? 0 : 1 }).ToList();



            if (userPerson.Count == 0)
                return null;

            var auth = userPerson.Select(x => new Authentication
            {
                UserId = x.UserId,
                Email = x.Email,
                Name = x.Name,
                PersonId = x.PersonId,
                PersonTypeId = x.PersonTypeId,
                PersonRules = x.PersonRules,
                UserProfileId = x.UserProfileId,
                PersonIntegrationCode = x.IntegrationCode,
                UserIntegrationCode = x.UserIntegrationCode,
                Complete = x.Complete,
                ImgProfileLogo = x.UrlImagemLogo

            }).ToList();

            auth.ForEach(x => x.ProfileClaims = CustomProfile.ListProfiles(x.UserProfileId, x.PersonRules.Select(y => y.PersonProfileId).ToList()));

            List<int> managerProfiles = Constants.DefaultManagerAccessProfile();
            Authentication user = null;
            foreach (var item in auth)
            {
                foreach (var profileClaim in item.ProfileClaims)
                {
                    if (managerProfiles.Contains(profileClaim))
                        return item;      // in case of manager
                }
            }
            // else return natural person 
            return auth.Where(x => x.PersonTypeId == (int)EnumPersonType.Natural_Person).First();
        }



        

        public bool CheckAuthenticationActive(string email, string password, string token)
        {
            bool ret = false;
            var _context = _contextFactory();
            var auth = _context.User
                .Where(x => x.Email.Equals(email) && x.LoginPassword.Equals(password) && x.IntegrationCode.Equals(token))
                .FirstOrDefault();
            if (auth != null)
            {
                try
                {
                    _context.User.Attach(auth);
                    var entry = _context.Entry(auth);
                    entry.Property(e => e.Active).IsModified = true;
                    // other changed properties
                    _context.SaveChanges();
                    ret = true;
                }
                catch (Exception)
                {

                }
            }

            return ret;
        }

        public Authentication CheckAuthenticationHash(Guid integrationCode)
        {
            var _context = _contextFactory();

            var userPerson = (from u in _context.User
                              join p in _context.Person on u.PersonId equals p.PersonId
                              where u.IntegrationCode.Equals(integrationCode)
                              && u.Active && (u.UserStatusId == (byte)GeneralEnumerators.EnumUserStatus.Ativo ||
                              u.UserStatusId == (byte)GeneralEnumerators.EnumUserStatus.AguardandoAtivacao)
                              select new { u.UserId, u.Email, u.PersonId, u.Name, u.UserProfileId, u.UrlImagemLogo, p.PersonRules, p.PersonTypeId, p.IntegrationCode, UserIntegrationCode = u.IntegrationCode, Complete = string.IsNullOrEmpty(u.SmartPhoneNumber) ? 0 : 1 }).ToList();



            if (userPerson.Count == 0)
                return null;

            var auth = userPerson.Select(x => new Authentication
            {
                UserId = x.UserId,
                Email = x.Email,
                Name = x.Name,
                PersonId = x.PersonId,
                PersonTypeId = x.PersonTypeId,
                PersonRules = x.PersonRules,
                UserProfileId = x.UserProfileId,
                PersonIntegrationCode = x.IntegrationCode,
                UserIntegrationCode = x.UserIntegrationCode,
                Complete = x.Complete,
                ImgProfileLogo = x.UrlImagemLogo

            }).ToList();

            auth.ForEach(x => x.ProfileClaims = CustomProfile.ListProfiles(x.UserProfileId, x.PersonRules.Select(y => y.PersonProfileId).ToList()));

            List<int> managerProfiles = Constants.DefaultManagerAccessProfile();
            Authentication user = null;
            foreach (var item in auth)
            {
                foreach (var profileClaim in item.ProfileClaims)
                {
                    if (managerProfiles.Contains(profileClaim))
                        return item;      // in case of manager
                }
            }
            // else return natural person 
            return auth.Where(x => x.PersonTypeId == (int)EnumPersonType.Natural_Person).First();
        }

        public bool CheckActivationUser(Guid UserIntegrationCode)
        {

            var _context = _contextFactory();

            var userToActivate = from u in _context.User
                                 where u.IntegrationCode.Equals(UserIntegrationCode)
                                 && u.Active && u.UserStatusId == (int)EnumUserStatus.AguardandoAtivacao
                                 select new { u.UserId };
            return userToActivate.Count() > 0;
        }
    }
}
