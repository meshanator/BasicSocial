using System.ComponentModel.DataAnnotations.Schema;

namespace BasicSocial.Core
{
	[Table("PrivateMessage")]
	public class PrivateMessage : Entity
	{
		public string Subject { get; set; }
		public string Content { get; set; }
		public bool IsRead { get; set; }
		public PrivateMessage LinkedPrivateMessage { get; set; }
		public int? ReceiverId { get; set; }
		public User Receiver { get; set; }
		public int? SenderId { get; set; }
		public User Sender { get; set; }
	}
}