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
    [Validator(typeof(PersonDetailsDTO))]
    public class PersonDetailsDTO
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string FantasyName { get; set; }
        public DbGeography Coordinates { get; set; }
        public string Address { get; set; }
        public string CustomDescription { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public long FileIdLogo { get; set; }
        public string Document { get; set; }
        public string PostCode { get; set; }
        public string WebSite { get; set; }
        public string UrlImageLogo { get; set; }



        //public string ExpertiseId { get; set; }
        //public string ExptiseName { get; set; }
        //public string ExpertiseDefaultDescription { get; set; }
    }
    public class PersonDetailsDTOValidator : AbstractValidator<PersonDetailsDTO>
    {
        public PersonDetailsDTOValidator()
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

            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 70)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O nome deve ter entre 1 e 70 caracteres."
                });

            RuleFor(x => x.FantasyName)
                .Length(1, 50)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O nome fantasia deve ter entre 1 e 50 caracteres."
                });


            RuleFor(x => x.WebSite)
                .Matches(GeneralRegularExpressions.RegexValidateURL)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidURL,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "Url inválida."
                });

            RuleFor(x => x.PhoneNumber)
                .Matches(GeneralRegularExpressions.RegexValidatePhoneNumber)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidPhoneNumber,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "Telefone inválido."
                });
        }
    }
}
