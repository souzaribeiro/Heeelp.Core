using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    public class UserHashDTO
    {
        public Guid IntegrationCode { get; set; }
        public string Hash { get;set;}
    }
}
