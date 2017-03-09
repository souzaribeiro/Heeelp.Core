using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Attributes;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;


namespace Heeelp.Core.Domain.ReadModel.DTO
{
    [Validator(typeof(CityDTO))]

    public class CityDTO
    {
        public int CityId { get; set; }

        public string Name { get; set; }

        public int StateRegionId { get; set; }

        public DbGeography Coordinates { get; set; }

        public string PostCode { get; set; }

        public bool Active { get; set; }

        public DateTime InsertedDate { get; set; }

        public string PhoneCode { get; set; }

    }
    public class CityDTOValidator : AbstractValidator<CityDTO>
    {
        public CityDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 70)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O nome deve estar entre 1 e 70 caracteres."
                });

            RuleFor(x => x.Coordinates)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O campo de coordenadas é obrigatório."
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

            RuleFor(x => x.InsertedDate)
                .NotEmpty()
                .WithState(x => new ErrorState

                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "data de inserção é obrigatória."
                });
        }
    }

}
