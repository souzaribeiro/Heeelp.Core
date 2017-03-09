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
    [Validator(typeof(PersonAddressClassifiedDTO))]
    public class PersonAddressClassifiedDTO
    {
        public int PersonAddressId { get; set; }

        public string Name { get; set; }
        public string FantasyName { get; set; }

        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public int PersonId { get; set; }

        public int StateId { get; set; }

        public string StateName { get; set; }

        public int CityId { get; set; }

        public string CityName { get; set; }

        public int NeighbourhoodId { get; set; }

        public string NeighbourhoodName { get; set; }

        public DbGeography Coordinates { get; set; }

        public long? PersonLogo { get; set; }

        public string Address { get; set; }
        public string UrlImageLogo { get; set; }

        public string PhoneNumber { get; set; }
        public string PersonDocumentNumber { get; set; }

    }
    public class PersonAddressClassifiedDTOValidator : AbstractValidator<PersonAddressClassifiedDTO>
    {
        public PersonAddressClassifiedDTOValidator()
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
        }
    }
}
