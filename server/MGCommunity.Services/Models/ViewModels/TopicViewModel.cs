using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MGCommunity.Services.Models.ViewModels
{
	public class ShortTopicViewModel
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public int RepliesCount { get; set; } 

		public string AuthorId { get; set; }

		public string AuthorUsername { get; set; }

		public DateTime CreatedOn { get; set; }

		public DateTime LastReply { get; set; }

		public string LastReplyBy { get; set; }
	}
}