﻿using System.ComponentModel.DataAnnotations;

namespace BasicSocial.Web.Models
{
	public class FeedPostModel
	{
		[Required]
		public string Subject { get; set; }
		public string Content { get; set; }
		public int FromUserId { get; set; }
		public int ToUserId { get; set; }
		public bool ImagePost { get; set; }
		public string Url { get; set; }
	}
}