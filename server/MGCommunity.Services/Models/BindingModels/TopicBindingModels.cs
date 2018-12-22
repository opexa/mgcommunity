namespace MGCommunity.Services.Models.BindingModels
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.Web;
	using System.Web.Http;
	using UserSessionUtils;

	public class CreateTopicBindingModel
	{
		[Required]
		public string Title { get; set; }

		[Required]
		public int CategoryId { get; set; }

		[Required]
		public string FirstReply { get; set; }
	}
}