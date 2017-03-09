using System;
using System.Collections;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using FluentValidation;
using FluentValidation.Attributes;
using System.Collections.Generic;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    [Validator(typeof(CompanyDetailsDTO))]
    public class CompanyDetailsDTO
    {

        public Guid CompanyIntegrationCode { get; set; }
        public int PersonId { get; set; }

        public string Name { get; set; }

        public string FantasyName { get; set; }

        public byte PersonOriginTypeId { get; set; }

        public string PersonOriginDetails { get; set; }

        public long? CampaignId { get; set; }

        public byte CountryId { get; set; }

        public byte LanguageId { get; set; }

        public byte PersonTypeId { get; set; }

        public byte PersonStatusId { get; set; }

        public string PersonStatusName { get; set; }

        public string PersonTypeName { get; set; }

        public string FriendlyNameURL { get; set; }

        public string PersonalWebSite { get; set; }
        public string CustomHeeelpPersonDomain { get; set; }

        public string CustomClubName { get; set; }
        public string CustomClubLogo { get; set; }
        public byte SkinId { get; set; }
        public string PhoneNumber { get; set; }

        public int? PersonFatherId { get; set; }

        public bool Active { get; set; }

        public string ImgDocument64 { get; set; }

        public ICollection<PersonRules> Rules { get; set; }

        public virtual CompanyInformationDTO CompanyInformation { get; set; }

        public string UrlImageLogo { get; set; }
        public string StreetName { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Neighbourhood { get; set; }
        public string PostCode { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactEMail { get; set; }
        public string State { get; set; }
        public string City { get; set; }

    }

    public class CompanyDetailsDTOValidator : AbstractValidator<CompanyDetailsDTO>
    {
        public CompanyDetailsDTOValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .Length(4, 50)
               .WithState(x => new ErrorState
               {
                   ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
                   DeveloperMessageTemplate = "{0} da empresa precisa ter entre 4 e 50 caracteres",
                   DocumentationPath = "/Usernames",
                   UserMessage = "A Razao Social da empresa precisa ter entre 4 e 50 caracteres."
               });
            RuleFor(x => x.FantasyName)
                .NotEmpty()
                .Length(4, 50)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
                    DeveloperMessageTemplate = "{0} da empresa precisa ter entre 4 e 50 caracteres",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O nome fantasia da empresa precisa ter entre 4 e 50 caracteres."
                });
            RuleFor(x => x.FriendlyNameURL)
                .NotEmpty()
                .Length(4, 50)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
                    DeveloperMessageTemplate = "{0} da empresa precisa ter entre 4 e 50 caracteres",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O nome da empresa precisa ter entre 4 e 50 caracteres."
                });

            //RuleFor(x => x.PersonalWebSite).Matches(GeneralRegularExpressions.RegexValidateURL)
            //   .WithState(x => new ErrorState
            //   {
            //       ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidURL,
            //       DeveloperMessageTemplate = "{0} web site inválido",
            //       DocumentationPath = "/Usernames",
            //       UserMessage = "O web site informado para a empresa não é válido."
            //   });

            RuleFor(x => x.PhoneNumber).Matches(GeneralRegularExpressions.RegexValidatePhoneNumber)
               .WithState(x => new ErrorState
               {
                   ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidPhoneNumber,
                   DeveloperMessageTemplate = "{0} Telefone da empresa inválido",
                   DocumentationPath = "/Usernames",
                   UserMessage = "O telefone informado para a empresa não é válido."
               });

            //RuleFor(x => x.PersonFatherId)
            //   .NotEmpty()
            //   .WithState(x => new ErrorState
            //   {
            //       ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
            //       DeveloperMessageTemplate = "{0} inválido",
            //       DocumentationPath = "/Usernames",
            //       UserMessage = "O pai é obrigatório."
            //   });

            //RuleFor(x => x.Active)
            //   .NotEmpty()
            //   .WithState(x => new ErrorState
            //   {
            //       ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
            //       DeveloperMessageTemplate = "{0} inválido",
            //       DocumentationPath = "/Usernames",
            //       UserMessage = "Informe se a empresa está ativa."
            //   });
        }
    }

}
