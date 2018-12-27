using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MGCommunity.Services.Models.ViewModels
{
	public class ShortCategoryViewModel
	{
		public int Id { get; set; }
		
		public string Name { get; set; }

		public string Description { get; set; }

		public int TopicsCount { get; set; }
	}
}