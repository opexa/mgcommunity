namespace MGCommunity.Data
{
	using Microsoft.AspNet.Identity.EntityFramework;
	using Models;
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Threading.Tasks;
	public class MGCommunityData : IMGCommunityData
	{
		private readonly DbContext context;

		private readonly IDictionary<Type, object> repositories;

		public MGCommunityData() : this(new MGCContext()) { }

		public MGCommunityData(MGCContext context)
		{
			this.context = context;
			this.repositories = new Dictionary<Type, object>();
		}

		public IRepository<User> Users
		{
			get
			{
				return this.GetRepository<User>();
			}
		}

		public IRepository<IdentityRole> UserRoles
		{
			get
			{
				return this.GetRepository<IdentityRole>();
			}
		}

		public IRepository<Section> Sections
		{
			get
			{
				return this.GetRepository<Section>();
			}
		}

		public IRepository<Category> Categories
		{
			get
			{
				return this.GetRepository<Category>();
			}
		}

		public IRepository<Topic> Topics
		{
			get
			{
				return this.GetRepository<Topic>();
			}
		}

		public IRepository<Reply> Replies
		{
			get
			{
				return this.GetRepository<Reply>();
			}
		}

		public IRepository<Like> Likes
		{
			get
			{
				return this.GetRepository<Like>();
			}
		}

		public IRepository<ExceptionEntry> ErrorsLog
		{
			get
			{
				return this.GetRepository<ExceptionEntry>();
			}
		}

		public IRepository<UserSession> UserSessions
		{
			get
			{
				return this.GetRepository<UserSession>();
			}
		}

		public int SaveChanges()
		{
			return this.context.SaveChanges();
		}

		private IRepository<T> GetRepository<T>() where T : class
		{
			if (!this.repositories.ContainsKey(typeof(T)))
			{
				var type = typeof(EfRepository<T>);
				this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
			}

			return (IRepository<T>)this.repositories[typeof(T)];
		}
	}
}
