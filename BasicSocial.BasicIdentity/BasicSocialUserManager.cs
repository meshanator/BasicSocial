using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace BasicSocial.BasicIdentity
{
	public class BasicSocialUserManager : UserManager<BasicSocialUser, int>
	{
		public BasicSocialUserManager(IUserStore<BasicSocialUser, int> store) : base(store)
		{
			this.UserLockoutEnabledByDefault = false;
		}

		public override Task<IdentityResult> CreateAsync(BasicSocialUser user, string password)
		{
			//Store.CreateAsync(user);
			return base.CreateAsync(user, password);
		}

		public override async Task<bool> CheckPasswordAsync(BasicSocialUser user, string password)
		{
			return await base.CheckPasswordAsync(user, password);
		}
	}
}
