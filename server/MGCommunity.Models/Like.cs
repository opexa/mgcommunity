namespace MGCommunity.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class Like
	{
		public int Id { get; set; }

		public string UserId { get; set; }

		public int ReplyId { get; set; }
	}
}
