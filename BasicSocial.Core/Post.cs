using System.ComponentModel.DataAnnotations.Schema;

namespace BasicSocial.Core
{
	[Table("Post")]
	public abstract class Post : Entity
	{
		public string Name { get; set; }
		public string Subject { get; set; }

		public int? ReceiverId { get; set; }
		public User Receiver { get; set; }

		public int? SenderId { get; set; }
		public User Sender { get; set; }
	}
}