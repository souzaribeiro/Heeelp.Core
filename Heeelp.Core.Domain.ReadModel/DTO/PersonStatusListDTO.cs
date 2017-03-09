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
    [Validator(typeof(PersonStatusListDTO))]
    public class PersonStatusListDTO
    {
        public int PersonStatusId { get; set; }

        public string  Name { get; set; }

    }
    public class PersonStatusListDTOValidator : AbstractValidator<PersonStatusListDTO>
    {
        public PersonStatusListDTOValidator()
        {
            // Ajustar tamanho do nome
            RuleFor(x => x.PersonStatusId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O status é obrigatório."
                });
        }
    }
}
