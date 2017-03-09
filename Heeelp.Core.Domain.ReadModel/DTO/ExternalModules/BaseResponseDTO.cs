using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.DTO.ExternalModules
{
    public class BaseResponseDTO
    {
        public string ResultMessage { get; set; }
        public string ResultCode { get; set; }
        public bool IsError { get; set; }
    }
}
