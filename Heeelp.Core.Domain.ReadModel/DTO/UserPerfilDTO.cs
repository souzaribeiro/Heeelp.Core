﻿using System;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using FluentValidation;
using FluentValidation.Attributes;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    [Validator(typeof(EmployeeDTO))]
    public class UserPerfilDTO
    {
        
        public Guid EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string SecundaryEmail { get; set; }
        public string SmartPhoneNumber { get; set; }
        public string UserProfileName { get; set; }
        public string UserStatusName { get; set; }
        public string UserProfileId { get; set; }
        public string UserStatusId { get; set; }
        //public int UserSystemId { get; set; }

    }

    public class UserPerfilDTOValidator : AbstractValidator<EmployeeDTO>
    {
        public UserPerfilDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(4, 50)
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.ValueOutOfRange,
                    DeveloperMessageTemplate = "{0} precisa do colaborador ter entre 4 e 50 caracteres",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O nome do colaborador precisa ter entre 4 e 50 caracteres."
                });
            RuleFor(x => x.Email).EmailAddress()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidEmail,
                    DeveloperMessageTemplate = "{0} E-mail do colaborador inválido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O email informado para o colaborador não é válido."
                });
            RuleFor(x => x.SmartPhoneNumber).Matches(GeneralRegularExpressions.RegexValidatePhoneNumber)
               .WithState(x => new ErrorState
               {
                   ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidPhoneNumber,
                   DeveloperMessageTemplate = "{0} Telefone  do colaborador inválido",
                   DocumentationPath = "/Usernames",
                   UserMessage = "O telefone informado para o colaborador não é válido."
               });
            RuleFor(x => x.UserProfileId)
                .NotNull()
                .WithState(x => new ErrorState
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.Required,
                    DeveloperMessageTemplate = "{0} Perfil do colaborador não definido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O Perfil do colaborador é obrigatório."
                });
        }

    }

}
