using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Dao
{
    public class  SecuritySourceDao : ISecuritySourceDao
    {
        private readonly Func<HeeelpReadDataContext> _contextFactory;
        public  SecuritySourceDao(Func<HeeelpReadDataContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }


        public SecuritySource Get(SecuritySource t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SecuritySource> List()
        {
            var _context = _contextFactory();
            return _context.SecuritySource.ToList();
        }
        public IEnumerable<SecuritySourceListDTO> ListSecuritySource()
        {
            var _context = _contextFactory();
            return _context.SecuritySource.Select(x => new SecuritySourceListDTO() { SecuritySourceId = x.SecuritySourceId, Name = x.Name } );
        }
    }
}
