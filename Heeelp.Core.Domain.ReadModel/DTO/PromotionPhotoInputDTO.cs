using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using FluentValidation;
using FluentValidation.Attributes;


namespace Heeelp.Core.Domain.ReadModel.DTO
{                                               
    public class PromotionPhotoInputDTO
    {
        public Guid PromotionIntegrationCode { get; set; }
        public int FileId { get; set; }
        public string PathImage { get; set; }
        public int UserSystemId { get; set; }
        public bool Active { get; set; }
        public bool IsDefault { get; set; }
        public byte ShowOrder { get; set; }
    }
   
}
