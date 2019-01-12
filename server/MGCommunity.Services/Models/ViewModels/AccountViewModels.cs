using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MGCommunity.Services.Models.ViewModels
{
	public class AccountDetailsViewModel
	{
		public string Id { get; set; }

		public string UserName { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public short StartYear { get; set; }

		public string Avatar { get; set; }

		public string Status { get; set; }
	}
}