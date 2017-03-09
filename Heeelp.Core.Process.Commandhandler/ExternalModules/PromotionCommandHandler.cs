using Heeelp.Core.Command.ExternalModules;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel;
using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using Heeelp.Core.Logging;
using System;
using System.Net.Http;

namespace Heeelp.Core.Process.CommandHandler.ExternalModules
{
    public class PromotionCommandHandler : ICommandHandler<CancelCouponCommand>,
        ICommandHandler<TransactCouponCommand>,
        ICommandHandler<AddDiscountPromotionCommand>,
        ICommandHandler<AddAwardPromotionCommand>,
        ICommandHandler<AddGiftPromotionCommand>,
        ICommandHandler<DeletedPromotionCommand>
    {
        public PromotionCommandHandler()
        {
        }

        public void Handle(AddDiscountPromotionCommand command)
        {

            PromotionDiscountInputDTO promotion = new PromotionDiscountInputDTO()
            {
                CurrencyId = command.CurrencyId,
                DiscountePercentege = command.DiscountePercentege,
                ExpertiseId = command.ExpertiseId,
                NeighbourhoodId = command.NeighbourhoodId,
                NormalPrice = command.NormalPrice,
                NumberOfAvailableCoupons = command.NumberOfAvailableCoupons,
                PersonId = command.PersonId,
                PersonIntegrationCode = command.PersonIntegrationCode,
                PotencialDemand = command.PotencialDemand,
                PromotionalPrice = command.PromotionalPrice,
                PromotionBillingModelId = command.PromotionBillingModelId,
                PromotionIntegrationCode = command.PromotionIntegrationCode,
                PromotionPaymentTypeId = command.PromotionPaymentTypeId,
                PromotionRecurrenceId = command.PromotionRecurrenceId,
                RequiredTimeForActivation = command.RequiredTimeForActivation,
                ShortDescription = command.ShortDescription,
                StartDateUTC = command.StartDateUTC,
                Title = command.Title,
                UserSessionId = command.UserSessionId,
                UserSystemId = command.UserSystemId,
                ValidUntilUTC = command.ValidUntilUTC,
                PromotionMethodPaymentId = command.PromotionMethodPaymentId
            };

            var _clientPromotion = new HttpClient();
            _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
            var resultTask = _clientPromotion.PostAsJsonAsync("api/Promotion/AddDiscountPromotion", promotion).Result;
            if (!resultTask.IsSuccessStatusCode)
            {
                LogManager.Error("Failure creating a new  Promotion. Status: " + resultTask.StatusCode);
                throw new Exception("Failure creating a new  Promotion. Status: " + resultTask.StatusCode);
            }
        }

        public void Handle(AddAwardPromotionCommand command)
        {

            PromotionAwardInputDTO promotion = new PromotionAwardInputDTO()
            {
                ExpertiseId = command.ExpertiseId,
                NeighbourhoodId = command.NeighbourhoodId,
                NumberOfAvailableCoupons = command.NumberOfAvailableCoupons,
                PersonIntegrationCode = command.PersonIntegrationCode,
                PromotionIntegrationCode = command.PromotionIntegrationCode,
                PriceInHeeelps = command.PriceInHeeelps,
                RequiredTimeForActivation = command.RequiredTimeForActivation,
                ShortDescription = command.ShortDescription,
                StartDateUTC = command.StartDateUTC,
                Title = command.Title,
                UserSessionId = command.UserSessionId,
                UserSystemId = command.UserSystemId,
                ValidUntilUTC = command.ValidUntilUTC

            };

            var _clientPromotion = new HttpClient();
            _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
            var resultTask = _clientPromotion.PostAsJsonAsync("api/Promotion/AddAwardPromotion", promotion).Result;
            if (!resultTask.IsSuccessStatusCode)
            {
                LogManager.Error("Failure creating a new  Promotion. Status: " + resultTask.StatusCode);
                throw new Exception("Failure creating a new  Promotion. Status: " + resultTask.StatusCode);
            }
        }

        public void Handle(AddGiftPromotionCommand command)
        {
            PromotionGiftInputDTO promotion = new PromotionGiftInputDTO()
            {
                ExpertiseId = command.ExpertiseId,
                NeighbourhoodId = command.NeighbourhoodId,
                NumberOfAvailableCoupons = command.NumberOfAvailableCoupons,
                PersonIntegrationCode = command.PersonIntegrationCode,
                PotencialDemand = command.PotencialDemand,
                PromotionBillingModelId = command.PromotionBillingModelId,
                PromotionIntegrationCode = command.PromotionIntegrationCode,
                PromotionPaymentTypeId = command.PromotionPaymentTypeId,
                RequiredTimeForActivation = command.RequiredTimeForActivation,
                ShortDescription = command.ShortDescription,
                StartDateUTC = command.StartDateUTC,
                Title = command.Title,
                UserSession = command.UserSession,
                UserSystemId = command.UserSystemId,
                ValidUntilUTC = command.ValidUntilUTC
            };

            var _clientPromotion = new HttpClient();
            _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
            var resultTask = _clientPromotion.PostAsJsonAsync("api/Promotion/AddGiftPromotion", promotion).Result;
            if (!resultTask.IsSuccessStatusCode)
            {
                LogManager.Error("Failure creating a new Gift Promotion. Status: " + resultTask.StatusCode);
                throw new Exception("Failure creating a new Gift Promotion. Status: " + resultTask.StatusCode);
            }
        }

        public void Handle(TransactCouponCommand command)
        {

            TransactCouponDto coupon = new TransactCouponDto()
            {
                CouponIntegrationCode = command.CouponIntegrationCode,
                UserSessionTrade = command.UserSessionTrade,
                PersonIntegrationCode = command.PersonIntegrationCode,
                UserId = command.UserId,
                QRCode = command.QRCode
            };

            var _clientPromotion = new HttpClient();
            _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
            var resultTask = _clientPromotion.PostAsJsonAsync("api/Coupon/TransactCoupon", coupon).Result;
            if (!resultTask.IsSuccessStatusCode)
            {
                LogManager.Error("Failure activating/transcting a Coupon. Status: " + resultTask.StatusCode);
                throw new Exception("Failure activating/transcting a Coupon. Status: " + resultTask.StatusCode);
            }
        }

        public void Handle(CancelCouponCommand command)
        {

            CancelCouponDto coupon = new CancelCouponDto()
            {
                CouponIntegrationCode = command.CouponIntegrationCode,
                UserSessionTrade = command.UserSessionTrade,
                PersonIntegrationCode = command.PersonIntegrationCode,
                UserId = command.UserId
            };


            var _clientPromotion = new HttpClient();
            _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
            var resultTask = _clientPromotion.PostAsJsonAsync("api/Coupon/CancelCoupon", coupon).Result;
            if (!resultTask.IsSuccessStatusCode)
            {
                LogManager.Error("Failure canceling a Coupon. Status: " + resultTask.StatusCode);
                throw new Exception("Failure canceling a Coupon. Status: " + resultTask.StatusCode);
            }
        }

        public void Handle(DeletedPromotionCommand command)
        {
            PromotionDeletedDTO promotion = new PromotionDeletedDTO()
            {
                PromotionIntegrationCode = command.PromotionIntegrationCode,
                UserSessionId = command.UserSessionId,
                UserSystemId = command.UserSystemId
            };


            var _clientPromotion = new HttpClient();
            _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
            var resultTask = _clientPromotion.PostAsJsonAsync("api/Promotion/DeletedPromotion", promotion).Result;
            if (!resultTask.IsSuccessStatusCode)
            {
                LogManager.Error("Failure deleted Promotion. Status: " + resultTask.StatusCode);
                throw new Exception("Failure deleted Promotion. Status: " + resultTask.StatusCode);
            }
        }

    }
}
