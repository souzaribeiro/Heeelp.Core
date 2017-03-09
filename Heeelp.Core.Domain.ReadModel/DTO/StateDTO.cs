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
    [Validator(typeof(StateDTO))]
    public class StateDTO
    {
        public int StateId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int? CountryRegionId { get; set; }

        public DbGeography Coordinates { get; set; }

        public bool? Active { get; set; }

    }
    public class StateDTOValidator : AbstractValidator<StateDTO>
    {
        public StateDTOValidator()
        {

            RuleFor(x => x.Name)
                .Length(1, 50)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O nome deve ter entre 1 e 50 caracteres."
                });
        }
    }
}
