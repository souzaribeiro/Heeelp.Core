using FluentValidation;
using FluentValidation.Attributes;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    [Validator(typeof(CompanyInformationDTO))]
    public class CompanyInformationDTO
    {
        public string StreetName { get; set; }

        public string Number { get; set; }

        public string Neighbourhood { get; set; }

        public string PostCode { get; set; }

        public string ContactEmail { get; set; }

        public Guid IntegrationCode { get; set; }

        public string DocumentNumber { get; set; }

        public int StateId { get; set; }
        public int CityId { get; set; }
        public string Complement { get; set; }

    }

    public class CompanyInformationDTOValidator : AbstractValidator<CompanyInformationDTO>
    {
        public CompanyInformationDTOValidator()
        {
            //RuleFor(x => x.StreetName)
            //    .NotEmpty()
            //    .Length(4, 150)
            //    .WithState(x => new ErrorState
            //    {
            //        ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
            //        DeveloperMessageTemplate = "{0} precisa do StreetName ter entre 4 e 150 caracteres",
            //        DocumentationPath = "/Usernames",
            //        UserMessage = "O nome da rua precisa ter entre 4 e 50 caracteres"
            //    });
            //RuleFor(x => x.ContactEmail).EmailAddress()
            //    .WithState(x => new ErrorState
            //    {
            //        ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidEmail,
            //        DeveloperMessageTemplate = "{0} E-mail da empresa inválido",
            //        DocumentationPath = "/Usernames",
            //        UserMessage = "O email informado para empresa não é válido."
            //    });
            //RuleFor(x => x.Number)
            //    .NotNull()
            //    .WithState(x => new ErrorState
            //    {
            //        ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
            //        DeveloperMessageTemplate = "{0} Numero do endereço não definido",
            //        DocumentationPath = "/Usernames",
            //        UserMessage = "O Numero do endereço é obrigatório"
            //    });
            //RuleFor(x => x.Neighbourhood)
            //    .NotNull()
            //    .WithState(x => new ErrorState
            //    {
            //        ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
            //        DeveloperMessageTemplate = "{0} Bairro do endereço não definido",
            //        DocumentationPath = "/Usernames",
            //        UserMessage = "O Bairro do endereço é obrigatório"
            //    });
            //RuleFor(x => x.PostCode)
            //    .NotNull()
            //    .WithState(x => new ErrorState
            //    {
            //        ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
            //        DeveloperMessageTemplate = "{0} CEP do endereço não definido",
            //        DocumentationPath = "/Usernames",
            //        UserMessage = "O CEP do endereço é obrigatório"
            //    });
            //RuleFor(x => x.IntegrationCode)
            //    .NotNull()
            //    .WithState(x => new ErrorState
            //    {
            //        ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
            //        DeveloperMessageTemplate = "{0} CEP do endereço não definido",
            //        DocumentationPath = "/Usernames",
            //        UserMessage = "O CEP do endereço é obrigatório"
            //    });

        }
    }
}
