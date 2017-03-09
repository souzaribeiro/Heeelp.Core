
using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebGrease.Css.Extensions;
using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;

namespace Heeelp.Core.WebAPI.Validation
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            var parameters = actionContext.ActionArguments.Select(x => x.Value).Where(x => x != null);

            parameters.ForEach(parameter =>
            {
                var argumentType = parameter.GetType();
                var validator = FindValidator(argumentType);
                if (validator != null)
                {
                    var validationResult = validator.Validate(parameter);
                    if (!validationResult.IsValid)
                    {
                        ThrowFormattedApiResponse(validationResult);
                    }
                }
            });
        }

        private IValidator FindValidator(Type type)
        {
            // Can be replaced with custom IOC logic
            if (type == typeof (NewCompanyDTO))
            {
                return new NewCompanyDTOValidator();
            }
            if (type == typeof(NewEmployeeDTO))
            {
                return new NewEmployeeDTOValidator();
            }
            if (type == typeof(CompanyInformationDTO))
            {
                return new CompanyInformationDTOValidator();
            }
            if (type == typeof(CompanyExpertiseDTO))
            {
                return new CompanyExpertiseDTOValidator();
            }
            if (type == typeof(EmployeeDTO))
            {
                return new EmployeeDTOValidator();
            }
            if (type == typeof(CompanyDTO))
            {
                return new CompanyDTOValidator();
            }
            if (type == typeof(EmployeeActivationDTO))
            {
                return new EmployeeActivationDTOValidator();
            }
            if (type == typeof(PasswordDTO))
            {
                return new PasswordDTOValidator();
            }

            if (type == typeof(PromotionDiscountInputDTO))
            {
                return new PromotionDiscountDtoValidator();
            }
            if (type == typeof(PromotionAwardInputDTO))
            {
                return new PromotionAwardInputDTOValidator();
            }



            return null;
        }

        private void ThrowFormattedApiResponse(ValidationResult validationResult)
        {
            var errorsModel = new ErrorsModel();

            var formattedErrors = validationResult.Errors.Select(x =>
            {
                var errorModel = new ErrorModel();
                var errorState = x.CustomState as ErrorState;
                if (errorState != null)
                {
                    errorModel.ErrorCode = errorState.ErrorCode;
                    errorModel.Field = x.PropertyName;
                    errorModel.Documentation = "https://developer.example.com/docs" + errorState.DocumentationPath;
                    errorModel.DeveloperMessage = string.Format(errorState.DeveloperMessageTemplate, x.PropertyName);

                    // Can be replaced by translating a localization key instead
                    // of just mapping over a hardcoded message
                    errorModel.UserMessage = errorState.UserMessage;
                }
                return errorModel;
            });
            errorsModel.Errors = formattedErrors;

            var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(JsonConvert.SerializeObject(errorsModel, Formatting.Indented))
            };
            throw new HttpResponseException(responseMessage);
        }


    }
}