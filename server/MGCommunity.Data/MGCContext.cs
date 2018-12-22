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
		}		

		public static MGCContext Create()
		{
			return new MGCContext();
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