using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Dao
{
    public class PersonProfileDao : IPersonProfileDao
    {
        private readonly Func<HeeelpReadDataContext> _contextFactory;
        public PersonProfileDao(Func<HeeelpReadDataContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }


        public PersonProfile Get(PersonProfile t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonProfile> List()
        {
            var _context = _contextFactory();
            return _context.PersonProfile.ToList();
        }
        public IEnumerable<PersonProfileListDTO> ListPersonProfiles()
        {
            var _context = _contextFactory();
            return _context.PersonProfile.Select(x => new PersonProfileListDTO() { PersonProfileId = x.PersonProfileId, Name = x.Name } );
        }
    }
}
