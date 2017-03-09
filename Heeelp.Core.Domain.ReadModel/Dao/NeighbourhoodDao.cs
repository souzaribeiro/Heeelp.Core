using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Dao
{
    public class NeighbourhoodDao : INeighbourhoodDao
    {
        private readonly Func<HeeelpReadDataContext> _contextFactory;
        public NeighbourhoodDao(Func<HeeelpReadDataContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }


        public Neighbourhood Get(Neighbourhood t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Neighbourhood> List()
        {
            var _context = _contextFactory();
            return _context.Neighbourhood.ToList();
        }

        public NeighbourhoodDTO Get(int id)
        {
            var _context = _contextFactory();
            return _context.Neighbourhood.Where(x => x.NeighbourhoodId == id).Select(x =>
                new NeighbourhoodDTO()
                {
                    NeighbourhoodId = x.NeighbourhoodId,
                    Name = x.Name,
                    CityId = x.CityId,
                    NeighbourhoodFatherId = x.NeighbourhoodFatherId,
                    Coordinates = x.Coordinates,
                    Active = x.Active,
                    InsertedDate = x.InsertedDate,
                    CityZoneId = x.CityZoneId,
                    PostCode = x.PostCode
                }).First();
        }
    }
}
