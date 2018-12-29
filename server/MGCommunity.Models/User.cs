using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MGCommunity.Models
{
	public class User : IdentityUser
	{
		public User()
		{
			this.Replies = new HashSet<Reply>();
			this.Likes = new HashSet<Reply>();
			this.Topis = new HashSet<Topic>();
		}
		
		public string FirstName { get; set; }

		public string LastName { get; set; }
				
		public short StartYear { get; set; }

		public string Avatar { get; set; }

		public UserClass Class { get; set; }

		public string Status { get; set; }

		public virtual ICollection<Reply> Replies { get; set; }

		public virtual ICollection<Reply> Likes { get; set; }

		public virtual ICollection<Topic> Topis { get; set; }

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
						UserManager<User> manager, string authenticationType)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

			// Add custom user claims here
			return userIdentity;
		}
	}
}
