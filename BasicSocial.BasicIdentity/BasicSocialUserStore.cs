using System;
using System.Linq;
using System.Threading.Tasks;
using BasicSocial.Core;
using BasicSocial.Dal;
using Microsoft.AspNet.Identity;

namespace BasicSocial.BasicIdentity
{
	public class BasicSocialUserStore : IUserPasswordStore<BasicSocialUser, int>, IUserLockoutStore<BasicSocialUser, int>, IUserTwoFactorStore<BasicSocialUser, int>
	{
		public GeneralContext Context { get; set; }

		public BasicSocialUserStore(GeneralContext context)
		{
			Context = context;
		}

		public void Dispose()
		{
			//Context.Dispose();
		}

		public Task CreateAsync(BasicSocialUser user)
		{
			var newUser = new User()
			{
				UserName = user.UserName,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Age = user.Age,
				PasswordHash = user.PasswordHash
			};

			Context.Users.Add(newUser);
			return Context.SaveChangesAsync();
		}

		public Task UpdateAsync(BasicSocialUser user)
		{
			var model = Context.Users.FirstOrDefault(x => x.Id == user.Id);
			model.PasswordHash = user.PasswordHash;
			return Context.SaveChangesAsync();
		}

		public Task DeleteAsync(BasicSocialUser user)
		{
			throw new NotImplementedException();
		}

		public Task<BasicSocialUser> FindByIdAsync(int userId)
		{
			var model = Context.Users.FirstOrDefault(x => x.Id == userId);
			var user = ToBasicSocialUser(model);
			return Task.FromResult(user);
		}

		public Task<BasicSocialUser> FindByNameAsync(string userName)
		{
			var model = Context.Users.FirstOrDefault(x => x.UserName == userName);

			//using (var context = new GeneralContext())
			//{
			//	var user1 = context.Users.FirstOrDefault(x => x.UserName == userName);
			//	var user = ToBasicSocialUser(user1);
			//	return Task.FromResult(user);
			//}
			var user = ToBasicSocialUser(model);
			return Task.FromResult(user);
		}

		public Task SetPasswordHashAsync(BasicSocialUser user, string passwordHash)
		{
			user.PasswordHash = passwordHash;
			return Task.CompletedTask;
		}

		public Task<string> GetPasswordHashAsync(BasicSocialUser user)
		{
			return Task.FromResult(user.PasswordHash);
		}

		public Task<bool> HasPasswordAsync(BasicSocialUser user)
		{
			return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
		}

		public static BasicSocialUser ToBasicSocialUser(User user)
		{
			return user != null ? new BasicSocialUser(user) : null;
		}


		public Task<DateTimeOffset> GetLockoutEndDateAsync(BasicSocialUser user)
		{
			throw new NotImplementedException();
		}

		public Task SetLockoutEndDateAsync(BasicSocialUser user, DateTimeOffset lockoutEnd)
		{
			throw new NotImplementedException();
		}

		public Task<int> IncrementAccessFailedCountAsync(BasicSocialUser user)
		{
			throw new NotImplementedException();
		}

		public Task ResetAccessFailedCountAsync(BasicSocialUser user) => Task.CompletedTask;

		public Task<int> GetAccessFailedCountAsync(BasicSocialUser user) => Task.FromResult(0);

		public Task<bool> GetLockoutEnabledAsync(BasicSocialUser user) => Task.FromResult(false);


		public Task SetLockoutEnabledAsync(BasicSocialUser user, bool enabled) => Task.CompletedTask;
		public Task SetTwoFactorEnabledAsync(BasicSocialUser user, bool enabled)
		{
			throw new NotImplementedException();
		}

		public Task<bool> GetTwoFactorEnabledAsync(BasicSocialUser user)
		{
			return Task.FromResult(false);
		}
	}
}
