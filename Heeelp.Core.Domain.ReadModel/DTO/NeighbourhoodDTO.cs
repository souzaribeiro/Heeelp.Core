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
    [Validator(typeof(NeighbourhoodDTO))]
    public class NeighbourhoodDTO
    {
        public int NeighbourhoodId { get; set; }

        public string Name { get; set; }

        public int CityId { get; set; }

        public int? NeighbourhoodFatherId { get; set; }

        public DbGeography Coordinates { get; set; }

        public bool Active { get; set; }

        public DateTime InsertedDate { get; set; }

        public int? CityZoneId { get; set; }

        public string PostCode { get; set; }

    }
    public class NeighbourhoodDTOValidator : AbstractValidator<NeighbourhoodDTO>
    {
        public NeighbourhoodDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 50)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O nome deve entre 1 e 50 caracteres."
                });

            RuleFor(x => x.CityId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A cidade é obrigatória."
                });

            RuleFor(x => x.Active)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O campo de atividade é obrigatório."
                });

            RuleFor(x => x.InsertedDate)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A data é obrigatória"
                });
        }
    }
}
