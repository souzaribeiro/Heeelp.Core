using System;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using FluentValidation;
using FluentValidation.Attributes;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    [Validator(typeof(CompanyCoworkerDTO))]
    public class CompanyCoworkerDTO
    {
        
        public Guid? CompanyId { get; set; }
        public string CompanyName { get; set; }

        public string FantasyName { get; set; }

        public string FriendlyNameURL { get; set; }

        public byte? PersonStatusId { get; set; }

        public string PersonStatusName { get; set; }

        public string PersonalWebSite { get; set; }

        public string CompanyPhoneNumber { get; set; }

        public string ManagerName { get; set; }

        public string ManagerSmartPhoneNumber { get; set; }

        public string ManagerEmail { get; set; }

    }

    public class CompanyCoworkerDTOValidator : AbstractValidator<CompanyCoworkerDTO>
    {
        public CompanyCoworkerDTOValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty()
                .Length(4, 50)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
                    DeveloperMessageTemplate = "{0} da empresa precisa ter entre 4 e 50 caracteres",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A Razao Social da empresa precisa ter entre 4 e 50 caracteres."
                });
            RuleFor(x => x.FantasyName)
                .NotEmpty()
                .Length(4, 50)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
                    DeveloperMessageTemplate = "{0} da empresa precisa ter entre 4 e 50 caracteres",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O nome fantasia da empresa precisa ter entre 4 e 50 caracteres."
                });
            RuleFor(x => x.FriendlyNameURL)
                .NotEmpty()
                .Length(4, 50)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
                    DeveloperMessageTemplate = "{0} da empresa precisa ter entre 4 e 50 caracteres",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O nome da empresa precisa ter entre 4 e 50 caracteres."
                });

            RuleFor(x => x.PersonalWebSite).Matches(GeneralRegularExpressions.RegexValidateURL)
               .WithState(x => new ErrorState
               {
                   ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidPhoneNumber,
                   DeveloperMessageTemplate = "{0} web site inválido",
                   DocumentationPath = "/Usernames",
                   UserMessage = "O web site informado para a empresa não é válido."
               });

            RuleFor(x => x.CompanyPhoneNumber).Matches(GeneralRegularExpressions.RegexValidatePhoneNumber)
               .WithState(x => new ErrorState
               {
                   ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidPhoneNumber,
                   DeveloperMessageTemplate = "{0} Telefone da empresa inválido",
                   DocumentationPath = "/Usernames",
                   UserMessage = "O telefone informado para a empresa não é válido."
               });

            RuleFor(x => x.ManagerSmartPhoneNumber).Matches(GeneralRegularExpressions.RegexValidatePhoneNumber)
               .WithState(x => new ErrorState
               {
                   ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidPhoneNumber,
                   DeveloperMessageTemplate = "{0} Telefone do gestor inválido",
                   DocumentationPath = "/Usernames",
                   UserMessage = "O telefone informado para o gestor da empresa não é válido."
               });
        }
    }

}
