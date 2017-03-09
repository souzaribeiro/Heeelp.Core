using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using FluentValidation;
using FluentValidation.Attributes;


namespace Heeelp.Core.Domain.ReadModel.DTO
{
    [Validator(typeof(UserUpdateDTO))]
    public class UserUpdateDTO
    {
        [Required]
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string SecundaryEmail { get; set; }

        public string SmartPhoneNumber { get; set; }

        public byte? UserProfileId { get; set; }

        public byte? UserStatusId { get; set; }

        public bool? Active { get; set; }

        public string LoginPassword { get; set; }

    }
    public class UserUpdateDTOValidator : AbstractValidator<UserUpdateDTO>
    {
        public UserUpdateDTOValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A pessoa é obrigatória."
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

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidEmail,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O email é inválido."
                });

            RuleFor(x => x.SecundaryEmail)
                .EmailAddress()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidEmail,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O email secundário é inválido."
                });



            RuleFor(x => x.SmartPhoneNumber)
                .Matches(GeneralRegularExpressions.RegexValidatePhoneNumber)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidPhoneNumber,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O telefone é inválido."
                });
        }
    }
}
