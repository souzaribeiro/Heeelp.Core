using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Attributes;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;


namespace Heeelp.Core.Domain.ReadModel.DTO
{
    [Validator(typeof(ExpertiseDTO))]
    public class ExpertiseDTO
    {
        public int ExpertiseId { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

        public int? ExpertiseFatherId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDateUTC { get; set; }

        public int ApprovalStatusId { get; set; }

        public int ApprovedBy { get; set; }

        public DateTime ApprovedDate { get; set; }

        public string DefaultDescription { get; set; }

        public bool IsPriceDefinedEditorially { get; set; }


    }
    public class ExpertiseDTOValidator : AbstractValidator<ExpertiseDTO>
    {
        public ExpertiseDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 50)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
                    DeveloperMessageTemplate = "{0} invalido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O nome deve ter entre 1 e 50 caracteres."
                });

            RuleFor(x => x.Active)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} requerido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O campo ativo é obrigatório."
                });

            RuleFor(x => x.CreatedBy)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} requerido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O criador é obrigatório."
                });

            RuleFor(x => x.CreatedDateUTC)
               .NotEmpty()
               .WithState(x => new ErrorState
               {
                   ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                   DeveloperMessageTemplate = "{0} requerido",
                   DocumentationPath = "/Usernames",
                   UserMessage = "A data de criação é obrigatória."
               });

            RuleFor(x => x.ApprovalStatusId)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} requerido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O campo de aprovação é obrigatório."
                });

            RuleFor(x => x.ApprovedBy)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} requerido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O campo de quem aprovou é obrigatório."
                });

            RuleFor(x => x.ApprovedDate)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} requerido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O campo de data de aprovação é obrigatório."
                });

            RuleFor(x => x.DefaultDescription)
                .NotEmpty()
                .Length(1, 150)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} requerido e deve estar entre 1-150 caracteres",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O campo de descrição deve estar entre 1 e 150 caracteres."
                });

            RuleFor(x => x.IsPriceDefinedEditorially)
                .NotEmpty()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} requerido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O campo de preço é obrigatório."
                });
        }
    }
}
