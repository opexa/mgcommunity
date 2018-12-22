using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MGCommunity.Models
{
	public class Topic
	{
		public Topic()
		{
			this.Replies = new HashSet<Reply>();
		}

		[Key]
		public int Id { get; set; }

		[MaxLength(100)]
		public string Title { get; set; }

		public string AuthorId { get; set; }

		public virtual User Author { get; set; }

		public int CategoryId { get; set; }

		public virtual Category Category { get; set; }

		public DateTime CreatedOn { get; set; }

		public bool Pinned { get; set; }

		public bool Locked { get; set; }

		public int RepliesCount { get; set; }

		public virtual ICollection<Reply> Replies { get; set; }
	}
}
