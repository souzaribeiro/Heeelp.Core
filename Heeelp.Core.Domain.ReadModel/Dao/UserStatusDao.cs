using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Dao
{
    public class UserStatusDao : IUserStatusDao
    {
        private readonly Func<HeeelpReadDataContext> _contextFactory;
        public UserStatusDao(Func<HeeelpReadDataContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }


        public UserStatus Get(UserStatus t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserStatus> List()
        {
            var _context = _contextFactory();
            return _context.UserStatus.ToList();
        }
        public IEnumerable<UserStatusListDTO> ListUserStatus()
        {
            var _context = _contextFactory();
            return _context.UserStatus.Select(x => new UserStatusListDTO() { UserStatusId = x.UserStatusId, Name = x.Name });
        }

        public IEnumerable<UserListDTO> ListUserByPerson(Guid integrationCode)
        {
            var _context = _contextFactory();


            return from u in _context.User
                   join p in _context.Person on u.PersonId equals p.PersonId
                   where p.IntegrationCode == integrationCode
                   select new UserListDTO() { Name = u.Name, UserId = u.UserId };


        }
    }
}
