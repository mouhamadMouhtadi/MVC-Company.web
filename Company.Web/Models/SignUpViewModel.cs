using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
	public class SignUpViewModel
	{
        [Required(ErrorMessage ="First Name is Required")]
        public string FirstName { get; set; }
		[Required(ErrorMessage = "Last Name is Required")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "Email is Required")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is Required")]
		[RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^\w\s]).+", ErrorMessage = "Password must have at least one lowercase letter, one uppercase letter, one digit, and one non-alphanumeric character.")]
		public string Password { get; set; }
		[Required(ErrorMessage = "ConfirmPassword is Required")]
		[Compare(nameof(Password),ErrorMessage ="Confirm Password Doesn't match password")]
		public string ConfirmPassword { get; set; }
		[Required(ErrorMessage = "IsActive is Required")]
		public bool IsActive { get; set; }
		
    }
}
