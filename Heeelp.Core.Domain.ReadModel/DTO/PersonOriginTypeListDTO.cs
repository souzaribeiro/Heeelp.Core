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
    [Validator(typeof(PersonOriginTypeListDTO))]
    public class PersonOriginTypeListDTO
    {
        public int PersonOriginTypeId { get; set; }

        public string  Name { get; set; }

    }
    public class PersonOriginTypeListDTOValidator : AbstractValidator<PersonOriginTypeListDTO>
    {
        public PersonOriginTypeListDTOValidator()
        {
            RuleFor(x => x.PersonOriginTypeId)
                 .NotEmpty()
                 .WithState(x => new ErrorState
                 {
                     ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                     DeveloperMessageTemplate = "{0} invalido",
                     DocumentationPath = "/Usernames",
                     UserMessage = "O person Origin Type é obrigatório."
                 });
        }
    }
}
