using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Dao
{
    public class PersonTypeDao : IPersonTypeDao
    {
        private readonly Func<HeeelpReadDataContext> _contextFactory;
        public PersonTypeDao(Func<HeeelpReadDataContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }


        public PersonType Get(PersonType t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonType> List()
        {
            var _context = _contextFactory();
            return _context.PersonType.ToList();
        }
        public IEnumerable<PersonTypeListDTO> ListPersonTypes()
        {
            var _context = _contextFactory();
            return _context.PersonType.Select(x => new PersonTypeListDTO() { PersonTypeId = x.PersonTypeId, Name = x.Name } );
        }
    }
}
