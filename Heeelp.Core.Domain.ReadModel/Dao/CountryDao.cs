using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Dao
{
    public class CountryDao : ICountryDao
    {
        private readonly Func<HeeelpReadDataContext> _contextFactory;
        public CountryDao(Func<HeeelpReadDataContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }


        public Country Get(Country t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Country> List()
        {
            var _context = _contextFactory();
            return _context.Country.ToList();
        }
        public IEnumerable<CountryListDTO> ListCountrys()
        {
            var _context = _contextFactory();
            return _context.Country.Select(x => new CountryListDTO() { CountryId = x.CountryId, Name = x.Name });
        }

        public CountryDTO Get(int id)
        {
            var _context = _contextFactory();
            return _context.Country.Where(x => x.CountryId == id).Select(x =>
                new CountryDTO()
                {
                    CountryId = x.CountryId,
                    Name = x.Name,
                    Code = x.Code,
                    PhoneCode = x.PhoneCode,
                    LanguageId = x.LanguageId,
                    CurrencyId = x.CurrencyId
                }).First();
        }
    }
}
