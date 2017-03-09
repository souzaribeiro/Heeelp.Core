using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Dao
{
    public class ServerInstanceDao : IServerInstanceDao
    {
        private readonly Func<HeeelpReadDataContext> _contextFactory;
        public ServerInstanceDao(Func<HeeelpReadDataContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }


        public ServerInstance Get(ServerInstance t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ServerInstance> List()
        {
            var _context = _contextFactory();
            return _context.ServerInstance.ToList();
        }
        public IEnumerable<ServerInstanceListDTO> ListServerInstances()
        {
            var _context = _contextFactory();
            return _context.ServerInstance.Select(x => new ServerInstanceListDTO() { ServerInstanceId = x.ServerInstanceId, Name = x.Name } );
        }
    }
}
