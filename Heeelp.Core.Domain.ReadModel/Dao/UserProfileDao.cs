using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Dao
{
    public class UserProfileDao : IUserProfileDao
    {
        private readonly Func<HeeelpReadDataContext> _contextFactory;
        public UserProfileDao(Func<HeeelpReadDataContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }


        public UserProfile Get(UserProfile t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserProfile> List()
        {
            var _context = _contextFactory();
            return _context.UserProfile.ToList();
        }
        public IEnumerable<UserProfileListDTO> ListUserProfiles()
        {
            var _context = _contextFactory();
            return _context.UserProfile.Select(x => new UserProfileListDTO() { UserProfileId = x.UserProfileId, Name = x.Name } );
        }
    }
}
