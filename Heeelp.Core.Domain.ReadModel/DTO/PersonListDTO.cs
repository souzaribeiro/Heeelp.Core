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
    [Validator(typeof(PersonListDTO))]
    public class PersonListDTO
    {
        public int PersonId { get; set; }

        public string  Name { get; set; }

        public Guid? IntegrationCode { get; set; }
        public int? PersonFatherId { get; set; }

    }
    public class PersonListDTOValidator : AbstractValidator<PersonListDTO>
    {
        public PersonListDTOValidator()
        {
            RuleFor(x => x.PersonId)
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
