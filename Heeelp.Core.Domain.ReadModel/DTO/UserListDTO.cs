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
    [Validator(typeof(UserListDTO))]
    public class UserListDTO
    {
        public int UserId { get; set; }

        public string  Name { get; set; }

    }
    public class UserListDTOValidator : AbstractValidator<UserListDTO>
    {
        public UserListDTOValidator()
        {
            RuleFor(x => x.UserId)
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
