using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Dao
{
    public class CityDao : ICityDao
    {
        private readonly Func<HeeelpReadDataContext> _contextFactory;
        public CityDao(Func<HeeelpReadDataContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }


        public City Get(City t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<City> List()
        {
            var _context = _contextFactory();
            return _context.City.ToList();
        }

        public CityDTO Get(int id)
        {
            var _context = _contextFactory();
            return _context.City.Where(x => x.CityId == id).Select(x =>
                new CityDTO()
                {
                    CityId = x.CityId,
                    Name = x.Name,
                    StateRegionId = x.StateRegionId,
                    Coordinates = x.Coordinates,
                    PostCode = x.PostCode,
                    Active = x.Active,
                    InsertedDate = x.InsertedDate,
                    PhoneCode = x.PhoneCode
                }).First();
        }
    }
}
