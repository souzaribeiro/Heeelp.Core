using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.DTO.ExternalModules.Promotion
{
    public class CouponIssuedOutputDTO : BaseResponseDTO
    {
        public string CouponCode { get; set; }
        public Guid IntegrationCode { get; set; }
        public int? NumberOfAvailableCoupons { get; set; }
    }
}
