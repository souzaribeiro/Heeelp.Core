using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Attributes;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;


namespace Heeelp.Core.Domain.ReadModel.DTO
{
    [Validator(typeof(CountryDTO))]
    public class CountryDTO
    {
        public byte CountryId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string PhoneCode { get; set; }

        public byte LanguageId { get; set; }

        public int CurrencyId { get; set; }

    }
    public class CountryDTOValidator : AbstractValidator<CountryDTO>
    {
        public CountryDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 50)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O nome deve ter entre 1 e 50 caracteres."
                });

            RuleFor(x => x.Code)
                .NotEmpty()
                .Length(1, 3)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O código deve ter entre 1 e 3 caracteres."
                });

            RuleFor(x => x.PhoneCode)
                .NotEmpty()
                .Length(2)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O DDD deve ter 2 caracteres."
                });

            RuleFor(x => x.LanguageId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A linguagem é obrigatória."
                });

            RuleFor(x => x.CurrencyId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A moeda é obrigatória."
                });
        }
    }
}
