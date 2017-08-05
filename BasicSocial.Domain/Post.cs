namespace BasicSocial.Domain
{
	public abstract class Post
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Subject { get; set; }
		public User Sendee { get; set; }
		public User Sender { get; set; }
	}
}