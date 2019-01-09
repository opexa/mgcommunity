namespace MGCommunity.Services.Models.ViewModels
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;

	public class HomeSectionViewModel
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public IEnumerable<ShortCategoryViewModel> Categories { get; set; }

		public bool Visible { get; set; }
	}
}