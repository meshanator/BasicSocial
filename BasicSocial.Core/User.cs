using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicSocial.Core
{
	[Table("User")]
    public class User : Entity
    {
		public string UserName { get; set; }
		public string PasswordHash { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }
		public virtual IList<User> Friends { get; set; }
		public virtual IList<Post> SentPosts { get; set; }
	    public virtual IList<Post> ReceivedPosts { get; set; }
		public virtual IList<PrivateMessage> PrivateMessagesReceived { get; set; }
	    public virtual IList<PrivateMessage> PrivateMessagesSent { get; set; }
	}
}
