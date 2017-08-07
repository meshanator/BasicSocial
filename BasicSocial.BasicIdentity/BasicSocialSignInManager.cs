using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace BasicSocial.BasicIdentity
{
	public class BasicSocialSignInManager : SignInManager<BasicSocialUser, int>
	{
		public BasicSocialSignInManager(UserManager<BasicSocialUser, int> userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
		{
		}

		public override Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
		{
			var passwordSignInAsync = base.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);

			if (passwordSignInAsync.Result == SignInStatus.Success)
				return passwordSignInAsync;

			return passwordSignInAsync;
		}
	}
}
