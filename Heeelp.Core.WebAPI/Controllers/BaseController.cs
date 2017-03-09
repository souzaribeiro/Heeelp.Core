using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Heeelp.Core.WebAPI.Validation;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using System.Collections.Generic;
using System.Web.Http.Description;

namespace Heeelp.Core.WebAPI.Controllers
{
    public class BaseController : ApiController
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public void ThrowFormattedApiResponse(ErrorState errorState, string fieldName)
        {
            ErrorModel errorModel = new ErrorModel();
            errorModel.ErrorCode = errorState.ErrorCode;
            errorModel.Field = fieldName;
            errorModel.Documentation = "https://developer.example.com/docs" + errorState.DocumentationPath;
            errorModel.DeveloperMessage = string.Format(errorState.DeveloperMessageTemplate, fieldName);
            errorModel.UserMessage = errorState.UserMessage;

            var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(JsonConvert.SerializeObject(errorModel, Formatting.Indented))
            };
            throw new HttpResponseException(responseMessage);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public void LogoutUser()
        {
            HttpContext.Current.GetOwinContext().Authentication.SignOut();
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public class Claims
        {
            public int personId;
            public int userSystemId;
            public Guid personIntegrationCode;
            public Guid userIntegrationCode;
            public List<GeneralEnumerators.EnumProfileClaims> Roles;

            public Claims Values()
            {
                Roles = new List<GeneralEnumerators.EnumProfileClaims>();
                ClaimsPrincipal principal = HttpContext.Current.User as ClaimsPrincipal;
                if (null != principal)
                {
                    foreach (Claim claim in principal.Claims)
                    {
                        this.userSystemId = Convert.ToInt32(claim.Subject.Name);
                        switch (claim.Type)
                        {
                            case "Name":
                                this.userSystemId = Convert.ToInt32(claim.Value);
                                break;
                            case "PersonId":
                                this.personId = Convert.ToInt32(claim.Value);
                                break;
                            case "PersonIntegrationCode":
                                if (!string.IsNullOrEmpty(claim.Value))
                                    this.personIntegrationCode = Guid.Parse(claim.Value);
                                break;
                            case "UserIntegrationCode":
                                if (!string.IsNullOrEmpty(claim.Value))
                                    this.userIntegrationCode = Guid.Parse(claim.Value);
                                break;
                            case "Role":
                                if (!string.IsNullOrEmpty(claim.Value))
                                {
                                    var value = claim.Value.Split(new char[] { ',' });
                                    foreach (var item in value)
                                    {
                                        this.Roles.Add((GeneralEnumerators.EnumProfileClaims)int.Parse(item));
                                    }
                                }
                                break;
                        }

                    }
                }
                return this;


            }
        }


    }
}