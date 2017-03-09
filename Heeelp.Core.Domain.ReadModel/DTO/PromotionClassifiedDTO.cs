using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    public class PromotionClassifiedDTO
    {
        public Guid IntegrationCode { get; set; }
        public long PromotionClassifiedId { get; set; }

        public int ClassifiedIssueId { get; set; }

        public int PromotionId { get; set; }

        public long PersonContentId { get; set; }

        public int? PublicEventId { get; set; }

        public int PersonId { get; set; }

        public string PersonName { get; set; }

        public string PersonLogo { get; set; }

        public int PersonEvaluationLevel { get; set; }

        public int PersonRankingNumber { get; set; }

        public int ExpertiseId { get; set; }

        public string ExpertiseName { get; set; }

        public decimal HeeelpCashBackValue { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public string Alert { get; set; }

        public DateTime IssueDateUTC { get; set; }

        public DateTime StartDateUTC { get; set; }

        public DateTime? ValidUntilUTC { get; set; }

        public string DateOfExpire { get; set; }

        public short? RequiredTimeForActivation { get; set; }

        public byte PromotionTypeId { get; set; }

        public string PromotionTypeName { get; set; }

        public decimal DiscountePercentege { get; set; }

        public decimal NormalPrice { get; set; }

        public decimal PromotionalPrice { get; set; }

        public byte CurrencyId { get; set; }

        public string CurrencyName { get; set; }

        public string CurrencySymbol { get; set; }

        public decimal VirtualCreditPrice { get; set; }

        public byte VirtualCurrencyId { get; set; }

        public string VirtualCurrencyName { get; set; }

        public string VirtualCurrencySymbol { get; set; }

        public short NumberOfAvailableCoupons { get; set; }

        public short NumberTotalCoupons { get; set; }

        public string PromotionPhoto { get; set; }

        public byte CountryId { get; set; }

        public string CountryName { get; set; }

        public int StateId { get; set; }

        public string StateName { get; set; }

        public int CityId { get; set; }

        public string CityName { get; set; }

        public int NeighbourhoodId { get; set; }

        public string NeighbourhoodName { get; set; }

        public string Address { get; set; }

        public string Coordinates { get; set; }

        public int PromotionViews { get; set; }

        public int SocialLikes { get; set; }

        public int SocialDontLikes { get; set; }

        public int SocialShares { get; set; }

        public List<long> ListFiles { get; set; }

        public string PhotoURL { get; set; }

        public string PhoneNumber { get; set; }

        public string PersonDocumentNumber { get; set; }

        public Guid PersonIntegrationCode { get; set; }
    }
}
