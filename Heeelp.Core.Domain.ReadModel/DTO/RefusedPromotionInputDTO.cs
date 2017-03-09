using FluentValidation;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using System;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    public class RefusedPromotionInputDTO
    {
        public Guid IntegrationCode { get; set; }
        public int UserSystemId { get; set; }
        public Guid UserSessionId { get; set; }
        public string RefusedReason { get; set; }


    }
    public class RefusedPromotionInputDTOValidator : AbstractValidator<RefusedPromotionInputDTO>
    {
        public RefusedPromotionInputDTOValidator()
        {
            RuleFor(x => x.IntegrationCode)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O campo IntegrationCode é obrigatório."
                });
        }
    }
}
