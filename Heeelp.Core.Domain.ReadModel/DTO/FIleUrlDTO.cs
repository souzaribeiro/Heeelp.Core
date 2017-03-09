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
    [Validator(typeof(FIleUrlDTO))]
    public class FIleUrlDTO
    {
        public long FileId { get; set; }
        public string URL { get; set; }
    }
    public class FIleUrlDTOValidator : AbstractValidator<FIleUrlDTO>
    {
        public FIleUrlDTOValidator()
        {
            RuleFor(x => x.FileId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O arquivo é obrigatório."
                });

            RuleFor(x => x.URL)
                .Matches(GeneralRegularExpressions.RegexValidateURL)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidURL,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A URL inválida."
                });
        }
    }
}
