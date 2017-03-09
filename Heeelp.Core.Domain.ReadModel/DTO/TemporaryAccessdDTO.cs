using System;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using FluentValidation;
using FluentValidation.Attributes;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
   // [Validator(typeof(UserChangePasswordDTO))]
    public class TemporaryAccessdDTO
    {                                 
                         
        public Guid IntegrationCode { get; set; }
                                                     
    }
                   

}
