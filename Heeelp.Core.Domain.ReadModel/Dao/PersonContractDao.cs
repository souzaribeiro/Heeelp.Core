using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heeelp.Core.Common;

namespace Heeelp.Core.Domain.ReadModel.Dao
{
    public class PersonContractDao : IPersonContractDao
    {
        private readonly Func<HeeelpReadDataContext> _contextFactory;
        public PersonContractDao(Func<HeeelpReadDataContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }


        public PersonContract Get(PersonContract t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonContract> List()
        {
            throw new NotImplementedException();
        }
    }
}
