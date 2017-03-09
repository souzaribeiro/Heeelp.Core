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
    [Validator(typeof(CountryListDTO))]
    public class CountryListDTO
    {
        public int CountryId { get; set; }

        public string  Name { get; set; }

    }
    public class CountryListDTOValidator : AbstractValidator<CountryListDTO>
    {
        public CountryListDTOValidator()
        {
            RuleFor(x => x.CountryId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O país está inválido."
                });
        }
    }
}
