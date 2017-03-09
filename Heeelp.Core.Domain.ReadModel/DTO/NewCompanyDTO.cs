using System;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using FluentValidation;
using FluentValidation.Attributes;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    [Validator(typeof(NewCompanyDTO))]
    public class NewCompanyDTO
    {
        public string CompanyName { get; set; }
        public string FantasyName { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyPhoneNumber { get; set; }
        public string ManagerName { get; set; }
        public string ManagerEmail { get; set; }
        public string ManagerSmartPhoneNumber { get; set; }
        public Guid CompanyId { get; set; }
        public int CreatedBy { get; set; }
        public string EnrollmentIP { get; set; }
        public byte SkinId { get; set; }
        public string CustomHeeelpPersonDomain { get; set; }
        public string CustomClubName { get; set; }
        public Guid PersonIntegrationFatherId { get; set; }
    }

    public class NewCompanyDTOValidator : AbstractValidator<NewCompanyDTO>
    {
        public NewCompanyDTOValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty()
                .Length(4, 50)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
                    DeveloperMessageTemplate = "{0} da empresa precisa ter entre 4 e 50 caracteres",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O nome da empresa precisa ter entre 4 e 50 caracteres."
                });
            RuleFor(x => x.CompanyEmail).EmailAddress()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidEmail,
                    DeveloperMessageTemplate = "{0} E-mail da empresa inválido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O email informado para a empresa não é válido."
                });
            RuleFor(x => x.CompanyPhoneNumber).Matches(GeneralRegularExpressions.RegexValidatePhoneNumber)
               .WithState(x => new ErrorState
               {
                   ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidPhoneNumber,
                   DeveloperMessageTemplate = "{0} Telefone da empresa inválido",
                   DocumentationPath = "/Usernames",
                   UserMessage = "O telefone informado para a empresa não é válido."
               });
            RuleFor(x => x.ManagerName)
                .NotEmpty()
                .Length(4, 50)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
                    DeveloperMessageTemplate = "{0} precisa do gestor ter entre 4 e 50 caracteres",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O nome do gestor precisa ter entre 4 e 50 caracteres"
                });
            RuleFor(x => x.ManagerEmail).EmailAddress()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidEmail,
                    DeveloperMessageTemplate = "{0} E-mail do gestor inválido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O email informado para o gestor não é válido."
                });
            RuleFor(x => x.ManagerSmartPhoneNumber).Matches(GeneralRegularExpressions.RegexValidatePhoneNumber)
               .WithState(x => new ErrorState
               {
                   ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidPhoneNumber,
                   DeveloperMessageTemplate = "{0} Telefone  do gestor inválido",
                   DocumentationPath = "/Usernames",
                   UserMessage = "O telefone informado para o gestor não é válido."
               });
            RuleFor(x => x.EnrollmentIP).Matches(GeneralRegularExpressions.RegexValidateIP)
               .WithState(x => new ErrorState
               {
                   ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidIP,
                   DeveloperMessageTemplate = "{0} IP inválido",
                   DocumentationPath = "/Usernames",
                   UserMessage = "O IP detectado não é válido."
               });
           
        }
    }

}
