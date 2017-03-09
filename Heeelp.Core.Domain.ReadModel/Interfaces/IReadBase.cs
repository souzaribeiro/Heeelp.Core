using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Interfaces
{
    public interface IReadBase<T>
    { 
        IEnumerable<T> List();

        T Get(T t);

    }
}
