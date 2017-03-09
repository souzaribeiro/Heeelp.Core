using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Dao
{
    public class PersonStatusDao : IPersonStatusDao
    {
        private readonly Func<HeeelpReadDataContext> _contextFactory;
        public PersonStatusDao(Func<HeeelpReadDataContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }


        public PersonStatus Get(PersonStatus t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonStatus> List()
        {
            var _context = _contextFactory();
            return _context.PersonStatus.ToList();
        }
        public IEnumerable<PersonStatusListDTO> ListPersonStatus()
        {
            var _context = _contextFactory();
            return _context.PersonStatus.Select(x => new PersonStatusListDTO() { PersonStatusId = x.PersonStatusId, Name = x.Name } );
        }
    }
}
