using System.ComponentModel.DataAnnotations.Schema;

namespace BasicSocial.Core
{
	[Table("Post")]
	public abstract class Post : Entity
	{
		public string Name { get; set; }
		public string Subject { get; set; }

		public virtual int? ReceiverId { get; set; }
		public virtual User Receiver { get; set; }

		public virtual int? SenderId { get; set; }
		public virtual User Sender { get; set; }
	}
}