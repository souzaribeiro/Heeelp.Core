using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Dao
{
    public class CurrencyDao : ICurrencyDao
    {
        private readonly Func<HeeelpReadDataContext> _contextFactory;
        public CurrencyDao(Func<HeeelpReadDataContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }


        public Currency Get(Currency t)
        {
            throw new NotImplementedException();
        }

        public CurrencyDTO Get(byte id)
        {
            var _context = _contextFactory();
            return _context.Currency.Where(x => x.CurrencyId == id).Select(c => new CurrencyDTO() { CurrencyId = c.CurrencyId, Name = c.Name, Active = c.Active, Symbol = c.Symbol }).First();
        }

        public IEnumerable<Currency> List()
        {
            var _context = _contextFactory();
            return _context.Currency.ToList();
        }
        public IEnumerable<CurrencyListDTO> ListCurrencys()
        {
            var _context = _contextFactory();
            return _context.Currency.Select(x => new CurrencyListDTO() { CurrencyId = x.CurrencyId, Name = x.Name });
        }
    }
}
