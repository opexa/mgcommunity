using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MGCommunity.Services.Models.BindingModels
{
	public class CreateSectionBindingModel
	{
		[Required]
		public string Name { get; set; }
	}
}