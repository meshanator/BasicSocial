namespace BasicSocial.Web.Models
{
	public class ComposeMessageViewModel
	{
		public string Subject { get; set; }
		public string Content { get; set; }
		public int ToUserId { get; set; }
	}
}