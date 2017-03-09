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
    [Validator(typeof(PersonProfileListDTO))]
    public class PersonProfileListDTO
    {
        public int PersonProfileId { get; set; }

        public string  Name { get; set; }

    }
    public class PersonProfileListDTOValidator : AbstractValidator<PersonProfileListDTO>
    {
        public PersonProfileListDTOValidator()
        {
            
            RuleFor(x => x.PersonProfileId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A pessoa é obrigatória."
                });
        }
    }
}
