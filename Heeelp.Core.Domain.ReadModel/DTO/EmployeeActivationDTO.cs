using System;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using FluentValidation;
using FluentValidation.Attributes;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    [Validator(typeof(EmployeeActivationDTO))]
    public class EmployeeActivationDTO
    {

        public Guid EmployeeId { get; set; }
        public string PassWord { get; set; }
    }

    public class EmployeeActivationDTOValidator : AbstractValidator<EmployeeActivationDTO>
    {
        public EmployeeActivationDTOValidator()
        {
            RuleFor(x => x.EmployeeId)
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
        }

    }

}
