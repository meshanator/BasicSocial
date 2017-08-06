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
			var test = User.Identity.GetUserId<int>();
			var user = Context.Users.FirstOrDefault(x => x.Id == test);
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}