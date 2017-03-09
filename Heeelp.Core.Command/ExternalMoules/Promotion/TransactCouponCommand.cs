using Heeelp.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Command.ExternalModules
{
    public class TransactCouponCommand : CommandBase
    {
        public TransactCouponCommand()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid CouponIntegrationCode { get; set; }
        public Guid PersonIntegrationCode { get; set; }
        public Guid UserSessionTrade { get; set; }
        public string QRCode { get; set; }
        public int UserId { get; set; }

    }
}
