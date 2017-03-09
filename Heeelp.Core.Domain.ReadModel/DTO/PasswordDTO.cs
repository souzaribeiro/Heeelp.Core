using System;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using FluentValidation;
using FluentValidation.Attributes;


namespace Heeelp.Core.Domain.ReadModel.DTO
{
    [Validator(typeof(PasswordDTO))]
    public class PasswordDTO
    {
        public Guid IntegrationCode { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
    }
    public class PasswordDTOValidator : AbstractValidator<PasswordDTO>
    {
        public PasswordDTOValidator()
        {
            RuleFor(x => x.IntegrationCode)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
                    DeveloperMessageTemplate = "{0} é obrigatório",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O código do colaborador é obrigatório"
                });
            RuleFor(x => x.PassWord)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidEmail,
                    DeveloperMessageTemplate = "{0} é obrigatório",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A senha é obrigatória"
                });
            RuleFor(x => x.Email).EmailAddress()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidEmail,
                    DeveloperMessageTemplate = "{0} E-mail do colaborador inválido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O email informado para o colaborador não é válido."
                });
        }

    }



}
