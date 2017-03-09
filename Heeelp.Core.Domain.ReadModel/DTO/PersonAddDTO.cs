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
    [Validator(typeof(PersonAddDTO))]
    public class PersonAddDTO
    {    
       
        public string Name { get; set; }

        public string FantasyName { get; set; }

        //public string NameFromSecurityCheck { get; set; }

        //public int? SecuritySourceId { get; set; }

        //public bool? IsSafe { get; set; }

        //public string FriendlyNameURL { get; set; }

        public byte PersonOriginTypeId { get; set; }

        //public string PersonOriginDetails { get; set; }

        public long? CampaignId { get; set; }

        public byte CountryId { get; set; }

        public byte LanguageId { get; set; }

        public byte PersonTypeId { get; set; }

        public byte PersonProfileId { get; set; }

        public byte PersonStatusId { get; set; }

        //public string PersonalWebSite { get; set; }

        public byte CurrencyId { get; set; }

        public DateTime CreationDateUTC { get; set; }

        //public string ActivationCode { get; set; }

        public DateTime? ActivationDateUTC { get; set; }

        public string PhoneNumber { get; set; }

        //public int? PersonFatherId { get; set; }

        public long? InviteId { get; set; }

        public short ServerInstanceId { get; set; }

        public bool Active { get; set; }

        public string ImgDocument64 { get; set; }

    }
    public class PersonAddDTOValidator : AbstractValidator<PersonAddDTO>
    {
        public PersonAddDTOValidator()
        {
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

            RuleFor(x => x.PersonOriginTypeId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O tipo de person origen é obrigatório."
                });


            RuleFor(x => x.LanguageId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O idioma é obrigatório."
                });

            RuleFor(x => x.PersonTypeId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O tipo de pessoa é obrigatório."
                });

            RuleFor(x => x.PersonStatusId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O status é obrigatório."
                });

            RuleFor(x => x.CurrencyId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A moeda é obrigatória."
                });

            RuleFor(x => x.CreationDateUTC)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A data de criação é obrigatória."
                });

            RuleFor(x => x.Active)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O campo ativo obrigatório."
                });

            RuleFor(x => x.PhoneNumber).Matches(GeneralRegularExpressions.RegexValidatePhoneNumber)
               .WithState(x => new ErrorState
               {
                   ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidPhoneNumber,
                   DeveloperMessageTemplate = "{0} Telefone da empresa inválido",
                   DocumentationPath = "/Usernames",
                   UserMessage = "O telefone informado para a empresa não é válido."
               });

            RuleFor(x => x.ImgDocument64)
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
