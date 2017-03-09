using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    public class CancelCouponDto
    {
        public Guid CouponIntegrationCode { get; set; }
        public Guid PersonIntegrationCode { get; set; }
        public Guid UserSessionTrade { get; set; }
        public int UserId { get; set; }

    }
}
