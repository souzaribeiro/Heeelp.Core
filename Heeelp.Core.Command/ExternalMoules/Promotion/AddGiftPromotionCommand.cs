using Heeelp.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Command.ExternalModules
{
    public class AddGiftPromotionCommand : CommandBase
    {
        public AddGiftPromotionCommand()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid PromotionIntegrationCode { get; set; }

        public Guid PersonIntegrationCode { get; set; }
        public int ExpertiseId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public DateTime StartDateUTC { get; set; }
        public DateTime? ValidUntilUTC { get; set; }
        public GeneralEnumerators.enumTimeForActivation RequiredTimeForActivation { get; set; }
        public GeneralEnumerators.enumPromotionBillingModel PromotionBillingModelId { get; set; }
        public GeneralEnumerators.enumPromotionPaymentType PromotionPaymentTypeId { get; set; }
        public decimal NormalPrice { get; set; }
        public short NumberOfAvailableCoupons { get; set; }
        public int NeighbourhoodId { get; set; }
        public int PotencialDemand { get; set; }
        public Guid UserSession { get; set; }
        public DateTime IssueDateUTC { get; set; }
        public int UserSystemId { get; set; }

    }
}
