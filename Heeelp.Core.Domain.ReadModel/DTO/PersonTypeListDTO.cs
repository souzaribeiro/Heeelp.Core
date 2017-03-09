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
    [Validator(typeof(PersonTypeListDTO))]
    public class PersonTypeListDTO
    {
        public int PersonTypeId { get; set; }

        public string  Name { get; set; }

    }
    public class PersonTypeListDTOValidator : AbstractValidator<PersonTypeListDTO>
    {
        public PersonTypeListDTOValidator()
        {
            RuleFor(x => x.PersonTypeId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O tipo de pessoa é obrigatório."
                });
        }
    }
}
