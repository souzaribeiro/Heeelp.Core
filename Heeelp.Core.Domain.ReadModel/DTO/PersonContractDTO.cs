using System;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using FluentValidation;
using FluentValidation.Attributes;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    [Validator(typeof(PersonContractDTO))]
    public class PersonContractDTO
    {

        public Guid Id { get; set; }

        public int PersonContractId { get; set; }

        public int PersonId { get; set; }

        public short ContractId { get; set; }

        public int UserId { get; set; }

        public DateTime AgreementDateUTC { get; set; }
        
        public long FileId { get; set; }
    }

    public class PersonContractDTOValidator : AbstractValidator<PersonContractDTO>
    {
        public PersonContractDTOValidator()
        {
            RuleFor(x => x.PersonId)
                .NotNull()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} é obrigatório",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A identificação do contratante é obrigatória."
                });
            RuleFor(x => x.ContractId)
                .NotNull()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} é obrigatório",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A identificação do contrato é obrigatória."
                });
            RuleFor(x => x.UserId)
                .NotNull()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} é obrigatório",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A identificação do usuário é obrigatória."
                });
            RuleFor(x => x.AgreementDateUTC)
                .NotNull()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} é obrigatório",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A data é obrigatória."
                });

            RuleFor(x => x.FileId)
                .NotNull()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} é obrigatório",
                    DocumentationPath = "/Usernames",
                    UserMessage = "A identificação dos termos do contrato é obrigatória"
                });

        }

    }

}
