using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BasicSocial.Core;

namespace BasicSocial.Web.Models
{
	public class ConversationModel
	{
		public User Me { get; set; }
		public User They { get; set; }
	}
}