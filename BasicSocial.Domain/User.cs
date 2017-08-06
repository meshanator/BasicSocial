using System.Collections.Generic;

namespace BasicSocial.Domain
{
    public class User
    {
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Age { get; set; }
		public IList<User> Friends { get; set; }
		public IList<Post> Posts { get; set; }
		public IList<PrivateMessage> PrivateMessagesReceived { get; set; }
	    public IList<PrivateMessage> PrivateMessagesSent { get; set; }
	}
}
