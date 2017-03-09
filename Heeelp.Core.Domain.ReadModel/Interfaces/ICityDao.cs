using Heeelp.Core.Domain.ReadModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Interfaces
{
    public interface ICityDao :IReadBase<City>
    {

        IEnumerable<City> List();
        CityDTO  Get(int id);
    }
}
