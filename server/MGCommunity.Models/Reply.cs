namespace MGCommunity.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public class Reply
	{
		public Reply()
		{
			this.Likes = new HashSet<Like>();
		}

		[Key]
		public int Id { get; set; }

		public int TopicId { get; set; }

		public virtual Topic Topic { get; set; }

		public string AuthorId { get; set; }

		public virtual User Author { get; set; }

		public DateTime PostedOn { get; set; }

		public string Content { get; set; }

		public virtual ICollection<Like> Likes{ get; set; }
	}
}
