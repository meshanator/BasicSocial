using System.Linq;
using System.Web.Mvc;
using BasicSocial.Dal;
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
	}
}