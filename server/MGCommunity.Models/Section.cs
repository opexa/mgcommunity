using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MGCommunity.Models
{
	public class Section
	{
		public Section()
		{
			this.Categories = new HashSet<Category>();
		}

		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public bool Visible { get; set; }

		public virtual ICollection<Category> Categories { get; set; }
	}
}
