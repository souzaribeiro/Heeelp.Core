using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using FluentValidation;
using FluentValidation.Attributes;
using static Heeelp.Core.Common.GeneralEnumerators;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    [Validator(typeof(PromotionAwardInputDTO))]
    public class PromotionAwardInputDTO
    {

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
    public class PromotionAwardInputDTOValidator : AbstractValidator<PromotionAwardInputDTO>
    {
        public PromotionAwardInputDTOValidator()
        {

            RuleFor(x => x.ExpertiseId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} obrigatório",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A expertise é obrigatória."
                });

            RuleFor(x => x.Title)
                .NotEmpty()
                .Length(1, 250)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
                    DeveloperMessageTemplate = "{0} deve ter entre 1 e 250 caracteres",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O título deve ter entre 1 e 250 caracteres."
                });

            RuleFor(x => x.ShortDescription)
                .Length(1, 500)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
                    DeveloperMessageTemplate = "{0} deve ter entre 1 e 500 caracteres",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A descrição curta deve ter entre 1 e 500 caracteres."
                });

            RuleFor(x => x.StartDateUTC)
                .NotEmpty()
                .GreaterThanOrEqualTo(x => DateTime.UtcNow)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} é obrigatório",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A data de início deve ser maior ou igual a data de publicação."
                });
                        
            RuleFor(x => x.ValidUntilUTC)
                .NotEmpty()
                .GreaterThanOrEqualTo(x => DateTime.UtcNow)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} é obrigatório",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A data de início deve ser maior ou igual a data de publicação."
                });

            RuleFor(x => x.RequiredTimeForActivation)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} é obrigatório",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O modelo de cobrança é obrigatório."
                });
            RuleFor(x => x.PriceInHeeelps)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} é obrigatório",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O preço da promoção em Heeelps é obrigatório."
                });

            RuleFor(x => x.NumberOfAvailableCoupons)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} é obrigatório",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O número do cupons disponíveis é obrigatório."
                });

            RuleFor(x => x.UserSessionId)
               .NotEmpty()
               .WithState(x => new ErrorState
               {
                   ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                   DeveloperMessageTemplate = "{0} é obrigatório",
                   DocumentationPath = "/Usernames",
                   UserMessage = "O UserSession obrigatório."
               });
            RuleFor(x => x.UserSystemId)
               .NotEmpty()
               .WithState(x => new ErrorState
               {
                   ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                   DeveloperMessageTemplate = "{0} é obrigatório",
                   DocumentationPath = "/Usernames",
                   UserMessage = "O UserId é obrigatório."
               });


        }

    }
}
