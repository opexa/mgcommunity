using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MGCommunity.Services.Models.BindingModels
{
	public class RegisterBindingModel
	{
		[Required(ErrorMessage = "Моля въведете потребителско име")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Моля въведете собствено име.")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Моля въведете фамилно име.")]
		public string LastName { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required(ErrorMessage = "Въведете първа година в МГ")]
		public short StartYear { get; set; }

		[Required(ErrorMessage = "Моля въведете буква паралелка.")]
		public short Class { get; set; }
	}

	public class LoginUserBindingModel
	{
		[Required]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }
	}

	public class ChangePasswordBindingModel
	{
		[Required]
		[DataType(DataType.Password)]
		public string OldPassword { get; set; }

		[Required]
		//[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Compare("NewPassword", ErrorMessage = "Новата и потвърждаващата пароли не съвпадат.")]
		public string ConfirmPassword { get; set; }
	}
}