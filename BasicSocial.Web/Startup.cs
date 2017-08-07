using System;
using System.Web;
using System.Web.Mvc;
using BasicSocial.BasicIdentity;
using BasicSocial.Dal;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using StructureMap;

[assembly: OwinStartupAttribute(typeof(BasicSocial.Web.Startup))]
namespace BasicSocial.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
	        var container = new Container();
	        container.Configure(config =>
	        {
		        config.For<SignInManager<BasicSocialUser, int>>().Use<BasicSocialSignInManager>();
		        config.For<IUserStore<BasicSocialUser, int>>().Use<BasicSocialUserStore>();
		        config.For<UserManager<BasicSocialUser, int>>().Use<BasicSocialUserManager>();
		        config.For<IControllerFactory>().Use<StructureMapControllerFactory>();
				config.For<IAuthenticationManager>().Use(() => HttpContext.Current.GetOwinContext().Authentication);

		        config.For<IUserTokenProvider<BasicSocialUser, int>>()
			        .Use(() => new DataProtectorTokenProvider<BasicSocialUser, int>(app.GetDataProtectionProvider().Create())
			        {
				        TokenLifespan = TimeSpan.FromHours(8)
			        });
		        config.For<GeneralContext>().Use<GeneralContext>().Singleton();
	        });

	        DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
			
	        app.UseCookieAuthentication(new CookieAuthenticationOptions
	        {
		        AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
		        LoginPath = new PathString("/Account/Login"),
		        Provider = new CookieAuthenticationProvider()
	        });
		}
    }
}
