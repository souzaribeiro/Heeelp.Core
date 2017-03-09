using Heeelp.Core.Domain.ReadModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Interfaces
{
    public interface IExpertiseDao :IReadBase<Expertise>
    {

        IEnumerable<ExpertiseListDTO> ListExpertises();

        ExpertiseDTO Get(int id);

        ExpertisePhotoDTO GetExpetisePhoto(int id);


        IEnumerable<ExpertiseListDTO> ListMainExpertises();

        IEnumerable<ExpertiseListDTO> ListSubExpertises(int expertiseFatherId);

        IEnumerable<ExpertiseListDTO> List(int? experitseFatherId);

    }
}
