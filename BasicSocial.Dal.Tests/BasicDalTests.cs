using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BasicSocial.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BasicSocial.Dal.Tests
{
	[TestClass]
	public class BasicDalTests
	{
		[TestMethod]
		public void TestSeed1()
		{
			using (var temp = new GeneralContext())
			{
				var user = new User()
				{
					Age = 28,
					FirstName = "Harry",
					LastName = "Person",
					Friends = new List<User>(),
					SentPosts = new List<Post>(),
					ReceivedPosts = new List<Post>(),
					PrivateMessagesReceived = new List<PrivateMessage>(),
					PrivateMessagesSent = new List<PrivateMessage>()
				};

				var user2 = new User()
				{
					Age = 28,
					FirstName = "Steven",
					LastName = "Lister",
					Friends = new List<User>()
					{
						user
					},
					SentPosts = new List<Post>(),
					ReceivedPosts = new List<Post>(),
					PrivateMessagesReceived = new List<PrivateMessage>(),
					PrivateMessagesSent = new List<PrivateMessage>()
				};

				var user3 = new User()
				{
					Age = 28,
					FirstName = "Jane",
					LastName = "Bottle",
					Friends = new List<User>()
					{
						user,
						user2
					},
					SentPosts = new List<Post>(),
					ReceivedPosts = new List<Post>(),
					PrivateMessagesReceived = new List<PrivateMessage>(),
					PrivateMessagesSent = new List<PrivateMessage>()
				};

				temp.Users.Add(user);
				temp.Users.Add(user2);
				temp.Users.Add(user3);
				temp.SaveChanges();
			}
		}

		[TestMethod]
		public void TestUserFriends1()
		{
			using (var context = new GeneralContext())
			{
				var user1 = context.Users.FirstOrDefault(x => x.Id == 4);
				var test = user1.Friends;

				foreach (var user in test)
				{
					Trace.WriteLine(user.UserName);
				}
			}
		}

		[TestMethod]
		public void TestPosts1()
		{
			using (var context = new GeneralContext())
			{
				var user1 = context.Users.FirstOrDefault(x => x.Id == 3);
				var user2 = context.Users.FirstOrDefault(x => x.Id == 4);

				var p = new ImagePost()
				{
					Name = "Name 1",
					Receiver = user2,
					Sender = user1,
					Subject = "Saying Hi",
					Url = "www.google.com/image1"
				};

				context.ImagePosts.Add(p);
				context.SaveChanges();
			}
		}

		[TestMethod]
		public void TestPosts2()
		{
			using (var context = new GeneralContext())
			{
				var user1 = context.Users.FirstOrDefault(x => x.Id == 1);
				var user2 = context.Users.FirstOrDefault(x => x.Id == 2);

				var p = new TextPost()
				{
					Name = "Name 1",
					Receiver = user1,
					Sender = user2,
					Subject = "Saying Hi 1",
					Content = "Content 1"
				};

				context.TextPosts.Add(p);
				context.SaveChanges();
			}
		}

		[TestMethod]
		public void TestUserPosts1()
		{
			using (var context = new GeneralContext())
			{
				var user1 = context.Users.FirstOrDefault(x => x.Id == 4);
				var test = user1.ReceivedPosts;
			}
		}

		[TestMethod]
		public void TestGetPosts1()
		{
			using (var context = new GeneralContext())
			{
				var user1 = context.ImagePosts.FirstOrDefault(x => x.Id == 2);
			}
		}

		[TestMethod]
		public void TestUserCreateMessage1()
		{
			using (var context = new GeneralContext())
			{
				var user1 = context.Users.FirstOrDefault(x => x.Id == 1);
				var user2 = context.Users.FirstOrDefault(x => x.Id == 2);

				var message = new PrivateMessage()
				{
					Content = "Content 1",
					Sender = user1,
					Receiver = user2
				};

				context.PrivateMessages.Add(message);
				context.SaveChanges();
			}
		}

		[TestMethod]
		public void TestUserMessages1()
		{
			using (var context = new GeneralContext())
			{
				var user1 = context.Users.FirstOrDefault(x => x.Id == 1);
				var test = user1.PrivateMessagesSent;
			}
		}
	}
}
