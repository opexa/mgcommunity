using System;
using System.ComponentModel.DataAnnotations;

namespace MGCommunity.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public int SectionId { get; set; }

		public virtual Section Section { get; set; }
	}
}
