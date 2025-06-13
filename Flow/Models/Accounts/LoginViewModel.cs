using System.ComponentModel.DataAnnotations;

namespace Flow.Models.Accounts
{
    public class LoginViewModel
	{
		[Required]
		public string Name { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}