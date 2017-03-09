using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    public class ExtractOutputDTO
    {
        public IEnumerable<ExtractDTO> Extract { get; set; }
        public decimal AccountBalance { get; set; }
        public decimal MyValueSaved { get; set; }
    }
}
