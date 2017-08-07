using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicSocial.Web.Models
{
	public class ComposeMessageModel
	{
		public string Subject { get; set; }
		public string Content { get; set; }
		public int ToUserId { get; set; }
	}
}