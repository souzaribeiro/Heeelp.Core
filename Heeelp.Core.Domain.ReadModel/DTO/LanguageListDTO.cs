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
    [Validator(typeof(LanguageListDTO))]
    public class LanguageListDTO
    {
        public int LanguageId { get; set; }

        public string  Name { get; set; }

    }
    public class LanguageListDTOValidator : AbstractValidator<LanguageListDTO>
    {
        public LanguageListDTOValidator()
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
        }
    }
}
