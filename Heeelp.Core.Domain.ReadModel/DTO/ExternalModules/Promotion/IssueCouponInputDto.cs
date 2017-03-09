using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Attributes;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.ExternalModules;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    [Validator(typeof(IssueCouponInputDto))]
    public class IssueCouponInputDto: TokenDTO
    {                                         

        public int PromotionId { get; set; }

        public int? PersonCustomerId { get; set; }

        public int? UserCustomerId { get; set; }

        public Guid UserSessionIssue { get; set; }

                                                        
    }
    public class IssueCouponInputDtoValidator : AbstractValidator<IssueCouponInputDto>
    {
        public IssueCouponInputDtoValidator()
        {
            RuleFor(x => x.PromotionId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} é obrigatório",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A promoção é obrigatória."
                });

        }

    }
}
