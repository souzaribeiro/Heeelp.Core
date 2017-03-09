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
    [Validator(typeof(FileTempListDTO))]
    public class FileTempListDTO
    {
        public int FileTempId { get; set; }

        public string File { get; set; }

    }
    public class FileTempListDTOValidator : AbstractValidator<FileTempListDTO>
    {
        public FileTempListDTOValidator()
        {
            RuleFor(x => x.FileTempId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O arquivo é obrigatória."
                });
        }
    }
}
