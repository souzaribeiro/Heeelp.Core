using Heeelp.Core.Common;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using static Heeelp.Core.Common.GeneralEnumerators;
using Microsoft.Owin;

namespace Heeelp.Core.WebAPI.Provider
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private HttpResponseMessage response;
        private IFormCollection data;

        public override Task MatchEndpoint(OAuthMatchEndpointContext context)
        {
            if (context.IsTokenEndpoint && context.Request.Method == "OPTIONS")
            {
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "authorization" });
                context.RequestCompleted();
                return Task.FromResult(0);
            }

            return base.MatchEndpoint(context);
        }
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext c)
        {
            c.Validated();

            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext c)
        {
            string allowedOrigin = "*";

            // Aqui você deve implementar sua regra de autenticação

            var _client = new HttpClient();
            _client.BaseAddress = new Uri(CustomConfiguration.WebApiCore);
            dynamic user = new JObject();
            user.Email = c.UserName;
            user.Password = c.Password;


            switch (((Microsoft.Owin.OwinRequest)c.Request).Path.ToString())
            {
                case "/TokenInternal":
                    response = _client.PostAsJsonAsync("/api/Authentication/ValidateUser", new { user }).Result;
                    break;
                case "/ActiveNewUser":
                    data = await c.Request.ReadFormAsync();
                    user = new JObject();
                    user.IntegrationCode = data["IntegrationCode"];
                    response = _client.PostAsJsonAsync("/api/Authentication/AuthFistAccess", new { IntegrationCode = user.IntegrationCode }).Result;
                    break;
                case "/Token":
                    data = await c.Request.ReadFormAsync();
                    user.Hash = data["Hash"];
                    user.IntegrationCode = data["IntegrationCode"];
                    response = _client.PostAsJsonAsync("/api/Authentication/ValidateUserHash", new { user }).Result;
                    break;
                default:
                    break;
            }
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var authenticationResponse = response.Content.ReadAsAsync<dynamic>().Result;


                int userProfileId = Convert.ToInt32(authenticationResponse.UserProfileId.ToString());

                var identity = new ClaimsIdentity("HeeelpCore");

                string ProfileList = string.Empty;
                var list = JArray.Parse(authenticationResponse.ProfileClaims.ToString());
                for (int i = 0; i < list.Count; i++)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, ((EnumProfileClaims)list[i]).ToString()));
                    ProfileList += list[i];
                    if ((i + 1) < list.Count)
                        ProfileList += ",";
                }
                var UserId = authenticationResponse.UserId.ToString();
                var PersonId = authenticationResponse.PersonId.ToString();
                var PersonIntegrationCode = authenticationResponse.PersonIntegrationCode.ToString();
                var UserIntegrationCode = authenticationResponse.UserIntegrationCode.ToString();
                var Complete = authenticationResponse.Complete.ToString();

                identity.AddClaim(new Claim(ClaimTypes.Name, UserId));
                identity.AddClaim(new Claim("Role", ProfileList));
                identity.AddClaim(new Claim("PersonId", PersonId));
                identity.AddClaim(new Claim("PersonIntegrationCode", PersonIntegrationCode));
                identity.AddClaim(new Claim("UserIntegrationCode", UserIntegrationCode));
                identity.AddClaim(new Claim("Complete", Complete));

                AuthenticationProperties properties = CreateProperties(
                    UserId,
                    ProfileList,
                    PersonId,
                    PersonIntegrationCode,
                    UserIntegrationCode,
                    Complete);

                var ticket = new AuthenticationTicket(identity, properties);

                c.Validated(ticket);

            }

            //return Task.FromResult<object>(null);
        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }


            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userId, string role, string personId, string personIntegrationCode, string userIntegrationCode, string complete)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "UserId", userId },
                { "Role", role },
                { "PersonId", personId }  ,
                { "PersonIntegrationCode", personIntegrationCode },
                { "UserIntegrationCode", userIntegrationCode },
                {"Complete",complete }
            };
            return new AuthenticationProperties(data);
        }
    }
}