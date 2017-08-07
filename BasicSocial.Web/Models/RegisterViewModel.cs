using System.ComponentModel.DataAnnotations;

namespace BasicSocial.Web.Models
{
	public class RegisterViewModel
	{
		[Required]
		[StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
		[Display(Name = "User name")]
		public string UserName { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		[DataType(DataType.Text)]
		[Required]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[DataType(DataType.Text)]
		[Required]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[DataType(DataType.Text)]
		[Range(10, int.MaxValue)]
		[Required]
		[Display(Name = "Age")]
		public int Age { get; set; }
	}
}