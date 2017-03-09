using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Attributes;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;


namespace Heeelp.Core.Domain.ReadModel.DTO
{
    [Validator(typeof(PersonBenefitClubDTO))]
    public class PersonBenefitClubDTO
    {
        public string CustomHeeelpPersonDomain { get; set; }
        public string CustomClubName { get; set; }
        public string CustomClubLogo { get; set; }
        public string Description { get; set; }

    }
    public class PersonBenefitClubDTOValidator : AbstractValidator<PersonBenefitClubDTO>
    {
        public PersonBenefitClubDTOValidator()
        {
            RuleFor(x => x.CustomHeeelpPersonDomain).Matches(GeneralRegularExpressions.RegexValidateURL)
               .WithState(x => new ErrorState
               {
                   ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidPhoneNumber,
                   DeveloperMessageTemplate = "{0}  inválido",
                   DocumentationPath = "/Usernames",
                   UserMessage = "O dominio informado para o clube de beneficios não é válido."
               });

            RuleFor(x => x.CustomClubName)
                .NotEmpty()
                .Length(1, 3)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O código deve ter entre 1 e 3 caracteres."
                });

            RuleFor(x => x.CustomClubLogo).Matches(GeneralRegularExpressions.RegexValidateURL)
               .WithState(x => new ErrorState
               {
                   ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidPhoneNumber,
                   DeveloperMessageTemplate = "{0}  inválido",
                   DocumentationPath = "/Usernames",
                   UserMessage = "O endereco informado para o logotipo do clube de beneficios não é válido."
               });

            RuleFor(x => x.Description)
                .NotEmpty()
                .Length(1, 5000)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O código deve ter entre 1 e 5000 caracteres."
                });

        }
    }
}
