using System.Web.Http;
using WebActivatorEx;
using Heeelp.Social.WebAPI;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Heeelp.Social.WebAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c => {

                    c.SingleApiVersion("v1", "Heeelp.Core.WebAPI");
                    c.IncludeXmlComments(string.Format(@"{0}\bin\Heeelp.Core.WebAPI.XML",
                           System.AppDomain.CurrentDomain.BaseDirectory));
                    c.UseFullTypeNameInSchemaIds();
                })
                .EnableSwaggerUi();
        }
    }
}
