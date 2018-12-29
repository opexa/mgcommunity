using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MGCommunity.Services.Models.ViewModels
{
	public class ReplyViewModel
	{
		public int Id { get; set; }

		public string AuthorUsername { get; set; }

		public string AuthorAvatar { get; set; }

		public string AuthorStatus { get; set; }

		public string DetailedDate { get; set; }

		public string RelativeDate { get; set; }

		public string Content { get; set; }

		public int LikesCount { get; set; }

		public IEnumerable<LikeViewModel> LastThreeLikes { get; set; }
	}
}