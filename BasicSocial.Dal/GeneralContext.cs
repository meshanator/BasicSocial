using System.Data.Entity;
using BasicSocial.Core;

namespace BasicSocial.Dal
{
    public class GeneralContext : DbContext
    {
	    public GeneralContext() : base(
		    "DefaultConnection")
		{
		}

	    public DbSet<User> Users { get; set; }
		public DbSet<ImagePost> ImagePosts { get; set; } 
		public DbSet<TextPost> TextPosts { get; set; }
		public DbSet<PrivateMessage> PrivateMessages { get; set; }

	    protected override void OnModelCreating(DbModelBuilder modelBuilder)
	    {
			//setting up relationships
			//user
		    modelBuilder.Entity<User>()
			    .HasMany(s => s.Friends)
				.WithMany()
			    .Map(cs =>
			    {
				    cs.MapLeftKey("Id");
				    cs.MapRightKey("FriendId");
				    cs.ToTable("UserFriend");
			    });

		    modelBuilder.Entity<User>()
			    .HasMany(s => s.SentPosts)
			    .WithOptional()
			    .WillCascadeOnDelete(false);

		    modelBuilder.Entity<User>()
			    .HasMany(s => s.ReceivedPosts)
			    .WithOptional()
			    .WillCascadeOnDelete(false);

		    modelBuilder.Entity<User>()
			    .HasMany(s => s.PrivateMessagesSent)
			    .WithOptional()
			    .WillCascadeOnDelete(false);

		    modelBuilder.Entity<User>()
			    .HasMany(s => s.PrivateMessagesReceived)
			    .WithOptional()
			    .WillCascadeOnDelete(false);

			//post
			modelBuilder.Entity<Post>()
			    .HasOptional(s => s.Receiver)
			    .WithMany(x => x.ReceivedPosts)
			    .HasForeignKey(x => x.ReceiverId)
				.WillCascadeOnDelete(false);

		    modelBuilder.Entity<Post>()
			    .HasOptional(s => s.Sender)
			    .WithMany(x => x.SentPosts)
			    .HasForeignKey(x => x.SenderId)
			    .WillCascadeOnDelete(false);

			//private message
		    modelBuilder.Entity<PrivateMessage>()
			    .HasOptional(s => s.Sender)
			    .WithMany(x => x.PrivateMessagesSent)
			    .HasForeignKey(x => x.SenderId)
			    .WillCascadeOnDelete(false);

		    modelBuilder.Entity<PrivateMessage>()
			    .HasOptional(s => s.Receiver)
			    .WithMany(x => x.PrivateMessagesReceived)
			    .HasForeignKey(x => x.ReceiverId)
			    .WillCascadeOnDelete(false);
		}
    }
}
