using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using FluentValidation;
using System.Data.Entity.Spatial;
using FluentValidation.Attributes;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    [Validator(typeof(MarketingProspectDTO))]
    public class MarketingProspectDTO
    {

        public string PlaceName { get; set; }

        public string PlaceAddress { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }

        public string PhoneNumber { get; set; }

        //public int? RecommendedBy { get; set; }      
    }
    public class MarketingProspectDTOValidator : AbstractValidator<MarketingProspectDTO>
    {
        public MarketingProspectDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(4, 50)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
                    DeveloperMessageTemplate = "{0} precisa ter entre 4 e 50 caracteres",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O nome precisa ter entre 4 e 50 caracteres"
                });
            RuleFor(x => x.Email).EmailAddress()
              .WithState(x => new ErrorState
              {
                  ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidEmail,
                  DeveloperMessageTemplate = "{0} E-mail inválido",
                  DocumentationPath = "/Usernames",
                  UserMessage = "O email informado não é válido."
              });
            RuleFor(x => x.PhoneNumber).Matches(GeneralRegularExpressions.RegexValidatePhoneNumber)
               .WithState(x => new ErrorState
               {
                   ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidPhoneNumber,
                   DeveloperMessageTemplate = "{0} Telefone é inválido",
                   DocumentationPath = "/Usernames",
                   UserMessage = "O telefone informado não é válido."
               });

        }
    }

}
