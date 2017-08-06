using System.Linq;
using System.Web.Mvc;
using BasicSocial.Dal;
using Microsoft.AspNet.Identity;

namespace BasicSocial.Web.Controllers
{
	public class HomeController : Controller
	{
		public GeneralContext Context { get; set; }

		public HomeController(GeneralContext context)
		{
			Context = context;
		}

		public ActionResult Index()
		{
			if (!Request.IsAuthenticated)
				return RedirectToAction("login", "account");

			var test = User.Identity.GetUserId<int>();
			var user = Context.Users.FirstOrDefault(x => x.Id == test);
			return View();
		}
	}
}