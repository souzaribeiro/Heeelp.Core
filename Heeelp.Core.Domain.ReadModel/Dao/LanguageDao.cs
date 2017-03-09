using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Dao
{
    public class LanguageDao : ILanguageDao
    {
        private readonly Func<HeeelpReadDataContext> _contextFactory;
        public LanguageDao(Func<HeeelpReadDataContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }


        public Language Get(Language t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Language> List()
        {
            var _context = _contextFactory();
            return _context.Language.ToList();
        }
        public IEnumerable<LanguageListDTO> ListLanguages()
        {
            var _context = _contextFactory();
            return _context.Language.Select(x => new LanguageListDTO() { LanguageId = x.LanguageId, Name = x.Name } );
        }
    }
}
