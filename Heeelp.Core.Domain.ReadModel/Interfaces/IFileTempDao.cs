using Heeelp.Core.Domain.ReadModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Interfaces
{
    public interface IFileTempDao : IReadBase<FileTemp>
    {

        IEnumerable<FileTempListDTO> ListFileTemps();
        int SaveFileTemp(FileTemp ft);
        FileTemp Get(int id);
        void Delete(int id);
    }
}
