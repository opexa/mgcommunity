namespace MGCommunity.Services.Models.BindingModels
{
	using System.ComponentModel.DataAnnotations;

	public class AddReplyBindingModel
	{
		[Required]
		public string Content { get; set; }

		[Required]
		public int TopicId { get; set; }
	}

	public class EditReplyBindingModel
	{
		[Required]
		public string Content { get; set; }

		[Required]
		public int ReplyId { get; set; }
	}
}