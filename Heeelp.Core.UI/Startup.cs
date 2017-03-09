using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Heeelp.Core.UI.Startup))]
namespace Heeelp.Core.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
