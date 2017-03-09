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
    public class PromotionListDto
    {
        public int PromotionId { get; set; }

        public Guid? IntegrationCode { get; set; }

        public int ExpertiseId { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public DateTime? ValidUntilUTC { get; set; }

        public byte PromotionStatusId { get; set; }

        public string PromotionStatusName { get; set; }

        public string FileUrl { get; set; }

        public string Alert { get; set; }

        public string FullDescription { get; set; }

    }
}
