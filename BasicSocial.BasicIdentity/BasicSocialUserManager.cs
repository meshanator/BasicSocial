using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace BasicSocial.BasicIdentity
{
	public class BasicSocialUserManager : UserManager<BasicSocialUser, int>
	{
		public BasicSocialUserManager(IUserStore<BasicSocialUser, int> store) : base(store)
		{
		}

		public override Task<IdentityResult> CreateAsync(BasicSocialUser user, string password)
		{
			//Store.CreateAsync(user);
			return base.CreateAsync(user, password);
		}
	}
}
