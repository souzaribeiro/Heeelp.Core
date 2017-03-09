using Heeelp.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Heeelp.Core.Common.GeneralEnumerators;

namespace Heeelp.Core.Command.ExternalModules
{
    public class AddDiscountPromotionCommand : CommandBase
    {
        public AddDiscountPromotionCommand()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid PromotionIntegrationCode { get; set; }

        public Guid PersonIntegrationCode { get; set; }
        public enumPromotionMethodPayment PromotionMethodPaymentId { get; set; }

        public int PersonId { get; set; }

        public int ExpertiseId { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public DateTime StartDateUTC { get; set; }

        public DateTime ValidUntilUTC { get; set; }

        public int RequiredTimeForActivation { get; set; }

        public byte PromotionBillingModelId { get; set; }

        public byte PromotionRecurrenceId { get; set; }

        public decimal DiscountePercentege { get; set; }

        public byte PromotionPaymentTypeId { get; set; }

        public decimal NormalPrice { get; set; }

        public decimal PromotionalPrice { get; set; }

        public short NumberOfAvailableCoupons { get; set; }
        public byte CurrencyId { get; set; }
        public int NeighbourhoodId { get; set; }
        public int PotencialDemand { get; set; }
        public Guid UserSessionId { get; set; }

        public int UserSystemId { get; set; }
    }
}
