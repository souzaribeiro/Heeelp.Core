using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Dao
{
    public class PersonExpertiseDao : IPersonExpertiseDao
    {
        private readonly Func<HeeelpReadDataContext> _contextFactory;
        public PersonExpertiseDao(Func<HeeelpReadDataContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }


        public PersonExpertise Get(PersonExpertise t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonExpertise> List()
        {
            var _context = _contextFactory();
            return _context.PersonExpertise.ToList();
        }
        public IEnumerable<PersonExpertiseListDTO> ListPersonExpertiseByPerson(int id)
        {
            var _context = _contextFactory();
            var ret = from pe in _context.PersonExpertise
                      join e in _context.Expertise on pe.ExpertiseId equals e.ExpertiseId
                      where pe.PersonId.Equals(id)
                      select new PersonExpertiseListDTO() { ExpertiseId = e.ExpertiseId, Name = e.Name };

            return ret.ToList();

        }
        public IEnumerable<PersonExpertiseListDTO> ListPersonExpertiseByPerson(Guid IntegrationCode)
        {
            var _context = _contextFactory();
            var ret = from pe in _context.PersonExpertise
                      join p in _context.Person on pe.PersonId equals p.PersonId
                      join e in _context.Expertise on pe.ExpertiseId equals e.ExpertiseId
                      where p.IntegrationCode.Equals(IntegrationCode)
                      select new PersonExpertiseListDTO() { ExpertiseId = e.ExpertiseId, Name = e.Name };

            return ret.ToList();

        }

    }
}
