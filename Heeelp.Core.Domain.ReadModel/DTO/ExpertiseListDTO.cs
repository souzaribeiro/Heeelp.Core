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
    [Validator(typeof(ExpertiseListDTO))]
    public class ExpertiseListDTO
    {
        public int ExpertiseId { get; set; }

        public string  Name { get; set; }

        public bool Active { get; set; }

        public string DefaultDescription { get; set; }

        public DateTime CreatedDateUTC { get; set; }

        public IEnumerable<ExpertiseListDTO> SubExpertises { get; set; }

    }
    public class ExpertiseListDTOValidator : AbstractValidator<ExpertiseListDTO>
    {
        public ExpertiseListDTOValidator()
        {
            RuleFor(x => x.ExpertiseId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A expertise é obrigatória."
                });
        }
    }
}
