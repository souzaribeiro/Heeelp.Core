using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    public class MyCouponsDTO
    {
        public int CouponId { get; set; }
        public int PromotionId { get; set; }
        public DateTime? ValidUntilUTC { get; set; }
        public string CouponCode { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string DiscountePercentege { get; set; }
        public string PromotionalPrice { get; set; }
        public string NormalPrice { get; set; }
        public string CouponStatus { get; set; }
        public byte CouponStatusId { get; set; }
        public string Address { get; set; }
        public string PhotoURL { get; set; }
        public string PersonName { get; set; }
        public string PersonLogo { get; set; }
        public string NeighbourhoodName { get; set; }
        public string PersonDocumentNumber { get; set; }
        public string HeeelpCashBackValue { get; set; }
        public string PhoneNumber { get; set; }
        public string DateOfExpire { get; set; }
        public int? PromotionMethodPaymentId { get; set; }
        public Guid PersonIntegrationCode { get; set; }
    }
}
