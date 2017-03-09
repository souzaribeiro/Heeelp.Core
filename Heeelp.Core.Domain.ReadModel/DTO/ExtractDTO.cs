using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    public class ExtractDTO
    {
        public int PersonId { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime EventDateUtc { get; set; }
    }
}
