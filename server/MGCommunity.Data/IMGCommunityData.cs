using MGCommunity.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGCommunity.Data
{
	public interface IMGCommunityData
	{
		IRepository<User> Users { get; }

		IRepository<IdentityRole> UserRoles { get; }

		IRepository<Section> Sections { get; }

		IRepository<Category> Categories { get; }

		IRepository<Topic> Topics { get; }

		IRepository<Reply> Replies { get; }

		IRepository<Like> Likes { get; }

		IRepository<ExceptionEntry> ErrorsLog { get; }

		IRepository<UserSession> UserSessions { get; }

		void Dispose();

		int SaveChanges();
	}
}
