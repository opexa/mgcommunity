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

		public int ParticipantsCount { get; set; }
	
		public string AuthorUsername { get; set; }

		public string AuthorAvatar { get; set; }

		public string CreatedOn { get; set; }

		public string LastReply { get; set; }

		public string LastReplyBy { get; set; }
	}

	public class TopicInfoViewModel
	{
		public string Title { get; set; }

		public string AuthorUsername { get; set; }

		public string CreatedOn { get; set; }

		public string MaxPage { get; set; }
	}
}