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
    [Validator(typeof(UserAddDTO))]
    public class UserAddDTO
    {                                     
                                                     

        public string Name { get; set; }

        public string Email { get; set; }

        public string SecundaryEmail { get; set; }

        public string SmartPhoneNumber { get; set; }

        public int PersonId { get; set; }

        public bool IsDefaultUser { get; set; }

        public byte UserProfileId { get; set; }

        public byte UserStatusId { get; set; }

        public int AuthenticationModeId { get; set; }

        public byte? LanguageId { get; set; }

        public DateTime CreationDateUTC { get; set; }

        public DateTime? ValidationDateUTC { get; set; }

        public short? FormFillTime { get; set; }

        public string ValidationToken { get; set; }

        public bool? SecurityCheckNecessary { get; set; }

        public short? ValidationAttempts { get; set; }

        public short? LoginFailAttempts { get; set; }

        public DateTime? LastLoginFailUTC { get; set; }

        public bool Active { get; set; }

        public string EnrollmentIP { get; set; }

        public string ValidationIP { get; set; }

        public int? CreatedBy { get; set; }

        public short ServerInstanceId { get; set; }

        public bool? IsPerpetual { get; set; }

        public List<int> listFileTemp { get; set; }

        public string LoginPassword { get; set; }

        public Guid PersonIntegrationId { get; set; }

    }
    public class UserAddDTOValidator : AbstractValidator<UserAddDTO>
    {
        public UserAddDTOValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 50)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O nome deve ter entre 1 e 50 caracteres."
                });

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidEmail,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O email é obrigatório."
                });

            RuleFor(x => x.PersonId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A pessoa é obrigatória."
                });

            RuleFor(x => x.IsDefaultUser)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O usuário é padrão?."
                });
        }
    }
}
