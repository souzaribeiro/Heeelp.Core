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
    [Validator(typeof(UserListAssociateDTO))]
    public class UserListAssociateDTO
    {

        public int UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string SecundaryEmail { get; set; }

        public string SmartPhoneNumber { get; set; }

        public int PersonId { get; set; }

        public string UserProfileId { get; set; }

        public string  UserProfileName { get; set; }

        public string UserStatusId { get; set; }

        public string  UserStatusName { get; set; }

        public bool Active { get; set; }

        public string LoginPassword { get; set; }


    }
    public class UserListAssociateDTOValidator : AbstractValidator<UserListAssociateDTO>
    {
        public UserListAssociateDTOValidator()
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
