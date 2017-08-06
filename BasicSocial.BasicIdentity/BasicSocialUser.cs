using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using BasicSocial.Core;
using Microsoft.AspNet.Identity;

namespace BasicSocial.BasicIdentity
{
    public class BasicSocialUser : IUser<int>
    {
	    public BasicSocialUser()
	    {
		    
	    }

		public BasicSocialUser(User user)
		{
			Id = user.Id;
			UserName = user.UserName;
			PasswordHash = user.PasswordHash;
			FirstName = user.FirstName;
			LastName = user.LastName;
			Age = user.Age;
		}

		public int Id { get; }
	    public string UserName { get; set; }
	    public string PasswordHash { get; set; }
	    public string FirstName { get; set; }
	    public string LastName { get; set; }
	    public int Age { get; set; }
	}
}
