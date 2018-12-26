using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MGCommunity.Models
{
	public class Category
	{
		public Category()
		{
			this.Topics = new HashSet<Topic>();
		}

		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public int SectionId { get; set; }

		public int TopicsCount { get; set; }

		public virtual Section Section { get; set; }

		public virtual ICollection<Topic> Topics { get; set; }
	}
}
