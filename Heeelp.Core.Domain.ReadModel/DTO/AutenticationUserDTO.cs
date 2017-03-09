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
    [Validator(typeof(AutenticationUserDTO))]
    public class AutenticationUserDTO
    {
        public int UserId { get; set; }

        public string  Name { get; set; }

        public string Email { get; set; }

        public string LoginPassword { get; set; }

    }


    public class AutenticationUserDTOValidator : AbstractValidator<AutenticationUserDTO>
    {
        public AutenticationUserDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidEmail,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O email está inválido."
                });

            RuleFor(x => x.Name)
                .Length(1, 50)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O nome deve ter entre 1 e 50 caracteres."
                });

            RuleFor(x => x.LoginPassword)
                .NotEmpty()
                .Length(4, 20)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
                    DeveloperMessageTemplate = "{0} senha inválida",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A senha está inválida."
                });
        }
    }

}
