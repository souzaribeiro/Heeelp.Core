using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Command.RegisterPromotion
{
    public class AddPromotionProspectCommand : CommandBase
    {
        public AddPromotionProspectCommand()
        {
            this.Id = Guid.NewGuid();
            FilesIdList = new List<long>();
        }
        public int PromotionId { get; set; }

        public int PersonId { get; set; }

        public long PersonContentId { get; set; }

        public int? PublicEventId { get; set; }

        public int ExpertiseId { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public string Alert { get; set; }

        public DateTime IssueDateUTC { get; set; }

        public DateTime StartDateUTC { get; set; }

        public DateTime? ValidUntilUTC { get; set; }

        public short? RequiredTimeForActivation { get; set; }

        public decimal HeeelpTaxValue { get; set; }

        public byte PromotionTypeId { get; set; }

        public byte PromotionBillingModelId { get; set; }

        public byte PromotionRecurrenceId { get; set; }

        public decimal DiscountePercentege { get; set; }

        public byte PromotionPaymentTypeId { get; set; }

        public decimal NormalPrice { get; set; }

        public decimal PromotionalPrice { get; set; }

        public byte CurrencyId { get; set; }

        public decimal VirtualCreditPrice { get; set; }

        public byte VirtualCurrencyId { get; set; }

        public int ExpertisePromotionCostReferenceId { get; set; }

        public short NumberOfAvailableCoupons { get; set; }

        public int UserAccountIdFrom { get; set; }

        public short ServerInstanceId { get; set; }

        public List<int> listFileTemp { get; set; }

        public Guid PersonIntegrationID { get; set; }

        public List<long> FilesIdList { get; set; }

        public int UserSystemId { get; set; }

        public int NeighbourhoodId { get; set; }       
        public int ActualConcorrency { get; set; }
        public int RadiusOfAction { get; set; }

    }
}
