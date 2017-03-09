using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using FluentValidation;
using FluentValidation.Attributes;
using Heeelp.Core.Common;

namespace Heeelp.Core.Domain.ReadModel.DTO
{                                                      
    public class PromotionDetailsInputDTO
    {
        public Guid PromotionIntegrationCode { get; set; }
        public string Alert { get; set; }
        public string FullDescription { get; set; }
        public int UserSystemId { get; set; }
        public Guid UserSessionId { get; set; }                  

    }                 
}
