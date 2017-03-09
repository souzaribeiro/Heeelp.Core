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
    [Validator(typeof(CurrencyListDTO))]
    public class CurrencyListDTO
    {
        public int CurrencyId { get; set; }

        public string  Name { get; set; }



    }
    public class CurrencyListDTOValidator : AbstractValidator<CurrencyListDTO>
    {
        public CurrencyListDTOValidator()
        {
            RuleFor(x => x.CurrencyId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A moeda está inválida."
                });
        }
    }
}
