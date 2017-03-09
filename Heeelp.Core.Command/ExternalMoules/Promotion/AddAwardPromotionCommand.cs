using Heeelp.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Command.ExternalModules
{
    public class AddAwardPromotionCommand : CommandBase
    {
        public AddAwardPromotionCommand()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid PromotionIntegrationCode { get; set; }

        public Guid PersonIntegrationCode { get; set; }

        public int ExpertiseId { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public DateTime StartDateUTC { get; set; }

        public DateTime ValidUntilUTC { get; set; }

        public int RequiredTimeForActivation { get; set; }

        public decimal PriceInHeeelps { get; set; }

        public short NumberOfAvailableCoupons { get; set; }
        public int NeighbourhoodId { get; set; }
        public Guid UserSessionId { get; set; }

        public int UserSystemId { get; set; }
    }
}
