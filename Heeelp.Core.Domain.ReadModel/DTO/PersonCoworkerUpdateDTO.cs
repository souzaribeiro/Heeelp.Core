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
    [Validator(typeof(PersonCoworkerUpdateDTO))]
    public class PersonCoworkerUpdateDTO
    {
        [Required]
        public int PersonId { get; set; }

        public string Name { get; set; }

        public string FantasyName { get; set; }

        public string FriendlyNameURL { get; set; }

        public byte? PersonTypeId { get; set; }

        public byte? PersonProfileId { get; set; }

        public byte? PersonStatusId { get; set; }

        public string PersonalWebSite { get; set; }

        public string PhoneNumber { get; set; }

    }
    public class PersonCoworkerUpdateDTOValidator : AbstractValidator<PersonCoworkerUpdateDTO>
    {
        public PersonCoworkerUpdateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 70)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O nome deve ter entre 1 e 70 caracteres."
                });

            RuleFor(x => x.FantasyName)
                .Length(1, 50)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O nome fantasia deve ter entre 1 e 50 caracteres."
                });

            RuleFor(x => x.PersonTypeId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O tipo de pessoa é obrigatório."
                });

            RuleFor(x => x.PersonStatusId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O status é obrigatório."
                });

            RuleFor(x => x.PersonalWebSite)
                .Matches(GeneralRegularExpressions.RegexValidateURL)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidURL,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "Url inválida."
                });

            RuleFor(x => x.PhoneNumber)
                .Matches(GeneralRegularExpressions.RegexValidatePhoneNumber)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidPhoneNumber,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "Telefone inválido."
                });
        }
    }
}
