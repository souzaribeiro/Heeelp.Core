using System;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using FluentValidation;
using FluentValidation.Attributes;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    [Validator(typeof(UserFirstAccessDTO))]
    public class UserFirstAccessDTO
    {                                 
                         
        public Guid IntegrationCode { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public bool UserAgree { get; set; }
    }

    public class UserFirstAccessDTOValidator : AbstractValidator<UserFirstAccessDTO>
    {
        public UserFirstAccessDTOValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .Length(4, 10)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
                    DeveloperMessageTemplate = "{0} precisa ter entre 4 e 10 caracteres",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A senha do usuário precisa ter entre 4 e 10 caracteres"
                });

        }
    }

}
