using System;
using System.Collections;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using FluentValidation;
using FluentValidation.Attributes;
using System.Collections.Generic;

namespace Heeelp.Core.Domain.ReadModel.DTO
{

    [Validator(typeof(CompanyExpertiseDTO))]
    public class CompanyExpertiseDTO
    {
        public Guid CompanyId { get; set; }
        public int ExpertiseId { get; set; }
    }

    public class CompanyExpertiseDTOValidator : AbstractValidator<CompanyExpertiseDTO>
    {
        public CompanyExpertiseDTOValidator()
        {
            RuleFor(x => x.CompanyId)
               .NotEmpty()
               .WithState(x => new ErrorState
               {
                   ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
                   DeveloperMessageTemplate = "{0} da empresa é obrigatório",
                   DocumentationPath = "/Usernames",
                   UserMessage = "O id da empresa é obrigatório"
               });
            RuleFor(x => x.ExpertiseId)
               .NotEmpty()
               .WithState(x => new ErrorState
               {
                   ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
                   DeveloperMessageTemplate = "{0} do expertise é obrigatório",
                   DocumentationPath = "/Usernames",
                   UserMessage = "O id do expertise é obrigatório"
               });
        }
    }
}
