namespace BasicSocial.Domain
{
	public class PrivateMessage
	{
		public int Id { get; set; }
		public string Subject { get; set; }
		public string Content { get; set; }
		public bool IsRead { get; set; }
		public PrivateMessage LinkedPrivateMessage { get; set; }
		public User Sendee { get; set; }
		public User Sender { get; set; }
	}
}