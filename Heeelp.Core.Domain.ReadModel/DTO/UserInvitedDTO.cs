using FluentValidation;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    public class UserInvitedDTO
    {
        public string Email { get; set; }
        public string InviteCode { get; set; }
        public string Name { get; set; }
        public string EnrollmentIP { get; set; }
    }
    public class UserInvitedDTOValidator : AbstractValidator<UserInvitedDTO>
    {
        public UserInvitedDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
                    DocumentationPath = "/Usernames",
                    UserMessage = "O Email é obrigatório."
                });
            RuleFor(x => x.Name)
               .NotEmpty()
               .WithState(x => new ErrorState
               {
                   ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
                   DocumentationPath = "/Usernames",
                   UserMessage = "O Nome é obrigatório."
               });
        }
    }
}
