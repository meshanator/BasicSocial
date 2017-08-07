using System.Data.Entity;
using BasicSocial.Core;

namespace BasicSocial.Web.Models
{
	public class FindFriendModel
	{
		public User Me { get; set; }
		public DbSet<User> Users { get; set; }
	}
}