using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using FluentValidation;
using FluentValidation.Attributes;


namespace Heeelp.Core.Domain.ReadModel.DTO
{
    [Validator(typeof(UserStatusListDTO))]
    public class UserStatusListDTO
    {
        public int UserStatusId { get; set; }

        public string  Name { get; set; }

    }
    public class UserStatusListDTOValidator : AbstractValidator<UserStatusListDTO>
    {
        public UserStatusListDTOValidator()
        {

            RuleFor(x => x.UserStatusId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O usuário é obrigatório."
                });
        }
    }
}
