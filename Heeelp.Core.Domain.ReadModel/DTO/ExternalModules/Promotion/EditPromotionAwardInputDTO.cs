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
    [Validator(typeof(EditPromotionAwardInputDTO))]
    public class EditPromotionAwardInputDTO
    {
        public Guid PromotionIntegrationCode { get; set; }
        public int PersonId { get; set; }
        public int ExpertiseId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public DateTime StartDateUTC { get; set; }
        public DateTime? ValidUntilUTC { get; set; }
        public enumTimeForActivation RequiredTimeForActivation { get; set; }
        public GeneralEnumerators.enumPromotionPaymentType PromotionPaymentTypeId { get; set; }
        public short NumberOfAvailableCoupons { get; set; }
        public DateTime IssueDateUTC { get; set; }
        public long UserSession { get; set; }
    }
    public class EditPromotionAwardInputDTOValidator : AbstractValidator<EditPromotionAwardInputDTO>
    {
        public EditPromotionAwardInputDTOValidator()
        {
            RuleFor(x => x.PersonId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} obrigatório",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A pessoa é obrigatória."
                });

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
            RuleFor(x => x.IssueDateUTC)
               .NotEmpty()
               .GreaterThanOrEqualTo(x => DateTime.UtcNow)
               .WithState(x => new ErrorState
               {
                   ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                   DeveloperMessageTemplate = "{0} é obrigatório",
                   DocumentationPath = "/Usernames",
                   UserMessage = "A data de início deve ser maior ou igual a data de publicação."
               });
            

            //todo: Desenvlver CustomValidator para tratar datas validas. ex.: http://stackoverflow.com/questions/2560829/how-to-validate-a-string-as-datetime-using-fluentvalidation
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
            RuleFor(x => x.PromotionPaymentTypeId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} é obrigatório",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O tipo de pagamento é obrigatório."
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

            RuleFor(x => x.UserSession)
               .NotEmpty()
               .WithState(x => new ErrorState
               {
                   ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                   DeveloperMessageTemplate = "{0} é obrigatório",
                   DocumentationPath = "/Usernames",
                   UserMessage = "O UserSession obrigatório."
               });
        }

    }
}
