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

		public ActionResult FindFriend()
		{
			var userId = User.Identity.GetUserId<int>();
			var user = Context.Users.FirstOrDefault(x => x.Id == userId);
			var model = new FindFriendModel
			{
				Me = user,
				Users = Context.Users
			};
			return View(model);
		}

		public ActionResult AddFriend(int theirUserId)
		{
			var userId = User.Identity.GetUserId<int>();
			var user = Context.Users.FirstOrDefault(x => x.Id == userId);
			var user2 = Context.Users.FirstOrDefault(x => x.Id == theirUserId);

			if(!user.Friends.Contains(user2))
				user.Friends.Add(user2);
			Context.SaveChanges();
			return RedirectToAction("FindFriend");
		}

		[HttpPost]
		public ActionResult PostFeed(FeedModel model)
		{
			var userId = User.Identity.GetUserId<int>();
			var user = Context.Users.FirstOrDefault(x => x.Id == userId);
			//var user2 = Context.Users.FirstOrDefault(x => x.Id == model.ToUserId);

			var textPost = new TextPost
			{
				Subject = model.Subject,
				Content = model.Content,
				Sender = user,
				Receiver = user
			};

			Context.TextPosts.Add(textPost);
			Context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpPost]
		public ActionResult PostToTheirFeed(FeedModel model)
		{
			var userId = User.Identity.GetUserId<int>();
			var user = Context.Users.FirstOrDefault(x => x.Id == userId);
			var user2 = Context.Users.FirstOrDefault(x => x.Id == model.ToUserId);

			var textPost = new TextPost
			{
				Subject = model.Subject,
				Content = model.Content,
				Sender = user,
				Receiver = user2
			};

			Context.TextPosts.Add(textPost);
			Context.SaveChanges();
			return RedirectToAction("FriendProfile", new { theirUserId = user2.Id });
		}

		public ActionResult FriendProfile(int theirUserId)
		{
			var userId = User.Identity.GetUserId<int>();
			var user = Context.Users.FirstOrDefault(x => x.Id == userId);
			var user2 = Context.Users.FirstOrDefault(x => x.Id == theirUserId);
			var model = new ConversationModel { Me = user, They = user2 };
			return View(model);
		}

		[HttpPost]
		public ActionResult ComposeMessage(ComposeMessageModel model)
		{
			var userId = User.Identity.GetUserId<int>();
			var user = Context.Users.FirstOrDefault(x => x.Id == userId);
			var user2 = Context.Users.FirstOrDefault(x => x.Id == model.ToUserId);

			var privateMessage = new PrivateMessage
			{
				Subject = model.Subject,
				Content = model.Content,
				Sender = user,
				Receiver = user2
			};

			Context.PrivateMessages.Add(privateMessage);
			Context.SaveChanges();
			return RedirectToAction("UserMessages", new {theirUserId = user2.Id});
		}

		public ActionResult UserMessages(int theirUserId)
		{
			var userId = User.Identity.GetUserId<int>();
			var user = Context.Users.FirstOrDefault(x => x.Id == userId);
			var user2 = Context.Users.FirstOrDefault(x => x.Id == theirUserId);
			var model = new ConversationModel {Me = user, They = user2};
			return View(model);
		}
	}
}