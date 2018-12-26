namespace MGCommunity.Data
{
	using Models;
	using Microsoft.AspNet.Identity.EntityFramework;
	using Migrations;
	using System;
	using System.Data.Entity;
	using System.Linq;

	public class MGCContext : IdentityDbContext<User>
	{
		public MGCContext() : base("MGCContext")
		{
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<MGCContext, Configuration>());

			//this.Configuration.LazyLoadingEnabled = false;
		}		

		public static MGCContext Create()
		{
			return new MGCContext();

		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
								.HasMany<Topic>(u => u.Topis)
								.WithMany(t => t.Participants)
								.Map(cs =>
								{
									cs.MapLeftKey("UserId");
									cs.MapRightKey("TopicId");
									cs.ToTable("UserTopic");
								});

			modelBuilder.Entity<Section>()
									.HasMany<Category>(s => s.Categories);

			base.OnModelCreating(modelBuilder);
		}

		public virtual IDbSet<Section> Sections { get; set; }

		public virtual IDbSet<Category> Categories { get; set; }

		public virtual IDbSet<Topic> Topics { get; set; }

		public virtual IDbSet<Reply> Replies { get; set; }

		public virtual IDbSet<Like> Likes { get; set; }

		public virtual IDbSet<UserSession> UserSessions { get; set; }

		public virtual IDbSet<ExceptionEntry> ErrorsLog { get; set; }
	}
}