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
    [Validator(typeof(ExpertisePhotoDTO))]
    public class ExpertisePhotoDTO
    {
        public int ExpertiseId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int? ExpertiseFatherId { get; set; }

        public List<long> ImageListId { get; set; }

        public IEnumerable<string> ImageUrlList { get; set; }

        public List<FIleUrlDTO> fileUrl { get; set; }


    }
    public class ExpertisePhotoDTOValidator : AbstractValidator<ExpertisePhotoDTO>
    {
        public ExpertisePhotoDTOValidator()
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

            RuleFor(x => x.ExpertiseFatherId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A expertise pai é obrigatória."
                });

            RuleFor(x => x.ImageListId)
               .NotEmpty()
               .WithState(x => new ErrorState
               {
                   ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                   DeveloperMessageTemplate = "{0} invalido",
                   DocumentationPath = "/Usernames",
                   UserMessage = "A imagem é obrigatória."
               });
        }
    }

}
