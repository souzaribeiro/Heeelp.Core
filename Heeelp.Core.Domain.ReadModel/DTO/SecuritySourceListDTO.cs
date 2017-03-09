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
    [Validator(typeof(SecuritySourceListDTO))]
    public class SecuritySourceListDTO
    {
        public int SecuritySourceId { get; set; }

        public string  Name { get; set; }

    }
    public class SecuritySourceListDTOValidator : AbstractValidator<SecuritySourceListDTO>
    {
        public SecuritySourceListDTOValidator()
        {
            RuleFor(x => x.SecuritySourceId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O Security Id é obrigatório."
                });
        }
    }
}
