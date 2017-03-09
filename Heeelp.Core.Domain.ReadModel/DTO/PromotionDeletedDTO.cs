using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using FluentValidation;
using FluentValidation.Attributes;


namespace Heeelp.Core.Domain.ReadModel.DTO
{
    [Validator(typeof(PromotionDeletedDTO))]
    public class PromotionDeletedDTO
    {
        public Guid PromotionIntegrationCode { get; set; }
        public Guid UserSessionId { get; set; }
        public int UserSystemId { get; set; }
    }
    public class PromotionDeletedDTOValidator : AbstractValidator<PromotionDeletedDTO>
    {
        public PromotionDeletedDTOValidator()
        {
            RuleFor(x => x.PromotionIntegrationCode)
                 .NotEmpty()
                 .WithState(x => new ErrorState
                 {
                     ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                     DeveloperMessageTemplate = "{0} obrigatório",
                     DocumentationPath = "/Usernames",
                     UserMessage = "A promotion é obrigatória."
                 });
            RuleFor(x => x.UserSessionId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} é obrigatório",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O UserSessionId obrigatório."
                });
        }
    }
}
