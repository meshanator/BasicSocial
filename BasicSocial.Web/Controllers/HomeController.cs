using System.Linq;
using System.Web.Mvc;
using BasicSocial.Core;
using BasicSocial.Dal;
using BasicSocial.Web.Models;
using Microsoft.AspNet.Identity;

namespace BasicSocial.Web.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		public GeneralContext Context { get; set; }

		public HomeController(GeneralContext context)
		{
			Context = context;
		}

		public ActionResult Index()
		{
			var userId = User.Identity.GetUserId<int>();
			var user = Context.Users.FirstOrDefault(x => x.Id == userId);
			return View(user);
		}

		public ActionResult Landing()
		{
			return View();
		}

		public ActionResult Login()
		{
			return View();
		}

		public ActionResult Registration()
		{
			return View();
		}

		public ActionResult Messages()
		{
			var userId = User.Identity.GetUserId<int>();
			var user = Context.Users.FirstOrDefault(x => x.Id == userId);
			return View(user);
		}

		public ActionResult ComposeMessage()
		{
			var userId = User.Identity.GetUserId<int>();
			var user = Context.Users.FirstOrDefault(x => x.Id == userId);
			return View(user);
		}

		[HttpPost]
		public ActionResult ComposeMessage(ComposeMessageModel model)
		{
			var userId = User.Identity.GetUserId<int>();
			var user = Context.Users.FirstOrDefault(x => x.Id == userId);
			var user2 = Context.Users.FirstOrDefault(x => x.Id == model.ToUserId);

			var privateMessage = new PrivateMessage()
			{
				Subject = model.Subject,
				Content = model.Content,
				Sender = user,
				Receiver = user2
			};

			Context.PrivateMessages.Add(privateMessage);
			Context.SaveChanges();

			return View(user);
		}
	}
}