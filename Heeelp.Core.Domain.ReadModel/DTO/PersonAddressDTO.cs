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
    [Validator(typeof(PersonAddressDTO))]
    public class PersonAddressDTO
    {
        public int PersonAddressId { get; set; }

        public int PersonId { get; set; }

        public byte AddressTypeId { get; set; }

        public DateTime StartDateUTC { get; set; }

        public string StreetName { get; set; }

        public string Number { get; set; }

        public int NeighbourhoodId { get; set; }

        public string Neighbourhood { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string PostCode { get; set; }

        public string Coordinates { get; set; }

        public string ContactPhoneNumber { get; set; }

        public short ServerInstanceId { get; set; }

        public int CreatedBy { get; set; }

        public string ContactEMail { get; set; }

        public bool Active { get; set; }

        public Guid PersonIntegrationID { get; set; }

    }
    public class PersonAddressDTOValidator : AbstractValidator<PersonAddressDTO>
    {
        public PersonAddressDTOValidator()
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

            RuleFor(x => x.AddressTypeId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O tipo de endereço é obrigatório."
                });

            RuleFor(x => x.StartDateUTC)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A data inicial é obrigatória."
                });

            RuleFor(x => x.PostCode)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O CEP é obrigatório."
                });

            RuleFor(x => x.ContactPhoneNumber)
                .NotEmpty()
                .Matches(GeneralRegularExpressions.RegexValidatePhoneNumber)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidPhoneNumber,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O telefone é inválido."
                });

            RuleFor(x => x.ServerInstanceId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O campo server instance é obrigatório."
                });

            RuleFor(x => x.CreatedBy)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O campo criador é obrigatório."
                });

            RuleFor(x => x.Active)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O campo ativo é obrigatório."
                });
        }
    }
}
