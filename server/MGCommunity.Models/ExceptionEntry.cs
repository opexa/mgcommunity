using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace MGCommunity.Models
{
	public class ExceptionEntry : Exception
	{
		[Key]
		public int Id { get; set; }

		public DateTime OccuredOn { get; set; }

		public IDictionary ExceptionData { get; set; }
	}
}
