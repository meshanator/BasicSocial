using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BasicSocial.Web.Startup))]
namespace BasicSocial.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
