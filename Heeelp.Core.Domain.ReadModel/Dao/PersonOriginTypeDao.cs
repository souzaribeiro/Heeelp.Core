using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Dao
{
    public class PersonOriginTypeDao : IPersonOriginTypeDao
    {
        private readonly Func<HeeelpReadDataContext> _contextFactory;
        public PersonOriginTypeDao(Func<HeeelpReadDataContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }


        public PersonOriginType Get(PersonOriginType t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonOriginType> List()
        {
            var _context = _contextFactory();
            return _context.PersonOriginType.ToList();
        }
        public IEnumerable<PersonOriginTypeListDTO> ListPersonOriginTypes()
        {
            var _context = _contextFactory();
            return _context.PersonOriginType.Select(x => new PersonOriginTypeListDTO() { PersonOriginTypeId = x.PersonOriginTypeId, Name = x.Name } );
        }
    }
}
