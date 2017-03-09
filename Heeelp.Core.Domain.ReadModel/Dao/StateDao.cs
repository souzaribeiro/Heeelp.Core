using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Dao
{
    public class StateDao : IStateDao
    {
        private readonly Func<HeeelpReadDataContext> _contextFactory;
        public StateDao(Func<HeeelpReadDataContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }


        public State Get(State t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<State> List()
        {
            var _context = _contextFactory();
            return _context.State.ToList();
        }

        public StateDTO Get(int id)
        {
            var _context = _contextFactory();
            return _context.State.Where(x => x.StateId == id).Select(x =>
                new StateDTO()
                {
                    StateId = x.StateId,
                    Name = x.Name,
                    Code = x.Code,
                    CountryRegionId = x.CountryRegionId,
                    Coordinates = x.Coordinates,
                    Active = x.Active
                }).First();
        }
    }
}
