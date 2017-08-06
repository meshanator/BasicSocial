using System;
using System.Collections.Generic;
using System.Linq;
using BasicSocial.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BasicSocial.Dal.Tests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			using (var temp = new GeneralContext())
			{
				var user = new User()
				{
					Age = 28,
					FirstName = "Meshan",
					LastName = "Naidoo",
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
					LastName = "Wagner",
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
					FirstName = "James",
					LastName = "Turner",
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
		public void TestUser3Friends()
		{
			using (var context = new GeneralContext())
			{
				var user1 = context.Users.FirstOrDefault(x => x.Id == 3);
				var test = user1.Friends;

			}
		}

		[TestMethod]
		public void TestPosts()
		{
			using (var context = new GeneralContext())
			{
				var user1 = context.Users.FirstOrDefault(x => x.Id == 1);
				var user2 = context.Users.FirstOrDefault(x => x.Id == 2);

				var p = new ImagePost()
				{
					Name = "Today1",
					Receiver = user1,
					Sender = user2,
					Subject = "Saying sup",
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
					Name = "Today1",
					Receiver = user1,
					Sender = user2,
					Subject = "Saying sup",
					Content = "post bro"
				};

				context.TextPosts.Add(p);
				context.SaveChanges();
			}
		}

		[TestMethod]
		public void TestUserPosts()
		{
			using (var context = new GeneralContext())
			{
				var user1 = context.Users.FirstOrDefault(x => x.Id == 1);
				var test = user1.ReceivedPosts;

			}
		}

		[TestMethod]
		public void TestUserCreateMessage()
		{
			using (var context = new GeneralContext())
			{
				var user1 = context.Users.FirstOrDefault(x => x.Id == 1);
				var user2 = context.Users.FirstOrDefault(x => x.Id == 2);

				var message = new PrivateMessage()
				{
					Content = "hello brother",
					Sender = user1,
					Receiver = user2
				};

				context.PrivateMessages.Add(message);
				context.SaveChanges();
			}
		}

		[TestMethod]
		public void TestUser1Messages()
		{
			using (var context = new GeneralContext())
			{
				var user1 = context.Users.FirstOrDefault(x => x.Id == 1);
				var test = user1.PrivateMessagesSent;

			}
		}


	}
}
