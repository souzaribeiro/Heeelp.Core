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
    [Validator(typeof(UserProfileListDTO))]
    public class UserProfileListDTO
    {
        public int UserProfileId { get; set; }

        public string  Name { get; set; }

    }
    public class UserProfileListDTOValidator : AbstractValidator<UserProfileListDTO>
    {
        public UserProfileListDTOValidator()
        {

            RuleFor(x => x.UserProfileId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O user é obrigatório."
                });
        }
    }
}
