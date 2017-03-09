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
    [Validator(typeof(ServerInstanceListDTO))]
    public class ServerInstanceListDTO
    {
        public int ServerInstanceId { get; set; }

        public string  Name { get; set; }

    }
    public class ServerInstanceListDTOValidator : AbstractValidator<ServerInstanceListDTO>
    {
        public ServerInstanceListDTOValidator()
        {

            RuleFor(x => x.ServerInstanceId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O server instance é obrigatório."
                });
        }
    }
}
