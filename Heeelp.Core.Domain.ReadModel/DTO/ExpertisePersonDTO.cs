using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using FluentValidation;
using FluentValidation.Attributes;


namespace Heeelp.Core.Domain.ReadModel.DTO
{
    [Validator(typeof(PersonExpertiseDTO))]
    public class PersonExpertiseDTO
    {                           

        public int PersonId { get; set; }

        public List<int> ExpertiseListId { get; set; }

        public int InsertedBy { get; set; }
     
        public byte ExhibitionOrder { get; set; }

        public Guid PersonIntegrationId { get; set; }
    }
    public class PersonExpertiseDTOValidator : AbstractValidator<PersonExpertiseDTO>
    {
        public PersonExpertiseDTOValidator()
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

            RuleFor(x => x.ExpertiseListId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A expertise é obrigatória."
                });

            RuleFor(x => x.PersonIntegrationId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A person integration é obrigatória."
                });
        }
    }
}
