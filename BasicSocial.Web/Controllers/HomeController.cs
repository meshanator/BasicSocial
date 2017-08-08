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
			var loggedInUserId = User.Identity.GetUserId<int>();
			var loggedInUser = Context.Users.FirstOrDefault(x => x.Id == loggedInUserId);
			return View(loggedInUser);
		}

		public ActionResult FindFriends()
		{
			var loggedInUserId = User.Identity.GetUserId<int>();
			var loggedInUser = Context.Users.FirstOrDefault(x => x.Id == loggedInUserId);
			var model = new FindFriendModel
			{
				Me = loggedInUser,
				Users = Context.Users
			};
			return View(model);
		}

		public ActionResult AddFriend(int theirUserId)
		{
			var loggedInUserId = User.Identity.GetUserId<int>();
			var loggedInUser = Context.Users.FirstOrDefault(x => x.Id == loggedInUserId);
			var theirUser = Context.Users.FirstOrDefault(x => x.Id == theirUserId);

			if(!loggedInUser.Friends.Contains(theirUser))
				loggedInUser.Friends.Add(theirUser);
			Context.SaveChanges();
			return RedirectToAction("FindFriends");
		}

		[HttpPost]
		public ActionResult PostToMyFeed(FeedPostModel postModel)
		{
			var loggedInUserId = User.Identity.GetUserId<int>();
			var loggedInUser = Context.Users.FirstOrDefault(x => x.Id == loggedInUserId);

			if (postModel.ImagePost)
			{
				var post = new ImagePost
				{
					Subject = postModel.Subject,
					Url = postModel.Url,
					Sender = loggedInUser,
					Receiver = loggedInUser
				};

				Context.ImagePosts.Add(post);
				Context.SaveChanges();
			}
			else
			{
				var post = new TextPost
				{
					Subject = postModel.Subject,
					Content = postModel.Content,
					Sender = loggedInUser,
					Receiver = loggedInUser
				};

				Context.TextPosts.Add(post);
				Context.SaveChanges();
			}

			return RedirectToAction("Index");
		}

		[HttpPost]
		public ActionResult PostToFriendFeed(FeedPostModel postModel)
		{
			var loggedInUserId = User.Identity.GetUserId<int>();
			var loggedInUser = Context.Users.FirstOrDefault(x => x.Id == loggedInUserId);
			var theirUser = Context.Users.FirstOrDefault(x => x.Id == postModel.ToUserId);

			if (postModel.ImagePost)
			{
				var post = new ImagePost
				{
					Subject = postModel.Subject,
					Url = postModel.Url,
					Sender = loggedInUser,
					Receiver = theirUser
				};

				Context.ImagePosts.Add(post);
				Context.SaveChanges();
			}
			else
			{
				var post = new TextPost
				{
					Subject = postModel.Subject,
					Content = postModel.Content,
					Sender = loggedInUser,
					Receiver = theirUser
				};

				Context.TextPosts.Add(post);
				Context.SaveChanges();
			}

			return RedirectToAction("FriendFeed", new { theirUserId = theirUser.Id });
		}

		public ActionResult FriendFeed(int theirUserId)
		{
			var loggedInUserId = User.Identity.GetUserId<int>();
			var loggedInUser = Context.Users.FirstOrDefault(x => x.Id == loggedInUserId);
			var theirUser = Context.Users.FirstOrDefault(x => x.Id == theirUserId);
			var model = new ConversationViewModel { Me = loggedInUser, They = theirUser };
			return View(model);
		}

		[HttpPost]
		public ActionResult ComposeMessage(ComposeMessageViewModel viewModel)
		{
			var loggedInUserId = User.Identity.GetUserId<int>();
			var loggedInUser = Context.Users.FirstOrDefault(x => x.Id == loggedInUserId);
			var theirUser = Context.Users.FirstOrDefault(x => x.Id == viewModel.ToUserId);

			var privateMessage = new PrivateMessage
			{
				Subject = viewModel.Subject,
				Content = viewModel.Content,
				Sender = loggedInUser,
				Receiver = theirUser
			};

			Context.PrivateMessages.Add(privateMessage);
			Context.SaveChanges();
			return RedirectToAction("Conversation", new {theirUserId = theirUser.Id});
		}

		public ActionResult Conversation(int theirUserId)
		{
			var loggedInUserId = User.Identity.GetUserId<int>();
			var loggedInUser = Context.Users.FirstOrDefault(x => x.Id == loggedInUserId);
			var theirUser = Context.Users.FirstOrDefault(x => x.Id == theirUserId);
			var model = new ConversationViewModel {Me = loggedInUser, They = theirUser};
			return View(model);
		}
	}
}